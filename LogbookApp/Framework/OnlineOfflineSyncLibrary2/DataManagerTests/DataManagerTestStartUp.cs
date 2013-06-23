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
    public class DataManagerTestStartUp : DataManagerTestBase<SyncableTestData,TestUser>
    {
        
     
            
        [TestInitialize]
        public override void Setup()
        {
            base.Setup(); 
            
        }

        [TestMethod]
        public void UserStartsUpInternetConnectedTryLoadUserData()
        {
            Internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(true, DateTime.Now);
            Target.Startup(UserName);
            Assert.IsTrue(_onlineDataService.LoadUserDataCalled);
        }


        [TestMethod]
        public void UserStartsUpInternetConnectedAndUserNotExistsCreateUserData()
        {
            Internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(false, DateTime.Now);

            Target.Startup(UserName);
            
            Assert.IsTrue(_onlineDataService.CreateUserDataCalled);
        }


        [TestMethod]
        public void UserStartsUpInternetConnectedAndUserExistsDontCreateUserData()
        {
            Internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(true, DateTime.Now);
            

            Target.Startup(UserName);

            Assert.IsFalse(_onlineDataService.CreateUserDataCalled);
        }

        [TestMethod]
        public void UserStartsUpInternetNotConnectedTryLoadOfflineData()
        {
            Internet.SetConnected(false);
            OfflineDataService.SetUserDataExists(true, DateTime.Now);
            Target.Startup(UserName);

            Assert.IsTrue(OfflineDataService.LoadUserDataCalled);
        }

        [TestMethod]
        public void UserStartsUpInternetNotConnectedAndUserNotExistsCreateUserData()
        {
            Internet.SetConnected(false);
            OfflineDataService.SetUserDataExists(false, DateTime.Now);

            Target.Startup(UserName);

            Assert.IsTrue(OfflineDataService.CreateUserDataCalled);
        }

        [TestMethod]
        public void UserStartsUpInternetNotConnectedAndUserExistsDontCreateUserData()
        {
            Internet.SetConnected(false);
            OfflineDataService.SetUserDataExists(true, DateTime.Now); ;
            Target.Startup(UserName);
            Assert.IsFalse(OfflineDataService.CreateUserDataCalled);
        }

        [TestMethod]
        public void ShouldSaveLocalData()
        {
            Internet.SetConnected(false);

            OfflineDataService.SetUserDataExists(true, null);
            Target.Startup(UserName);
            var entity = new TestEntity();
            Target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(OfflineDataService.LocalDataSaved);
        }


        [TestMethod]
        public void ShouldSaveLocalDataWhenNewUser()
        {
            Internet.SetConnected(false);

            OfflineDataService.SetUserDataExists(false, null);
            Target.Startup(UserName);
            var entity = new TestEntity();
            Target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(OfflineDataService.LocalDataSaved);
        }


     


    }
}
