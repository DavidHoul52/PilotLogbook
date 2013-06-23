using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary
{
    public interface ISyncManager<TSyncableData,TUser>
        where TUser : IUser
        where TSyncableData: ISyncableData<TUser>
          
    {
        Task UpdateTargetData(TSyncableData sourceData,TSyncableData targetData, DateTime now);
      
    }
}