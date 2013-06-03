using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using LogbookApp.Data;
using LogbookApp.Storage;

namespace LogbookApp.FlightDataManagement
{
    public class FlightDataManager : IFlightDataManager
    {
        private readonly IOnlineFlightData _onLineData;
        private readonly LocalDataService _localData;
        private readonly Action _onlineDataUpdatedFromOffLine;
        private readonly string _displayName;
        private UserManager _userManager;
        private ISyncManager _syncManager;


        public FlightDataManager(IOnlineFlightData onLineData, LocalDataService localData,
            string displayName,ISyncManager syncManager)
        {
            _onLineData = onLineData;
            _localData = localData;
            _syncManager = syncManager;
            _displayName = displayName;
            _userManager = new UserManager();
            FlightData = new FlightData();
          


        }


        public FlightData FlightData { get; set; }
        public DataType DataType { get; private set; }
        
      

        public async Task<bool> GetData(DateTime now)
        {
            return await PerformDataGetAction(async (flightDataService) =>
            {
                if (flightDataService != null)
                {
                    _userManager.DisplayName = _displayName;
                
                     
                     await GetData(flightDataService);
                }

            });
          
            
        }

        private async Task GetData(IFlightDataService flightDataService)
        {
            await _userManager.GetUser(flightDataService, DateTime.Now);
            FlightData.User = _userManager.User;
            await GetLookups(flightDataService);
            await GetFlights(flightDataService);
        }


        private async Task<bool> PerformDataGetAction(Func<IFlightDataService, Task> dataAction)
        {
            var availableData = await GetAvailableDataService();
            
            if (availableData != null)
            {
                await dataAction(availableData);
                return true;
            }
            return false;
        }

        public async Task<IFlightDataService> GetAvailableDataService()
        {
            var localUser = await _localData.GetUser(_displayName);
            var localAvailable = (localUser != null);
            var onLineUser = await _onLineData.GetUser(_displayName);
            if (onLineUser == null)
            {
                onLineUser = await SavedUserOnline();
                if (onLineUser != null)
                    InsertUserOffline(onLineUser);


            }
            var onLineAvailable = (onLineUser != null);
            var localNewer = (localAvailable && onLineAvailable && localUser.TimeStamp
                > onLineUser.TimeStamp);
            if (!onLineAvailable && localAvailable || (localAvailable && localNewer))
                DataType=DataType.OffLine;
            else if (onLineAvailable)
                DataType = DataType.OnLine;
            else
                DataType = DataType.None;


            if (DataType == DataType.OffLine && onLineAvailable)
            {
                await GetData(_localData);
                await _syncManager.UpdateOnlineData(FlightData, DateTime.Now);
            }

            switch (DataType)
            {
                case DataType.None:
                    return null;
                case DataType.OffLine:
                    return _localData;
                case DataType.OnLine:
                    return _onLineData;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        

        }

       
        private async Task<User> SavedUserOnline()
        {

            return await InsertUser(new User { DisplayName = _displayName }, DateTime.Now, _onLineData);
        }

        private async void InsertUserOffline(User onLineUser)
        {
            
            await InsertUser(onLineUser, DateTime.Now,_localData);
        }


        public async Task GetLookups()
        {
            await PerformDataGetAction(async (flightDataService) =>
            {
                await GetLookups(flightDataService);
            });
        }

        private async Task GetLookups(IFlightDataService flightDataService)
        {
            {
                FlightData.Lookups = await flightDataService.GetLookups(FlightData.User.id);
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
            var availableData =await GetAvailableDataService();
            if (DataType == DataType.OnLine)
            {
                await updateAction(availableData as IOnlineFlightData);
                await availableData.UpdateUser(FlightData.User);
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


        public async Task GetUser()
        {
            await PerformDataGetAction(async (flightservice) =>
               FlightData.User= await flightservice.GetUser(_displayName));
        }

        
        public bool FlightsChanged { get; set; }
        public async Task GetFlights()
        {
            await PerformDataGetAction(async (flightservice) =>
            {
                await GetFlights(flightservice);

            });
            
            
        }

        private async Task GetFlights(IFlightDataService flightservice)
        {
            FlightData.Flights = await flightservice.GetFlights(FlightData.User.id);
        }

        public async Task<bool> Available()
        {
            return DataType != DataType.None;
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