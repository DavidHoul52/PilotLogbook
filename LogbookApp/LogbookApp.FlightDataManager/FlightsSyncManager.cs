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
    public class FlightsSyncManager<TOnlineFlightData> : SyncManager<FlightData,TOnlineFlightData,User>
        where TOnlineFlightData : IOnlineFlightData
    {
     

        private async void SyncLookups(Lookups lookups, int userId)
        {
            var onLineLookups = await OnLineDataService.LoadLookups(userId);
            await SyncTable(lookups.Aircraft, onLineLookups.Aircraft);
            await SyncTable(lookups.Airfields, onLineLookups.Airfields);
        }

     

  
        public async override Task UpdateTargetData(TOnlineFlightData onlineDataService,
            FlightData sourceData, FlightData targetData, DateTime now)
        {
            OnLineDataService = onlineDataService;
            SyncLookups(sourceData.Lookups, sourceData.User.id);
            var flights = await OnLineDataService.GetFlights(sourceData.User.id);
            await SyncTable(sourceData.Flights, flights);
            targetData.User.TimeStamp = sourceData.User.TimeStamp;  // TODO : could this go in the base class
        }
    }
}
