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

        Task<TSyncableData> LoadUserData(string userName);

        Task<TSyncableData> CreateUserData(string userName, DateTime? timeStamp);


        Task<bool> GetUserDataExists(string userName);

        bool Loaded { get;  }

       
    }
}