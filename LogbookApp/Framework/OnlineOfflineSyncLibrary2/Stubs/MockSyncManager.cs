using System;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary.Test
{
    public class MockSyncManager<TSyncableData,TOnlineDataService, TUser> : 
        ISyncManager<TSyncableData, TOnlineDataService,TUser>
        where TUser : IUser
        where TSyncableData : ISyncableData<TUser>
        where TOnlineDataService : IDataService<TSyncableData, TUser>
        
    {
        public bool UpdateTargetDataCalled { get; private set; }



    

        public async Task UpdateTargetData(TOnlineDataService onlineDataService, TSyncableData sourceData, TSyncableData targetData,
            DateTime now)
        {
            UpdateTargetDataCalled = true;
        }
    }
}