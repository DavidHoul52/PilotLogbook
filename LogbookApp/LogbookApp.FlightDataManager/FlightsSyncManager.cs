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
    public class FlightsSyncManager : SyncManager<FlightData,IOnlineFlightData,User>
    {
        public FlightsSyncManager(IOnlineFlightData onlineDataService)
            : base(onlineDataService)
        {
        }


        private void SyncLookups(Lookups lookups, int userId)
        {
            var onLineLookups = await _onLineDataService.GetLookups(userId);
            await SyncTable(lookups.Aircraft, onLineLookups.Aircraft);
            await SyncTable(lookups.Airfields, onLineLookups.Airfields);

            bool done = true;
        }

        public override Task UpdateTargetData(FlightData sourceData, FlightData targetData, DateTime now)
        {
            SyncLookups(sourceData.Lookups, sourceData.User.id);
            SyncTable(sourceData.Flights, targetData.Flights);
            sourceData.User.TimeStamp = now;
            await _onLineDataService.UpdateUserFromLocal(sourceFlightData.User);
        }
    }
}
