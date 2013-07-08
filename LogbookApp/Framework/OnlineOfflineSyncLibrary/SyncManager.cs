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

       

      
        public abstract Task UpdateOnlineData(TOnlineDataService onlineDataService,
            TSyncableData sourceData, TSyncableData targetData, DateTime now);
     


        protected async Task SyncTableUpdates<T>(ObservableCollection<T> sourceItems,
            ObservableCollection<T> targetItems, TSyncableData targetData)
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

           
        }

        protected async Task SyncTableDeletes<T>(ObservableCollection<T> sourceItems,
        ObservableCollection<T> targetItems, TSyncableData targetData)
        where T : IEntity
        {
           

            // delete items which no longer exist in source
            foreach (var targetItem in targetItems)
            {
                
                if (NotFoundIn(sourceItems,targetItem) && targetData.CanDelete(targetItem))
                {
                    await OnLineDataService.Delete(targetItem);
                }

            }


        }


        private static bool NotFoundIn<T>(ObservableCollection<T> sourceItems, T x) where T: IEntity
        {
            return sourceItems.FirstOrDefault(s => s.id == x.id)==null;
        }
    }
}