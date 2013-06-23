using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.FlightDataManagerTest;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary2.DataManagerTests
{
    public class DataManagerTestBase<TSyncableData, TUser>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>, new()
    {
        protected SyncableTestData Data;
        protected MockOnlineDataService<TSyncableData, TUser> _onlineDataService;
        protected MockOfflineDataService<TSyncableData, TUser> OfflineDataService;
        protected string UserName;
        protected MockInternetTools Internet;
        protected DataManager<TSyncableData, TUser, MockOnlineDataService<TSyncableData, TUser>,
            MockOfflineDataService<TSyncableData, TUser>> Target;
        protected DateTime now;
        protected MockSyncManager<TSyncableData, TUser> SyncManager;

        public virtual void Setup()
        {
            
            UserName = "David";
            _onlineDataService = new MockOnlineDataService<TSyncableData, TUser>(UserName);
            OfflineDataService = new MockOfflineDataService<TSyncableData, TUser>(UserName);
            Internet = new MockInternetTools();
            now = new DateTime(2013, 5, 1);
            SyncManager = new MockSyncManager<TSyncableData, TUser>();
            Target = new DataManager<TSyncableData, TUser,
                MockOnlineDataService<TSyncableData, TUser>,
            MockOfflineDataService<TSyncableData, TUser>>
                (_onlineDataService, OfflineDataService, Internet,SyncManager);
        }


        protected void StartupAsConnected()
        {
            Internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(true, DateTime.Now);
            Target.Startup(UserName);
        }

        protected void StartupAsNewUserConnected()
        {
            Internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(false, null);
            Target.Startup(UserName);
        }

        protected void StartupAsNotConnectedNewUser()
        {
            Internet.SetConnected(false);
            _onlineDataService.SetUserDataExists(false, DateTime.Now);
            Target.Startup(UserName);
        }
    }
}
