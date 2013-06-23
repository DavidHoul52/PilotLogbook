using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace OnlineOfflineSyncLibrary2.Stubs
{
    public class MockOfflineDataService<TSyncableData, TUser> : MockBaseDataService<TSyncableData, TUser>,
        IOfflineDataService<TSyncableData, TUser>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>, new()
    {
        public MockOfflineDataService(string userName)
            : base( userName)
        {
        }

      


        public DateTime? LastUpdated { get; set; }
        public bool LocalDataSaved { get; set; }




        public async Task SaveLocalData(TSyncableData data)
        {
            LocalDataSaved = true;
        }
    }
}
