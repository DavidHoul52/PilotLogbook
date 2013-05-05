using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogbookApp.Storage;

namespace LogbookApp.Data
{
    public class FlightDataManager : IFlightDataManager
    {
        private readonly IFlightDataService _onLineData;
        private readonly LocalDataManager _localData;
        private readonly Action _onlineDataUpdatedFromOffLine;
        private readonly string _displayName;
        private IUserManager _userManager;

        public FlightDataManager(IFlightDataService onLineData, LocalDataManager localData,
            Action onlineDataUpdatedFromOffLine, string displayName)
        {
            _onLineData = onLineData;
            _localData = localData;
            _onlineDataUpdatedFromOffLine = onlineDataUpdatedFromOffLine;
            _displayName = displayName;
            _userManager = new UserManager();
        }

        public DataType DataType { get; private set; }
        public List<Flight> Flights { get; set; }
      

        public async Task<bool> GetData(DateTime now)
        {
            var success = await PerformDataGetAction(async (flightDataService) =>
            {
                if (flightDataService != null)
                {
                    _userManager.DisplayName = _displayName;
                    await _userManager.GetUser(flightDataService, now);
                    User = _userManager.User;
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
            if (await _onLineData.Available(_displayName))
            {
                DataType=DataType.OnLine;
                if (_localData.User!=null && (_onLineData.User.LastUpdated == null ||
                    _onLineData.User.LastUpdated < _localData.User.LastUpdated))
                    UpdateOnlineDataFromOffLineData();
                return _onLineData;
            }
            if (await _localData.Available(_displayName))
            {
                DataType = DataType.OffLine;
                return _localData;
            }
            DataType=DataType.None;
            return null;

        }

        private void UpdateOnlineDataFromOffLineData()
        {
            if (_onlineDataUpdatedFromOffLine != null) _onlineDataUpdatedFromOffLine();
        }


        public async Task GetLookups()
        {
            await PerformDataGetAction(async (flightDataService) =>
            {
                await flightDataService.GetLookups();

            });
        }

    

        public Lookups Lookups { get; set; }
        public async Task<bool> InsertFlight(Flight flight, DateTime insertTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.InsertFlight(flight), insertTime);
            return true;

        }

        public async Task<bool> DeleteFlight(Flight flight, DateTime deleteTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteFlight(flight), deleteTime);
            return true;
        }

      
        public async Task<bool> SaveFlight(Flight flight, DateTime saveTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.SaveFlight(flight), saveTime);
            return true;
        }

      

        public async Task InsertAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAircraft(aircraft),upDateTime);
     
        }

        private async Task PerformDataUpdateAction(Func<IFlightDataService,Task> dataAction,  DateTime upDateTime)
        {
            var availableData = await GetAvailableDataService();
            await dataAction(availableData);
            await availableData.UpdateUser(upDateTime);
          
        }

        public async Task InsertAircraftType(AcType acType,DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAircraftType(acType), upDateTime);
        }

        public async Task InsertAirfield(Airfield @from, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAirfield(@from), upDateTime);
        }

        public async Task UpdateAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAircraft(aircraft), upDateTime);
        }

        public async Task<bool> DeleteAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteAircraft(aircraft), upDateTime);
            return true;
        }

        public async Task UpdateAirfield(Airfield airfield, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAirfield(airfield), upDateTime);
        }

        public async Task<bool> DeleteAirfield(Airfield airfield, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAirfield(airfield), upDateTime);
            return true;
        }

        public async Task UpdateAcType(AcType acType, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAcType(acType), upDateTime);
        }

        public async Task InsertAcType(AcType acType, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAcType(acType), upDateTime);
        }



        public async Task InsertUser(User user, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.InsertUser(user), upDateTime);
        }

        public async Task GetUser()
        {
            await PerformDataGetAction((flightservice) => flightservice.GetUser(_displayName));
        }

        public User User { get; private set; }
        public bool FlightsChanged { get; set; }
        public async Task GetFlights()
        {
            await PerformDataGetAction((flightservice) => flightservice.GetFlights());
            
            
        }

        public async Task<bool> Available()
        {
            return DataType != DataType.None;
        }

        public async Task<bool> Delete<T>(T item, DateTime deleteTime)
        {
            return await PerformDataGetAction((flightservice) => flightservice.Delete<T>(item));
          
        }
    }
}