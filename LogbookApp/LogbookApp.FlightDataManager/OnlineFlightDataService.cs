using System;
using System.Threading.Tasks;
using BaseData;
using LogbookApp.Data;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.FlightDataManagement
{
    public class OnlineFlightDataService : DataService<FlightData>, IOnlineDataService<FlightData,User>
    {
        private MobileF

        public Task<User> GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(T item) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public Task Insert<T>(T item) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public Task Delete<T>(T item) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetUserDataExists(string userName)
        {
            throw new NotImplementedException();
        }

        public bool IsConnected { get; set; }
    }
}
