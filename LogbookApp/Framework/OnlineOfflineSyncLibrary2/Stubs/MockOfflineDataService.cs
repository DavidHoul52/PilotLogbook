﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace OnlineOfflineSyncLibrary2.Stubs
{
    public class MockOfflineDataService : MockBaseDataService, IOfflineDataService<ISyncableData<TestUser>,TestUser>
    {
        public MockOfflineDataService(ISyncableData<TestUser> targetData, string userName) : base(targetData, userName)
        {
        }

        public async Task SaveLocalData(ISyncableData<TestUser> data)
        {
            
        }


        public async Task LoadUserData(string userName, ISyncableData<TestUser> data)
        {
            
        }

        public async Task<DataServiceState> GetServiceState(string userName)
        {
            
        }
    }
}
