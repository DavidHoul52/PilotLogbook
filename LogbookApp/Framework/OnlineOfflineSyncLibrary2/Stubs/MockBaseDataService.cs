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
        protected SyncableTestData TargetData;
        protected readonly string UserName;
        private bool _userDataExists;

        protected MockBaseDataService(string userName)
        {
            
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

        public bool LoadUserDataCalled { get; set; }
        public bool CreateUserDataCalled { get; set; }

        public async Task<TestUser> GetUser(string userName)
        {
            return TargetData.User;
        }

        public async Task<SyncableTestData> LoadUserData(string userName)
        {
            LoadUserDataCalled = true;
            return TargetData;
        }

        public async Task<SyncableTestData> CreateUserData(string userName, DateTime? timeStamp)
        {
            CreateUserDataCalled = true;
            return InternalCreateUserData(timeStamp);
        }


       public async Task<bool> GetUserDataExists(string userName)
       {
           return TargetData != null;
       }

        public void SetUserDataExists(bool exists, DateTime? timeStamp)
        {
            if (exists)
               TargetData = InternalCreateUserData(timeStamp);
            else
            {
                TargetData = null;
            }
        }

        private static SyncableTestData InternalCreateUserData(DateTime? timeStamp)
        {
            return new SyncableTestData { User = new TestUser { TimeStamp = timeStamp } };
        }
    }
}
