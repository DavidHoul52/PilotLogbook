using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary2.DataManagerTests
{
    [TestClass]
    public class DataManagerTestAferStartup : DataManagerTestBase<SyncableTestData, TestUser>
    {

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

        }

        [TestMethod]
        public void UserNotConnectedOnStartsUpThenConnectsUserExistsOnlineShouldSync()
        {
            Internet.SetConnected(false);
            OfflineDataService.SetUserDataExists(true, DateTime.Now);
            Target.Startup(UserName);
            _onlineDataService.SetUserDataExists(true,DateTime.Now.AddDays(-1));
            Internet.SetConnected(true);
            var entity = new TestEntity();
            Target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(SyncManager.UpdateTargetDataCalled);
        }

        [TestMethod]
        public void UserNotConnectedOnStartsUpThenConnectsUserExistsOnlineShouldSaveLocalData()
        {
            Internet.SetConnected(false);
            OfflineDataService.SetUserDataExists(true, DateTime.Now);
            Target.Startup(UserName);
            _onlineDataService.SetUserDataExists(true, DateTime.Now.AddDays(-1));
            Internet.SetConnected(true);
            var entity = new TestEntity();
            Target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(OfflineDataService.LocalDataSaved);
        }

     

        [TestMethod]
        public void UserNotConnectedOnStartsUpThenConnectsUserNotExistsOnlineShouldCreateUseData()
        {
            Internet.SetConnected(false);
            OfflineDataService.SetUserDataExists(true, DateTime.Now);
            Target.Startup(UserName);
            _onlineDataService.SetUserDataExists(false,null);
            Internet.SetConnected(true);
            var entity = new TestEntity();
            Target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(_onlineDataService.CreateUserDataCalled);
        }


        [TestMethod]
        public void UserNotConnectedOnStartsUpThenConnectsUserNotExistsOnlineShouldSyncData()
        {
            Internet.SetConnected(false);
            OfflineDataService.SetUserDataExists(true, DateTime.Now);
            Target.Startup(UserName);
            _onlineDataService.SetUserDataExists(false, null);
            Internet.SetConnected(true);
            var entity = new TestEntity();
            Target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(SyncManager.UpdateTargetDataCalled);
        }

        [TestMethod]
        public void UserConnectedOnStartsUpThenDisconnectsUserNotExistsOfflineShouldCreateOffLineData()
        {
            Internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(true, DateTime.Now.AddDays(-1));
            OfflineDataService.SetUserDataExists(false, null);
            Target.Startup(UserName);
            
            Internet.SetConnected(false);
            var entity = new TestEntity();
            Target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(OfflineDataService.CreateUserDataCalled);
        }

      
    }
}
