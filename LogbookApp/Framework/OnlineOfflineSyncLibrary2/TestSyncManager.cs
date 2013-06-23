using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace OnlineOfflineSyncLibrary2
{
    public class TestSyncManager<TSyncableData, TUser> : SyncManager<TSyncableData,
        MockOnlineDataService<TSyncableData, TUser>, TUser>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>, new()
    {
        public TestSyncManager(MockOnlineDataService<TSyncableData, TUser> onlineDataService) :
            base(onlineDataService)
        {
        }





        public async override Task UpdateTargetData(TSyncableData sourceData,
            TSyncableData targetData, DateTime now)
        {
            
        }
    }
}
