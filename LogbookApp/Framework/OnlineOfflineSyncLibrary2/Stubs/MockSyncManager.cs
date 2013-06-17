using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary.Test
{
    public class MockSyncManager<TSyncableData, TUser> : ISyncManager<TSyncableData,TUser>
        where TSyncableData : ISyncableData<TUser>
        where TUser: IUser
    {
        public bool UpdateTargetDataCalled { get; private set; }

        public async Task UpdateTargetData(TSyncableData sourceData, DateTime now)
        {
            UpdateTargetDataCalled = true;
        }
    }
}