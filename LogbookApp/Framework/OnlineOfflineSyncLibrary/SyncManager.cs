using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary
{
    public abstract class SyncManager<TSyncableData,TOnlineDataService,TUser> : ISyncManager<TSyncableData,TUser>
        where TOnlineDataService : IDataService
        where TSyncableData : ISyncableData<TUser>
        where TUser : IUser
    {
        protected readonly TOnlineDataService _onLineDataService;

        public SyncManager(TOnlineDataService onlineDataService)
        {
            _onLineDataService = onlineDataService;
            
        }

        public abstract Task UpdateTargetData(TSyncableData sourceFlightData, DateTime now);
       

     


        protected async Task SyncTable<T>(ObservableCollection<T> sourceItems,
            ObservableCollection<T> targetItems)
            where T: IEntity
        {
            // update items which have changed and are newer
            foreach (var item in sourceItems)
            {
                var targetItem = targetItems.FirstOrDefault(x => x.id == item.id);
                if (targetItem == null) // new
                {
                    await _onLineDataService.Insert(item);
                }
                else
                if (item.TimeStamp > targetItem.TimeStamp)
                    await _onLineDataService.Update(item);
            }

            // delete items which no longer exist in source

            foreach (var targetItem in targetItems)
            {
                var sourceItem = sourceItems.FirstOrDefault(x => x.id == targetItem.id);
                if (sourceItem == null) // doesn't exist
                {
                   await _onLineDataService.Delete(targetItem);
                }
                
            }
        }

      
    }
}