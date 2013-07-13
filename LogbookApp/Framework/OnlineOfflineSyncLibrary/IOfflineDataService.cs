using System;
using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary
{
    public interface IOfflineDataService<TSyncableData,TUser> 
         : IDataUpdateActions, IDataService<TSyncableData, TUser>
         where TUser : IUser
        where TSyncableData : ISyncableData<TUser>
        
    {
        Task SaveLocalData(TSyncableData data);

      
    }
}
