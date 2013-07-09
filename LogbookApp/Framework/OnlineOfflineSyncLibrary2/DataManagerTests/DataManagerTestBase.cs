using System;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary.TestMocks;

namespace OnlineOfflineSyncLibrary2.DataManagerTests
{
    public class DataManagerTestBase<TDataManager,TSyncableData, TUser,TOnlineDataService, TSyncManager>
        where TDataManager :
        DataManager<TSyncableData, TUser, TOnlineDataService,
         IOfflineDataService<TSyncableData,TUser>,TSyncManager>,new()
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>, new()
        where TOnlineDataService : MockOnlineDataService<TSyncableData, TUser>, new()
        where TSyncManager : MockSyncManager<TSyncableData, TOnlineDataService,
            TUser>,new()
    {
        protected SyncableTestData Data;
        protected TOnlineDataService _onlineDataService;
        protected MockOfflineDataService<TSyncableData, TUser> OfflineDataService;
        protected string UserName;
        protected MockInternetTools Internet;
        protected TDataManager Target;
        protected DateTime now;
        protected TSyncManager SyncManager;

        public virtual void Setup()
        {
            
            UserName = "David";
            _onlineDataService = new TOnlineDataService();
            OfflineDataService = new MockOfflineDataService<TSyncableData, TUser>(UserName);
            Internet = new MockInternetTools();
            now = new DateTime(2013, 5, 1);
            SyncManager = new TSyncManager();
            Target = new TDataManager {};
            Target.SetConstructorParams(_onlineDataService, OfflineDataService, Internet,SyncManager);
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
