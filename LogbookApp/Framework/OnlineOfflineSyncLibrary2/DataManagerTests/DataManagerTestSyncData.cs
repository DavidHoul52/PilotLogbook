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
    public class DataManagerTestSyncData : DataManagerTestBase
    {

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

        }

        [TestMethod]
        public void UserNotConnectedOnStartsUpThenConnectsUserExistsOnline()
        {
            _internet.SetConnected(false);
            _offlineDataService.SetUserDataExists(true, DateTime.Now);
            _target.Startup(_userName);
            _internet.SetConnected(true);
            var entity = new TestEntity();
            _target.PerformDataUpdateAction(dataService => dataService.Insert(entity), entity, now);
            Assert.IsTrue(_syncManager.UpdateTargetDataCalled);
        }
    }
}
