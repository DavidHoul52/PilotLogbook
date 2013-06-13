using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;
using LogbookApp.Data;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.FlightDataManagement
{
    public class FlightsSyncManager : SyncManager<FlightData,IOnlineFlightData>
    {
        public FlightsSyncManager(IOnlineFlightData onlineDataService)
            : base(onlineDataService)
        {
        }

        public async override Task UpdateOnlineData(FlightData sourceFlightData, DateTime now)
        {
            await SyncLookups(sourceFlightData.Lookups, sourceFlightData.User.id);
            var onLineFlights = await _onLineDataService.GetFlights(sourceFlightData.User.id);
            await SyncTable<Flight>(sourceFlightData.Flights, onLineFlights);
            sourceFlightData.User.TimeStamp = now;
            await _onLineDataService.UpdateUserFromLocal(sourceFlightData.User);
        }


        private async Task SyncLookups(Lookups lookups, int userId)
        {
            var onLineLookups = await _onLineDataService.GetLookups(userId);
            await SyncTable(lookups.Aircraft, onLineLookups.Aircraft);
            await SyncTable(lookups.Airfields, onLineLookups.Airfields);

            bool done = true;
        }
    }
}
