using System;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary.Test
{
    public class MockSyncManager : ISyncManager<SyncableTestData, TestUser>
        
    {
        public bool UpdateTargetDataCalled { get; private set; }



        public async Task UpdateTargetData(SyncableTestData sourceData, SyncableTestData targetData, DateTime now)
        {
            UpdateTargetDataCalled = true;
        }
    }
}