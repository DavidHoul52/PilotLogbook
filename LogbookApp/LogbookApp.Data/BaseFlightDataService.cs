using System;
using System.Threading.Tasks;
using BaseData;

namespace LogbookApp.Data
{
    public abstract class BaseFlightDataService
    {
        protected string DisplayName;

        protected BaseFlightDataService(string displayName)
        {
            DisplayName = displayName;
        }

        public async Task UpdateUserFromLocal(User localUser)
        {
            var user = await GetUser(DisplayName);
            if (user != null)
            {
                user.TimeStamp = localUser.TimeStamp;
                if (user.id == 0)
                    user.id = localUser.id;
                await UpdateUser(user);
            }
        }

        public virtual async Task UpdateUser(User user)
        {

            await UpdateUserInternal(user);
            LastUpdated = user.TimeStamp;
        }

        protected abstract Task UpdateUserInternal(User user);
        

        public abstract Task Update<T>(T item) where T : IEntity;

        public async Task<User> GetUser(string displayName)
        {
            var user = await GetUserInternal(displayName);
            if (user != null)
                LastUpdated = user.TimeStamp;
            else
                LastUpdated = null;
            return user;

        }

        protected abstract Task<User> GetUserInternal(string displayName);

        public DateTime? LastUpdated { get; protected set; }

       
    }
}