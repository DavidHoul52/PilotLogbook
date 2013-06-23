using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary
{
    public abstract class DataService<TSyncableData, TUser>
        where TUser : IUser
        where TSyncableData : ISyncableData<TUser>
    {
        public async Task<TSyncableData> LoadUserData(string userName)
        {
            Loaded = true;
            return await InternalLoadUserData(userName);
        }

        protected abstract Task<TSyncableData> InternalLoadUserData(string userName);
        

        public async Task<TSyncableData> CreateUserData(string userName, DateTime? timeStamp)
         {
             Loaded = true;
             await InternalCreateUserData(userName);
             return await InternalLoadUserData(userName);
         }

        protected abstract Task InternalCreateUserData(string userName);

        public bool Loaded { get; private set; }
    }
}