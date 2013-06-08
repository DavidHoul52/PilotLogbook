using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using InternetDetection;
using LogbookApp.Data;
using LogbookApp.Storage;

namespace LogbookApp.FlightDataManagement
{
    public class FlightDataManager : IFlightDataManager
    {
        private readonly IOnlineFlightData _onLineData;
        private readonly LocalDataService _localData;
        private readonly Action _onlineDataUpdatedFromOffLine;
        
        private UserManager _userManager;
        private ISyncManager _syncManager;
        private readonly IInternetTools _internetTools;
        public IFlightDataService DataService { get; private set; }


        public FlightDataManager(IOnlineFlightData onLineData, LocalDataService localData,
           ISyncManager syncManager, IInternetTools internetTools)
        {
            _onLineData = onLineData;
            _localData = localData;
            _syncManager = syncManager;
            _internetTools = internetTools;
            
            _userManager = new UserManager();
            FlightData = new FlightData();
          


        }


        public FlightData FlightData { get; set; }
    

         public async void StartUp(string displayName)
        {
            _userManager.DisplayName = displayName;
            var localUser = await _localData.GetUser(displayName); 
            
            var onLineUser= await GetOnLineUser();
            
            
            var localNewer = (localUser != null && onLineUser != null 
                && (onLineUser.TimeStamp == null || (localUser.TimeStamp > onLineUser.TimeStamp)));
             

             if (!_internetTools.IsConnected || localNewer)
             {
                 SetDataService(DataType.OffLine);
                 if (onLineUser != null)
                     await _syncManager.UpdateOnlineData(FlightData, DateTime.Now);
             }
             else
             {
                 SetDataService(DataType.OnLine);
                 
             }

             FlightData.User = await _userManager.GetUser(DataService, DateTime.Now);
             if (!FlightData.User.IsNew)
             {
                 await GetData();
             }

          

        }

        private async Task<User> GetOnLineUser()
        {
            if (_internetTools.IsConnected)
                return _onLineData.GetUser(_userManager.DisplayName).Result;
              
            return null;
        }


        public void ConnectionStateChanged(bool isConnected)
        {
            if (isConnected)
                SetDataService(DataType.OnLine);
                
            else
                SetDataService(DataType.OffLine);
        }


        public void SetDataService(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.OffLine:
                     DataService = _localData;
                    break;
                case DataType.OnLine:
                     DataService = _onLineData;
                     break;
                default:
                    throw new ArgumentOutOfRangeException("dataType");
            }
            
               
               
        }


        public async Task GetData()
        {
            await GetLookups();
            await GetFlights();
        }


        private async Task PerformDataGetAction(Func<IFlightDataService, Task> dataAction)
        {
            await dataAction(DataService);
         
        }

        //public async Task<IFlightDataService> GetAvailableDataService()
        //{
        //    var localUser = await _localData.GetUser("_displayName");// TODO:
        //    var localAvailable = (localUser != null);
        //    var onLineUser = await _onLineData.GetUser("_displayName");// TODO:
        //    if (onLineUser == null)
        //    {
        //        onLineUser = await SavedUserOnline();
        //        if (onLineUser != null)
        //            InsertUserOffline(onLineUser);


        //    }
        //    var onLineAvailable = (onLineUser != null);

        //    var localNewer = (localAvailable && onLineAvailable && localUser.TimeStamp
        //        > onLineUser.TimeStamp);
        //    if (!onLineAvailable && localAvailable || (localAvailable && localNewer))
        //        _dataService.DataType = DataType.OffLine;
        //    else if (onLineAvailable)
        //        _dataService.DataType = DataType.OnLine;
         

        //    if (_dataService.DataType == DataType.OffLine && onLineAvailable)
        //    {
        //        await GetData(_localData);
        //        await _syncManager.UpdateOnlineData(FlightData, DateTime.Now);
        //    }

            

        

        //}


      
       
        private async Task<User> SavedUserOnline()
        {

            return await InsertUser(new User { DisplayName = "_displayName" }, DateTime.Now, _onLineData);// TODO:
        }

        private async void InsertUserOffline(User onLineUser)
        {
            
            await InsertUser(onLineUser, DateTime.Now,_localData);
        }


        public async Task GetLookups()
        {
            {
                FlightData.Lookups = await DataService.GetLookups(FlightData.User.id);
            }
        }


        public async Task InsertFlight(Flight flight, DateTime insertTime)
        {
            FlightData.AddFlight(flight);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertFlight(flight),
                flight,
                insertTime);
            

        }

        public async Task DeleteFlight(Flight flight, DateTime deleteTime)
        {
            FlightData.Flights.Remove(flight);
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteFlight(flight),
                flight,
                deleteTime);
            
        }

      
        public async Task SaveFlight(Flight flight, DateTime saveTime)
        {
            
            await PerformDataUpdateAction((flightservice) => flightservice.SaveFlight(flight),
                flight,
                saveTime);
        }

      

        public async Task InsertAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            FlightData.AddAircraft(aircraft);
            
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAircraft(aircraft),
                aircraft,
                upDateTime);
     
        }

        private async Task PerformDataUpdateAction(Func<IOnlineFlightData,Task> updateAction,IEntity entity,
              DateTime upDateTime)
        {
            FlightData.User.TimeStamp = upDateTime;
            entity.TimeStamp = upDateTime;
            
            if (DataService.DataType == DataType.OnLine)
            {
                await updateAction(_onLineData);
                await _onLineData.UpdateUser(FlightData.User);
            }

            await (_localData.SaveFlightData(FlightData));
            await _localData.UpdateUser(FlightData.User);
            
            
          
        }

    

        public async Task InsertAirfield(Airfield @from, DateTime upDateTime)
        {
            FlightData.AddAirfield(@from);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAirfield(@from),
                from,upDateTime);
        }

        public async Task UpdateAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAircraft(aircraft),
                aircraft,
                upDateTime);
        }

        public async Task DeleteAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            FlightData.RemoveAircraft(aircraft);
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteAircraft(aircraft),
                aircraft,
                upDateTime);
            
        }

        public async Task UpdateAirfield(Airfield airfield, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAirfield(airfield), 
                airfield,
                upDateTime);
        }

        public async Task DeleteAirfield(Airfield airfield, DateTime upDateTime)
        {
            FlightData.RemoveAirfield(airfield);
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteAirfield(airfield),
                airfield,
                upDateTime);
         
        }

        public async Task UpdateAcType(AcType acType, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAcType(acType),
                acType,
                upDateTime);
        }

        public async Task InsertAcType(AcType acType, DateTime upDateTime)
        {
            FlightData.AddAcType(acType);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAcType(acType),
                acType,
                upDateTime);
        }



        public async Task InsertUser(User user, DateTime upDateTime)
        {
            FlightData.User = user;
            await PerformDataUpdateAction((flightservice) => flightservice.InsertUser(user),
                user,
                upDateTime);
        }

          public async Task<User> InsertUser(User user, DateTime upDateTime, IFlightDataService flightservice)
          {
            user.TimeStamp = upDateTime;
            FlightData.User = user;
            await flightservice.InsertUser(user);
            return user;

        }


       

        
        public bool FlightsChanged { get; set; }


        public async Task GetFlights()
        {
            FlightData.Flights = await DataService.GetFlights(FlightData.User.id);
        }


        public async Task DeleteAcType(AcType acType, DateTime upDateTime)
        {
            await FlightData.RemoveAcType(acType);
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteAcType(acType),
                acType,
                upDateTime);
        }


       
    }
}