using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary
{
    public abstract class SyncManager<TSyncableData,TOnlineDataService,TUser> :
        ISyncManager<TSyncableData, TOnlineDataService,TUser>
        where TOnlineDataService : IDataService<TSyncableData,TUser>
        where TSyncableData : ISyncableData<TUser>
        where TUser : IUser
    {
        protected TOnlineDataService OnLineDataService;

       

      
        public abstract Task UpdateTargetData(TOnlineDataService onlineDataService, TSyncableData sourceData, TSyncableData targetData, DateTime now);
     


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
                    await OnLineDataService.Insert(item);
                }
                else
                if (item.TimeStamp > targetItem.TimeStamp)
                    await OnLineDataService.Update(item);
            }

            // delete items which no longer exist in source

            foreach (var targetItem in targetItems)
            {
                var sourceItem = sourceItems.FirstOrDefault(x => x.id == targetItem.id);
                if (sourceItem == null) // doesn't exist
                {
                   await OnLineDataService.Delete(targetItem);
                }
                
            }
        }


       
     
    }
}