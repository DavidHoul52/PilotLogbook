using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary2.DataManagerTests
{
    [TestClass]
    public class DataManagerTestAferStartup : DataManagerTestBase
    {

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

        }

        [TestMethod]
        public void UserNotConnectedOnStartsUpThenConnectsUserExistsOnlineShouldSync()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetUserDataExists(true, DateTime.Now);
            _target.Startup(_userName);
            _onlineDataService.SetUserDataExists(true,DateTime.Now.AddDays(-1));
            _internet.SetConnected(true);
            var entity = new TestEntity();
            _target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(_syncManager.UpdateTargetDataCalled);
        }

        [TestMethod]
        public void UserNotConnectedOnStartsUpThenConnectsUserExistsOnlineShouldSaveLocalData()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetUserDataExists(true, DateTime.Now);
            _target.Startup(_userName);
            _onlineDataService.SetUserDataExists(true, DateTime.Now.AddDays(-1));
            _internet.SetConnected(true);
            var entity = new TestEntity();
            _target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(_offlineDataService.LocalDataSaved);
        }

     

        [TestMethod]
        public void UserNotConnectedOnStartsUpThenConnectsUserNotExistsOnlineShouldCreateUseData()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetUserDataExists(true, DateTime.Now);
            _target.Startup(_userName);
            _onlineDataService.SetUserDataExists(false,null);
            _internet.SetConnected(true);
            var entity = new TestEntity();
            _target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(_onlineDataService.CreateUserDataCalled);
        }


        [TestMethod]
        public void UserNotConnectedOnStartsUpThenConnectsUserNotExistsOnlineShouldSyncData()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetUserDataExists(true, DateTime.Now);
            _target.Startup(_userName);
            _onlineDataService.SetUserDataExists(false, null);
            _internet.SetConnected(true);
            var entity = new TestEntity();
            _target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(_syncManager.UpdateTargetDataCalled);
        }

        [TestMethod]
        public void UserConnectedOnStartsUpThenDisconnectsUserNotExistsOfflineShouldCreateOffLineData()
        {
            _internet.SetConnected(true);
            _onlineDataService.SetUserDataExists(true, DateTime.Now.AddDays(-1));
            _offlineDataService.SetUserDataExists(false, null);
            _target.Startup(_userName);
            
            _internet.SetConnected(false);
            var entity = new TestEntity();
            _target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(_offlineDataService.CreateUserDataCalled);
        }

      
    }
}
