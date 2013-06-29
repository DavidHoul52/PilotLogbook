using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary.TestMocks;

namespace OnlineOfflineSyncLibrary2
{
    public class TestSyncManager<TSyncableData,TOnlineDataService, TUser> : SyncManager<TSyncableData,
        TOnlineDataService, TUser>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>, new()
        where TOnlineDataService : MockOnlineDataService<TSyncableData, TUser> 
    {
     




        public async override Task UpdateOnlineData(TOnlineDataService onlineDataService, TSyncableData sourceData, TSyncableData targetData,
            DateTime now)
        {
          
        }
    }
}
