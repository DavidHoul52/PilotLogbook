using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary.TestMocks
{
    public class MockSyncManager<TSyncableData,TOnlineDataService, TUser> : 
        ISyncManager<TSyncableData, TOnlineDataService,TUser>
        where TUser : IUser
        where TSyncableData : ISyncableData<TUser>
        where TOnlineDataService : IDataService<TSyncableData, TUser>
        
    {
        public bool UpdateTargetDataCalled { get; private set; }



    

        public async Task UpdateOnlineData(TOnlineDataService onlineDataService, TSyncableData sourceData, TSyncableData targetData,
            DateTime now)
        {
            UpdateTargetDataCalled = true;
        }
    }
}