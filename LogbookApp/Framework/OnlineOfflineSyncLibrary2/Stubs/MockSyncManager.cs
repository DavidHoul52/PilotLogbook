using System;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary.Test
{
    public class MockSyncManager<TSyncableData, TUser> : ISyncManager<TSyncableData, TUser>
        where TUser : IUser
        where TSyncableData : ISyncableData<TUser>
        
    {
        public bool UpdateTargetDataCalled { get; private set; }



        public async Task UpdateTargetData(TSyncableData sourceData, TSyncableData targetData, DateTime now)
        {
            UpdateTargetDataCalled = true;
        }
    }
}