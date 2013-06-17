using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace OnlineOfflineSyncLibrary2.Stubs
{
    public abstract class MockBaseDataService
    {
        protected readonly ISyncableData<TestUser> TargetData;
        protected readonly string UserName;

        protected MockBaseDataService(ISyncableData<TestUser> targetData, string userName)
        {
            TargetData = targetData;
            UserName = userName;
        }

        public async Task Update<T>(T item) where T : IEntity
        {

        }

        public async Task Insert<T>(T item) where T : IEntity
        {

        }

        public async Task Delete<T>(T item) where T : IEntity
        {

        }



        public async Task CreateUserData(string userName)
        {
            CreateUserDataCalled = true;
        }

      

        public void SetupGetUser(TestUser testUser)
        {
            TargetData.User = testUser;
        }

        public bool LoadUserDataCalled { get; set; }
        public bool CreateUserDataCalled { get; set; }

        public async Task<TestUser> GetUser(string userName)
        {
            return TargetData.User;
        }

        public async Task LoadUserData(string userName, ISyncableData<TestUser> data)
        {

        }

        public async Task<DataServiceState> GetServiceState(string userName)
        {

        }
    }
}
