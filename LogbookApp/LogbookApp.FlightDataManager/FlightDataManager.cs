using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using InternetDetection;
using LogbookApp.Data;
using LogbookApp.Storage;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.FlightDataManagement
{
    public class FlightDataManager<TOnlineFlightData,TOfflineFlightData,TFlightSyncManager>
        //: DataManager<FlightData, User, IOnlineFlightData,LocalDataService,
        //FlightsSyncManager<IOnlineFlightData>>,
        :DataManager<FlightData,User,TOnlineFlightData,TOfflineFlightData,TFlightSyncManager>,
        IFlightDataManager
        where TOnlineFlightData : IOnlineFlightData
        where TOfflineFlightData : IOfflineDataService<FlightData, User>
        where TFlightSyncManager : ISyncManager<FlightData, TOnlineFlightData, User> 
        
    {
        private readonly TOnlineFlightData _onlineDataService;

        public FlightDataManager()
        {
            
        }
        public FlightDataManager(TOnlineFlightData onlineDataService,
            TOfflineFlightData offlineDataService, IInternetTools internet,
            TFlightSyncManager syncManager) : 
            base(onlineDataService, offlineDataService, internet, syncManager)
        {
            _onlineDataService = onlineDataService;
        }

        public async Task DeleteFlight(Flight flight, DateTime deleteTime)
        {
            Data.Flights.Remove(flight);
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
            Data.AddAircraft(aircraft);

            await PerformDataUpdateAction((flightservice) => flightservice.InsertAircraft(aircraft),
                aircraft,
                upDateTime);
     
        }

        public async Task InsertAirfield(Airfield @from, DateTime upDateTime)
        {
            Data.AddAirfield(@from);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAirfield(@from),
                from, upDateTime);
        }

        public async Task UpdateAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            await PerformDataUpdateAction((flightservice) => flightservice.UpdateAircraft(aircraft),
              aircraft,
              upDateTime);
        }

        public async Task DeleteAircraft(Aircraft aircraft, DateTime upDateTime)
        {
            Data.RemoveAircraft(aircraft);
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
            Data.RemoveAirfield(airfield);
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
            Data.AddAcType(acType);
            await PerformDataUpdateAction((flightservice) => flightservice.InsertAcType(acType),
                acType,
                upDateTime);
        }


        public async Task DeleteAcType(AcType acType, DateTime upDateTime)
        {
            await Data.RemoveAcType(acType);
            await PerformDataUpdateAction((flightservice) => flightservice.DeleteAcType(acType),
                acType,
                upDateTime);
        }

        public async Task GetFlights()
        {
           if (_onlineDataService.ConnectionTracker.IsConnected) 
             await  _onlineDataService.GetFlights(Data.User.id);
            
        }

        public async Task LoadData()
        {
            if (_onlineDataService.ConnectionTracker.IsConnected)
                Data= await LoadUserData(_onlineDataService);
        }

        public async Task GetLookups()
        {
                if (_onlineDataService.ConnectionTracker.IsConnected)
                Data.Lookups=await _onlineDataService.LoadLookups(Data.User.id);
        
        }
    }
}
