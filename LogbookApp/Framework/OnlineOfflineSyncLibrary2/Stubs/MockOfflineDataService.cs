using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace OnlineOfflineSyncLibrary2.Stubs
{
    public class MockOfflineDataService : MockBaseDataService, IOfflineDataService<SyncableTestData, TestUser>
    {
        public MockOfflineDataService(string userName)
            : base( userName)
        {
        }

      


        public DateTime? LastUpdated { get; set; }
        public bool LocalDataSaved { get; set; }


        public async Task SaveLocalData(ISyncableData<TestUser> data)
        {
            LocalDataSaved = true;
        }
    }
}
