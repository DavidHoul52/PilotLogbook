using System;
using System.Threading.Tasks;
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
        private SyncManager _syncManager;


        public FlightDataManager(IOnlineFlightData onLineData, LocalDataService localData,
            string displayName)
        {
            _onLineData = onLineData;
            _localData = localData;
            _syncManager = new SyncManager(_onLineData);
            _displayName = displayName;
            _userManager = new UserManager();
            FlightData = new FlightData();
            localData.SetFlightData(FlightData);


        }


        public FlightData FlightData { get; set; }
        public DataType DataType { get; private set; }
        
      

        public async Task<bool> GetData(DateTime now)
        {
            var success = await PerformDataGetAction(async (flightDataService) =>
            {
                if (flightDataService != null)
                {
                    _userManager.DisplayName = _displayName;
                     await _userManager.GetUser(flightDataService, now);
                     FlightData.User = _userManager.User;
                   
                }

            });
            if (success)
            {
                await GetLookups();
                await GetFlights();
                
                return true;
            }
            return false;
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

        private async Task<IFlightDataService> GetAvailableDataService()
        {
            var localAvailable = await _localData.Available(_displayName);
            var onLineAvailable = await _onLineData.Available(_displayName);
            var localNewer = (localAvailable && onLineAvailable && 
                _localData.LastUpdated.GetValueOrDefault(DateTime.MinValue) >
                _onLineData.LastUpdated.GetValueOrDefault(DateTime.MinValue));
            if (!onLineAvailable && localAvailable || (localAvailable && localNewer))
                DataType=DataType.OffLine;
            else if (onLineAvailable)
                DataType = DataType.OnLine;
            else
                DataType = DataType.None;


            if (DataType == DataType.OffLine && onLineAvailable)
                _syncManager.UpdateOnlineData(FlightData);

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

        private void UpdateOnlineDataFromOffLineData()
        {
            
        }


        public async Task GetLookups()
        {
            await PerformDataGetAction(async (flightDataService) =>
            {
                { FlightData.Lookups = await flightDataService.GetLookups(FlightData.User.id); }

            });
        }

    

        
        public async Task InsertFlight(Flight flight, DateTime insertTime)
        {
            FlightData.AddFlight(flight);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertFlight(flight), insertTime);
            

        }

        public async Task DeleteFlight(Flight flight, DateTime deleteTime)
        {
            FlightData.Flights.Remove(flight);
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteFlight(flight), deleteTime);
            
        }

      
        public async Task SaveFlight(Flight flight, DateTime saveTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.SaveFlight(flight), saveTime);
        }

      

        public async Task InsertAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            FlightData.Lookups.Aircraft.Add(aircraft);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAircraft(aircraft),upDateTime);
     
        }

        private async Task PerformDataUpdateAction(Func<IFlightDataService,Task> dataAction,  DateTime upDateTime)
        {
            FlightData.User.LastUpdated = upDateTime;
            var availableData = await GetAvailableDataService();
            await dataAction(availableData);
            if (availableData.DataType != DataType.OffLine)
            {
                await (dataAction(_localData));
                await _localData.UpdateUser(FlightData.User);
            }
            await availableData.UpdateUser(FlightData.User);
          
        }

    

        public async Task InsertAirfield(Airfield @from, DateTime upDateTime)
        {
            FlightData.AddInsert(@from);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAirfield(@from), upDateTime);
        }

        public async Task UpdateAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAircraft(aircraft), upDateTime);
        }

        public async Task DeleteAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            FlightData.RemoveAircraft(aircraft);
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteAircraft(aircraft), upDateTime);
            
        }

        public async Task UpdateAirfield(Airfield airfield, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAirfield(airfield), upDateTime);
        }

        public async Task DeleteAirfield(Airfield airfield, DateTime upDateTime)
        {
            FlightData.RemoveAirfield(airfield);
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAirfield(airfield), upDateTime);
         
        }

        public async Task UpdateAcType(AcType acType, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAcType(acType), upDateTime);
        }

        public async Task InsertAcType(AcType acType, DateTime upDateTime)
        {
            FlightData.AddAcType(acType);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAcType(acType), upDateTime);
        }



        public async Task InsertUser(User user, DateTime upDateTime)
        {
            FlightData.User = user;
            await PerformDataUpdateAction((flightservice) => flightservice.InsertUser(user), upDateTime);
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
                FlightData.Flights = await flightservice.GetFlights(FlightData.User.id);

            });
            
            
        }

        public async Task<bool> Available()
        {
            return DataType != DataType.None;
        }

        public async Task DeleteAcType(AcType acType, DateTime upDateTime)
        {
            await FlightData.RemoveAcType(acType);
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteAcType(acType), upDateTime);
        }
    }
}