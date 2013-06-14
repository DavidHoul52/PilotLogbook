using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace OnlineOfflineSyncLibrary2
{
    public class TestSyncManager : SyncManager<SyncableTestData, MockDataService, TestUser>
    {
        public TestSyncManager(MockDataService onlineDataService) : base(onlineDataService)
        {
        }

        

        public async override Task UpdateTargetData(SyncableTestData sourceFlightData, DateTime now)
        {
            
        }
    }
}
