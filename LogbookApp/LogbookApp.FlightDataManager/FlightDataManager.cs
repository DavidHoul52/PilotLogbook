using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
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
        
        private readonly Action _onlineDataUpdatedFromOffLine;
        
        private UserManager _userManager;
        private ISyncManager _syncManager;
        private readonly IInternetTools _internetTools;
        public IFlightDataService DataService { get; private set; }
        public LocalDataService LocalDataService { get; private set; }


        public FlightDataManager(IOnlineFlightData onLineData, LocalDataService localDataService,
           ISyncManager syncManager, IInternetTools internetTools)
        {
            _onLineData = onLineData;
            LocalDataService = localDataService;
            _syncManager = syncManager;
            _internetTools = internetTools;
            
            
            _userManager = new UserManager();
            FlightData = new FlightData();
          


        }

        


        public FlightData FlightData { get; set; }
    

         public async Task StartUp(string displayName)
        {
            DisplayName = displayName;
            await LoadData(LocalDataService);
            await CheckConnectionState();
          
        }

        public string DisplayName { get; set; }


        public async Task LoadData(IFlightDataService flightDataService)
        {
            var userdataExists = await flightDataService.UserDataExists(DisplayName);
            if (!userdataExists)
            {
                if (FlightData.User == null || FlightData.User.DisplayName != DisplayName)
                    FlightData.User = _userManager.CreateUser(DisplayName,DateTime.Now);
                await flightDataService.CreateUserData(FlightData, DateTime.Now);
            }
            else
               

            FlightData.User = await flightDataService.GetUser(DisplayName);
            FlightData.Lookups = await flightDataService.GetLookups(FlightData.User.id);
            FlightData.Flights = await flightDataService.GetFlights(FlightData.User.id);

        }

     




      


        public async Task CheckConnectionState()
        {
            if (_internetTools.IsConnected)
                DataService = _onLineData;
                
            else
                DataService = LocalDataService;

            await OnDataServiceChanged();
        }


      

        private async Task OnDataServiceChanged()
        {


            if (DataService.DataType == DataType.OnLine)
            {
                await LoadData(DataService);

                if (DetectNeedForSyncUpdate())
                   await _syncManager.UpdateOnlineData(FlightData, DateTime.Now);
            }


        
           
        }

        public bool DetectNeedForSyncUpdate()
        {
            return _onLineData.LastUpdated == null || (LocalDataService.LastUpdated >
                _onLineData.LastUpdated);

        }


        private async Task PerformDataGetAction(Func<IFlightDataService, Task> dataAction)
        {
            await dataAction(DataService);
         
        }

      

   

        public async Task LoadData()
        {
            await LoadData(DataService);
        }

        public async Task GetLookups()
        {
            {
                FlightData.Lookups = await DataService.GetLookups(FlightData.User.id);
            }
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
            entity.TimeStamp = upDateTime;

            await CheckConnectionState();

            FlightData.User.TimeStamp = upDateTime;
            if (DataService.DataType == DataType.OnLine)
            {
                await updateAction(_onLineData);
                await _onLineData.UpdateUser(FlightData.User);
            }

            await (LocalDataService.SaveFlightData(FlightData));
            await LocalDataService.UpdateUser(FlightData.User);
            



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