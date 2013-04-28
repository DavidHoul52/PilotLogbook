using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class FlightDataManager : IFlightDataManager
    {
        private readonly IFlightDataService _onLineData;
        private readonly IFlightDataService _localData;
        private readonly Action _onlineDataUpdatedFromOffLine;

        public FlightDataManager(IFlightDataService onLineData, IFlightDataService localData,
            Action onlineDataUpdatedFromOffLine)
        {
            _onLineData = onLineData;
            _localData = localData;
            _onlineDataUpdatedFromOffLine = onlineDataUpdatedFromOffLine;
        }

        public DataType DataType { get; private set; }
        public List<Flight> Flights { get; set; }
        public async Task GetData()
        {
            await PerformDataGetAction((flightDataService)=> flightDataService.GetData());
        }


        private async Task PerformDataGetAction(Func<IFlightDataService, Task> dataAction)
        {
            var availableData = await GetAvailableDataService();
            await dataAction(availableData);
            

        }

        private async Task<IFlightDataService> GetAvailableDataService()
        {
            if (await _onLineData.Available())
            {
                DataType=DataType.OnLine;
                if (_onLineData.User.LastUpdated == null ||
                    _onLineData.User.LastUpdated < _localData.User.LastUpdated)
                    UpdateOnlineDataFromOffLineData();
                return _onLineData;
            }
            DataType=DataType.OffLine;
            return _localData;

        }

        private void UpdateOnlineDataFromOffLineData()
        {
            if (_onlineDataUpdatedFromOffLine != null) _onlineDataUpdatedFromOffLine();
        }


        public async Task GetLookups()
        {
            throw new System.NotImplementedException();
        }

        public Lookups Lookups { get; set; }
        public Task<bool> InsertFlight(Flight flight)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteFlight(Flight flight)
        {
            throw new System.NotImplementedException();
        }

      
        public async Task<bool> SaveFlight(Flight flight, DateTime saveTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.SaveFlight(flight), saveTime);
            return true;
        }

        public void SaveFlights()
        {
            throw new System.NotImplementedException();
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
            await PerformDataGetAction((flightservice) => flightservice.GetUser());
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
            await PerformDataGetAction((flightservice) => flightservice.Delete<T>(item));
            return true;
        }
    }
}