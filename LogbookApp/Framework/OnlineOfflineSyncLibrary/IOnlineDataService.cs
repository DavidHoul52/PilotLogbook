using System;
using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary
{
    public interface IOnlineDataService<TSyncableData,TUser> : IDataService<TSyncableData, TUser>
        where TUser : IUser
        where TSyncableData : ISyncableData<TUser>
    {
        bool IsConnected { get; set; }
        
    }
}
