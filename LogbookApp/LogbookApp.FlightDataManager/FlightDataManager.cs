using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Core.AnimationMetrics;
using LogbookApp.LocalData;
using LogbookApp.Storage;

namespace LogbookApp.Data
{
    public class FlightDataManager : IFlightDataManager
    {
        private readonly IFlightDataService _onLineData;
        private readonly LocalDataService _localData;
        private readonly Action _onlineDataUpdatedFromOffLine;
        private readonly string _displayName;
        private UserManager _userManager;
        private LocalDataSyncer _localDataSyncer;
        

        public FlightDataManager(IFlightDataService onLineData, LocalDataService localData,
            Action onlineDataUpdatedFromOffLine, string displayName)
        {
            _onLineData = onLineData;
            _localData = localData;
            _onlineDataUpdatedFromOffLine = onlineDataUpdatedFromOffLine;
            _displayName = displayName;
            _userManager = new UserManager();
            _localDataSyncer = new LocalDataSyncer();

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
            var localNewer = (localAvailable && onLineAvailable && _localData.LastUpdated >
                _onLineData.LastUpdated.GetValueOrDefault(DateTime.MinValue));
            if (!onLineAvailable && localAvailable || (localAvailable && localNewer))
                DataType=DataType.OffLine;
            else if (onLineAvailable)
                DataType = DataType.OnLine;
            else
                DataType = DataType.None;


            if (DataType == DataType.OffLine && onLineAvailable)
                UpdateOnlineDataFromOffLineData();

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
            if (_onlineDataUpdatedFromOffLine != null) _onlineDataUpdatedFromOffLine();
        }


        public async Task GetLookups()
        {
            await PerformDataGetAction(async (flightDataService) =>
            {
                { FlightData.Lookups= await flightDataService.GetLookups();}

            });
        }

    

        
        public async Task<bool> InsertFlight(Flight flight, DateTime insertTime)
        {
            FlightData.Flights.Add(flight);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertFlight(flight), insertTime);
            return true;

        }

        public async Task<bool> DeleteFlight(Flight flight, DateTime deleteTime)
        {
            FlightData.Flights.Remove(flight);
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
            FlightData.Lookups.Aircraft.Add(aircraft);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAircraft(aircraft),upDateTime);
     
        }

        private async Task PerformDataUpdateAction(Func<IFlightDataService,Task> dataAction,  DateTime upDateTime)
        {
            var availableData = await GetAvailableDataService();
            await dataAction(availableData);
            if (availableData.DataType != DataType.OffLine)
            {
               _localDataSyncer.Sync(_onLineData,_localData);
                await (dataAction(_localData));
                await _localData.UpdateUser(upDateTime);
            }
            await availableData.UpdateUser(upDateTime);
          
        }

        public async Task InsertAircraftType(AcType acType,DateTime upDateTime)
        {
            FlightData.Lookups.AcTypes.Add(acType);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAircraftType(acType), upDateTime);
        }

        public async Task InsertAirfield(Airfield @from, DateTime upDateTime)
        {
            FlightData.Lookups.Airfields.Add(from);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAirfield(@from), upDateTime);
        }

        public async Task UpdateAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAircraft(aircraft), upDateTime);
        }

        public async Task<bool> DeleteAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            FlightData.Lookups.Aircraft.Remove(aircraft);
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteAircraft(aircraft), upDateTime);
            return true;
        }

        public async Task UpdateAirfield(Airfield airfield, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAirfield(airfield), upDateTime);
        }

        public async Task<bool> DeleteAirfield(Airfield airfield, DateTime upDateTime)
        {
            FlightData.Lookups.Airfields.Remove(airfield);
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAirfield(airfield), upDateTime);
            return true;
        }

        public async Task UpdateAcType(AcType acType, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAcType(acType), upDateTime);
        }

        public async Task InsertAcType(AcType acType, DateTime upDateTime)
        {
            FlightData.Lookups.AcTypes.Add(acType);
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
                FlightData.Flights = await flightservice.GetFlights();

            });
            
            
        }

        public async Task<bool> Available()
        {
            return DataType != DataType.None;
        }

        //public async Task<bool> Delete<T>(T item, DateTime deleteTime)
        //{
        //    return await PerformDataGetAction((flightservice) => flightservice.Delete<T>(item));
          
        //}
    }
}