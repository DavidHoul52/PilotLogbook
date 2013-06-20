using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace OnlineOfflineSyncLibrary2
{
    public class TestSyncManager : SyncManager<SyncableTestData, MockOnlineDataService, TestUser>
    {
        public TestSyncManager(MockOnlineDataService onlineDataService) : base(onlineDataService)
        {
        }

        

        

        public async override Task UpdateTargetData(SyncableTestData sourceData, SyncableTestData targetData, DateTime now)
        {
            
        }
    }
}
