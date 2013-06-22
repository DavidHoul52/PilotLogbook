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
    public class DataManagerTestBase
    {
        protected SyncableTestData _data;
        protected MockOnlineDataService _onlineDataService;
        protected MockOfflineDataService _offlineDataService;
        protected string _userName;
        protected MockInternetTools _internet;
        protected DataManager<SyncableTestData, TestUser> _target;
        protected DateTime now;
        protected MockSyncManager _syncManager;

        public virtual void Setup()
        {
            
            _userName = "David";
            _onlineDataService = new MockOnlineDataService( _userName);
            _offlineDataService = new MockOfflineDataService( _userName);
            _internet = new MockInternetTools();
            now = new DateTime(2013, 5, 1);
            _syncManager = new MockSyncManager();
            _target = new DataManager<SyncableTestData, TestUser>
                (_onlineDataService, _offlineDataService, _internet,_syncManager);
        }
    }
}
