using System;
using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary
{
   


    public interface IDataService<TSyncableData,TUser>
        where TUser: IUser
        where TSyncableData : ISyncableData<TUser>
    {

        Task<TUser> GetUser(string userName);

        Task Update<T>(T item)
        where T : IEntity;

        Task Insert<T>(T item)
            where T : IEntity;

        Task Delete<T>(T item)
              where T : IEntity;

        Task<bool> LoadUserData(string userName, TSyncableData data);

        Task CreateUserData(string userName);

        Task<DataServiceState> GetServiceState(string userName);

    }
}