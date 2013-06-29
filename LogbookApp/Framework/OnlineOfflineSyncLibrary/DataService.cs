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
            var result= await InternalLoadUserData(userName);
            User = result.User;
            return result;
        }

        protected abstract Task<TSyncableData> InternalLoadUserData(string userName);
        

        public async Task<TSyncableData> CreateUserData(string userName, DateTime? timeStamp)
         {
             Loaded = true;
             await InternalCreateUserData(userName);
             var result= await InternalLoadUserData(userName);
            User = result.User;
            return result;
         }

        protected abstract Task InternalCreateUserData(string userName);

        public bool Loaded { get; private set; }

        public async Task UpdateUserTimeStamp(DateTime? timeStamp)
        {
            User.TimeStamp = timeStamp;
            await InternalUpdateUserTimeStamp(timeStamp);
        }

        protected abstract Task InternalUpdateUserTimeStamp(DateTime? timeStamp);

        public TUser User { get; set; }
    }
}