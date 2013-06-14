using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary
{
    public interface ISyncManager<TSyncableData,TUser>
        where TSyncableData: ISyncableData<TUser>
            where TUser: IUser
    {
        Task UpdateTargetData(TSyncableData sourceData, DateTime now);
      
    }
}