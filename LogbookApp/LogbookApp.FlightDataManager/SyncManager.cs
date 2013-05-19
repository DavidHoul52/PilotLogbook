using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.FlightDataManagement
{
    public class SyncManager : ISyncManager
    {
        private readonly IOnlineFlightData _onLineData;

        public SyncManager(IOnlineFlightData onLineData)
        {
            _onLineData = onLineData;
        }

        public async Task UpdateOnlineData(FlightData flightData, DateTime now)
        {
            await SyncLookups(flightData.Lookups, flightData.User.id);
            var onLineFlights = await _onLineData.GetFlights(flightData.User.id);
            await SyncTable<Flight>(flightData.Flights, onLineFlights);
            flightData.User.TimeStamp = now;
            await _onLineData.UpdateUser(flightData.User);

        }

        private async Task SyncLookups(Lookups lookups, int userId)
        {
            var onLineLookups = await _onLineData.GetLookups(userId);
            await SyncTable(lookups.Aircraft, onLineLookups.Aircraft);
            await SyncTable(lookups.Airfields, onLineLookups.Airfields);
             
            bool done = true;
        }

        private async Task SyncTable<T>(ObservableCollection<T> sourceItems,
            ObservableCollection<T> targetItems)
            where T: IEntity
        {
            // update items which have changed and are newer
            foreach (var item in sourceItems)
            {
                var targetItem = targetItems.FirstOrDefault(x => x.id == item.id);
                if (targetItem == null) // new
                {
                    await _onLineData.Insert(item);
                }
                else
                if (item.TimeStamp > targetItem.TimeStamp)
                    await _onLineData.Update(item);
            }

            // delete items which no longer exist in source

            foreach (var targetItem in targetItems)
            {
                var sourceItem = sourceItems.FirstOrDefault(x => x.id == targetItem.id);
                if (sourceItem == null) // doesn't exist
                {
                   await _onLineData.Delete(targetItem);
                }
                
            }
        }
    }
}