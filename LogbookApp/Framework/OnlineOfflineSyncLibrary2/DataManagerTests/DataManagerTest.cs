using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;
using LogbookApp.FlightDataManagerTest;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary2.DataManagerTests
{
    [TestClass]
    public class DataManagerTest
    {
        private TestUser _user;
        private SyncableTestData _data;
        private MockOnlineDataService _onlineDataService;
        private MockOfflineDataService _offlineDataService;
        private string _userName;
        private MockInternetTools _internet;
        private DataManager<TestUser> _target;
        private DateTime now;
        private MockSyncManager<SyncableTestData, TestUser> _syncManager;
            
            [TestInitialize]
        public void Setup()
        {
            _user = new TestUser();
            _data = new SyncableTestData();
            _userName = "David";
            _onlineDataService = new MockOnlineDataService(_data, _userName);
            _offlineDataService = new MockOfflineDataService(_data, _userName);
            _internet = new MockInternetTools();
            now = new DateTime(2013,5,1);                
             _syncManager = new MockSyncManager<SyncableTestData, TestUser>();
            _target = new DataManager<TestUser>(_data,_onlineDataService,_offlineDataService,_internet);
        }

        [TestMethod]
        public void UserStartsUpInternetConnectedTryLoadUserData()
        {
            _internet.SetConnected(true);
            _onlineDataService.SetupGetUser(_user);

            _target.Startup(_userName);
            
            Assert.IsTrue(_onlineDataService.LoadUserDataCalled);
        }


        [TestMethod]
        public void UserStartsUpInternetConnectedAndUserNotExistsCreateUserData()
        {
            _internet.SetConnected(true);
            _onlineDataService.SetupGetUser(null);

            _target.Startup(_userName);
            
            Assert.IsTrue(_onlineDataService.CreateUserDataCalled);
        }


        [TestMethod]
        public void UserStartsUpInternetConnectedAndUserExistsDontCreateUserData()
        {
            _internet.SetConnected(true);
            _onlineDataService.SetupGetUser(_user);

            _target.Startup(_userName);

            Assert.IsFalse(_onlineDataService.CreateUserDataCalled);
        }

        [TestMethod]
        public void UserStartsUpInternetNotConnectedTryLoadOfflineData()
        {
            _internet.SetConnected(false);
            _target.Startup(_userName);

            Assert.IsTrue(_offlineDataService.LoadUserDataCalled);
        }

        [TestMethod]
        public void UserStartsUpInternetNotConnectedAndUserNotExistsCreateUserData()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetupGetUser(null);

            _target.Startup(_userName);

            Assert.IsTrue(_offlineDataService.CreateUserDataCalled);
        }

        [TestMethod]
        public void UserStartsUpInternetNotConnectedAndUserExistsDontCreateUserData()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetupGetUser(_user);

            _target.Startup(_userName);

            Assert.IsFalse(_offlineDataService.CreateUserDataCalled);
        }


        [TestMethod]
        public void UserNotConnectedOnStartsUpTheConnectsUserExistsOnline()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetupGetUser(_user);

            _target.Startup(_userName);

            _internet.SetConnected(true);
            var entity = new TestEntity();
            _target.PerformDataUpdateAction(dataService =>  dataService.Insert(entity),entity, now);
            Assert.IsTrue(_syncManager.UpdateTargetDataCalled);
        }


    }
}
