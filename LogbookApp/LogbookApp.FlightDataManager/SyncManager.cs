using System.Collections.ObjectModel;
using System.Linq;
using LogbookApp.Data;

namespace LogbookApp.FlightDataManagement
{
    public class SyncManager
    {
        private readonly IOnlineFlightData _onLineData;

        public SyncManager(IOnlineFlightData onLineData)
        {
            _onLineData = onLineData;
        }

        public void UpdateOnlineData(FlightData flightData)
        {
            SyncLookups(flightData.Lookups, flightData.User.id);
        }

        private async void SyncLookups(Lookups lookups, int userId)
        {
            var onLineLookups = await _onLineData.GetLookups(userId);
            SyncTable(lookups.Aircraft, onLineLookups.Aircraft);
        }

        private void SyncTable<T>(ObservableCollection<T> sourceItems,
            ObservableCollection<T> targetItems)
            where T: Entity
        {
            // update items which have changed and are newer
            foreach (var item in sourceItems)
            {
                var targetItem = targetItems.FirstOrDefault(x => x.id == item.id);
                if (item.TimeStamp > targetItem.TimeStamp)
                    _onLineData.Update(item);
            }
        }
    }
}