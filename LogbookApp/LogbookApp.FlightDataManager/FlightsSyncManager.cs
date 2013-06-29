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
     

        private async void SyncLookups(Lookups sourceLookups,Lookups targetLookups)
        {

            await SyncTable(sourceLookups.Aircraft, targetLookups.Aircraft);
            await SyncTable(sourceLookups.Airfields, targetLookups.Airfields);
        }




        public async override Task UpdateOnlineData(TOnlineFlightData onlineDataService,
            FlightData sourceData, FlightData targetData, DateTime now)
        {
            OnLineDataService = onlineDataService;
            SyncLookups(sourceData.Lookups, targetData.Lookups);
            await SyncTable(sourceData.Flights,targetData.Flights);

            sourceData.User.TimeStamp = DateTime.Now;
            await OnLineDataService.UpdateUserTimeStamp( sourceData.User.TimeStamp); // TODO : could this go in the base class
            
        }

      
    }
}
