using System;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary.TestMocks;

namespace OnlineOfflineSyncLibrary2.DataManagerTests
{
    public class DataManagerTestBase<TDataManager,TSyncableData, TUser,TOnlineDataService,
        TOfflineDataService,
        TSyncManager,
        TDataUpdateActions>
        where TDataManager :
        DataManager<TSyncableData, TUser, TOnlineDataService,TOfflineDataService,
          TSyncManager, TDataUpdateActions>, new()
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>, new()
        where TOnlineDataService : MockOnlineDataService<TSyncableData, TUser>,TDataUpdateActions, new()
        where TOfflineDataService : MockOfflineDataService<TSyncableData, TUser>, TDataUpdateActions, new()
        where TSyncManager : MockSyncManager<TSyncableData, TOnlineDataService,
            TUser>,new()
        where TDataUpdateActions: IDataUpdateActions
    {
        protected SyncableTestData Data;
        protected TOnlineDataService _onlineDataService;
        protected TOfflineDataService OfflineDataService;
        protected string UserName;
        protected MockInternetTools Internet;
        protected TDataManager Target;
        protected DateTime now;
        protected TSyncManager SyncManager;

        public virtual void Setup()
        {
            
            UserName = "David";
            _onlineDataService = new TOnlineDataService();
            OfflineDataService = new TOfflineDataService();
            OfflineDataService.UserName = UserName;
            Internet = new MockInternetTools();
            now = new DateTime(2013, 5, 1);
            SyncManager = new TSyncManager();
            Target = new TDataManager {};
            Target.SetConstructorParams(_onlineDataService, OfflineDataService, Internet,SyncManager);
        }


        protected void StartupAsConnected(DateTime? onLineLastUpdated)
        {
            Internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(true, onLineLastUpdated);
            Target.Startup(UserName);
        }

        protected void StartupAsNewUserConnected()
        {
            Internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(false, null);
            Target.Startup(UserName);
        }

        protected void StartupAsNotConnectedNewUser(DateTime? onLineLastUpdated)
        {
            Internet.SetConnected(false);
            _onlineDataService.SetUserDataExists(false, onLineLastUpdated);
            Target.Startup(UserName);
            
        }

        protected void StartupAsOfflineExistingUser(DateTime? offLineLastUpdated, DateTime? onLineLastUpdated)
        {
            Internet.SetConnected(false);
            OfflineDataService.SetUserDataExists(true, offLineLastUpdated);
            _onlineDataService.SetUserDataExists(true, onLineLastUpdated);
            Target.Startup(UserName);
        }
    }
}
