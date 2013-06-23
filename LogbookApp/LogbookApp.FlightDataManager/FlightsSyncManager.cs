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


        private async void SyncLookups(Lookups lookups, int userId)
        {
            var onLineLookups = await _onLineDataService.LoadLookups(userId);
            await SyncTable(lookups.Aircraft, onLineLookups.Aircraft);
            await SyncTable(lookups.Airfields, onLineLookups.Airfields);
        }

        public async override Task UpdateTargetData(FlightData sourceData, FlightData targetData, DateTime now)
        {
            
            SyncLookups(sourceData.Lookups, sourceData.User.id);
            await SyncTable(sourceData.Flights, targetData.Flights);
            targetData.User.TimeStamp = sourceData.User.TimeStamp;  // TODO : could this go in the base class
        }

    }
}
