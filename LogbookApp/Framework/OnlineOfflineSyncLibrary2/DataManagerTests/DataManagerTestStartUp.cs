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
    public class DataManagerTestStartUp : DataManagerTestBase
    {
        
     
            
        [TestInitialize]
        public override void Setup()
        {
            base.Setup(); 
            
        }

        [TestMethod]
        public void UserStartsUpInternetConnectedTryLoadUserData()
        {
            _internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(true, DateTime.Now);
            _target.Startup(_userName);
            Assert.IsTrue(_onlineDataService.LoadUserDataCalled);
        }


        [TestMethod]
        public void UserStartsUpInternetConnectedAndUserNotExistsCreateUserData()
        {
            _internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(false, DateTime.Now);

            _target.Startup(_userName);
            
            Assert.IsTrue(_onlineDataService.CreateUserDataCalled);
        }


        [TestMethod]
        public void UserStartsUpInternetConnectedAndUserExistsDontCreateUserData()
        {
            _internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(true, DateTime.Now);
            

            _target.Startup(_userName);

            Assert.IsFalse(_onlineDataService.CreateUserDataCalled);
        }

        [TestMethod]
        public void UserStartsUpInternetNotConnectedTryLoadOfflineData()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetUserDataExists(true, DateTime.Now);
            _target.Startup(_userName);

            Assert.IsTrue(_offlineDataService.LoadUserDataCalled);
        }

        [TestMethod]
        public void UserStartsUpInternetNotConnectedAndUserNotExistsCreateUserData()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetUserDataExists(false, DateTime.Now);

            _target.Startup(_userName);

            Assert.IsTrue(_offlineDataService.CreateUserDataCalled);
        }

        [TestMethod]
        public void UserStartsUpInternetNotConnectedAndUserExistsDontCreateUserData()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetUserDataExists(true, DateTime.Now); ;
            _target.Startup(_userName);
            Assert.IsFalse(_offlineDataService.CreateUserDataCalled);
        }

        [TestMethod]
        public void ShouldSaveLocalData()
        {
            _internet.SetConnected(false);

            _offlineDataService.SetUserDataExists(false, null);
            _target.Startup(_userName);
            var entity = new TestEntity();
            _target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(_offlineDataService.LocalDataSaved);
        }


     


    }
}
