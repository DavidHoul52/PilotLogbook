using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;
using LogbookApp.FlightDataManagerTest;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace OnlineOfflineSyncLibrary2.DataManagerTests
{
    [TestClass]
    public class DataManagerTest
    {
        private TestUser _user;
        private SyncableTestData _data;
        private MockDataService _onlineDataService;
        private MockDataService _offlineDataService;
        private string _userName;
        private MockInternetTools _internet;
        private DataManager<TestUser> _target;

        [TestInitialize]
        public void Setup()
        {
            _user = new TestUser();
            _data = new SyncableTestData();
            _userName = "David";
            _onlineDataService = new MockDataService(DataType.OnLine, _data, _userName);
            _offlineDataService = new MockDataService(DataType.OffLine, _data, _userName);
            _internet = new MockInternetTools();
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


    }
}
