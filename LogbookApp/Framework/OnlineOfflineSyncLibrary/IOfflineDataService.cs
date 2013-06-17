using System;
using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary
{
    public interface IOfflineDataService<TSyncableData,TUser> : IDataService<TSyncableData, TUser>
         where TUser : IUser
        where TSyncableData : ISyncableData<TUser>
    {
        Task SaveLocalData(ISyncableData<TUser> data);
     
    }
}
