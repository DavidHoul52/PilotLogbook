using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary
{
    public interface ISyncManager<TSyncableData,TOnlineDataService,TUser>
        where TUser : IUser
        where TSyncableData: ISyncableData<TUser>
        where TOnlineDataService : IDataService<TSyncableData, TUser>
          
    {
        Task UpdateTargetData(TOnlineDataService onlineDataService, TSyncableData sourceData, TSyncableData targetData, DateTime now);
      
    }
}