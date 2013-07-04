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
     

        private async Task SyncUpdatesLookups(Lookups sourceLookups,FlightData targetData)
        {

            await SyncTableUpdates(sourceLookups.Aircraft, targetData.Lookups.Aircraft,targetData);
            await SyncTableUpdates(sourceLookups.Airfields, targetData.Lookups.Airfields,targetData);
        }

        private async Task SyncDeleteLookups(Lookups sourceLookups, FlightData targetData)
        {

            await SyncTableDeletes(sourceLookups.Aircraft, targetData.Lookups.Aircraft, targetData);
            await SyncTableDeletes(sourceLookups.Airfields, targetData.Lookups.Airfields, targetData);
        }



        public async override Task UpdateOnlineData(TOnlineFlightData onlineDataService,
            FlightData sourceData, FlightData targetData, DateTime now)
        {
            OnLineDataService = onlineDataService;
            await SyncTableDeletes(sourceData.Flights, targetData.Flights, targetData);
            await SyncDeleteLookups(sourceData.Lookups, targetData);

            await SyncUpdatesLookups(sourceData.Lookups, targetData);
            await SyncTableUpdates(sourceData.Flights,targetData.Flights,targetData);

            sourceData.User.TimeStamp = DateTime.Now;
            await OnLineDataService.UpdateUserTimeStamp( sourceData.User.TimeStamp); // TODO : could this go in the base class
            
        }

      
    }
}
