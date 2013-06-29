using System.Linq;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace LogbookApp.FlightDataManagerTest.SyncManagerTests
{
    [TestClass]
    public class SyncManagerTestsUser : SyncManagerTestsBase<FlightsSyncManager<MockOnlineFlightData>, FlightData, User,
        MockOnlineFlightData>
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        //[TestMethod]
        //public void ShouldUpdateOnlineUserIdIfNotExists()
        //{

        //    LocalData.User = new User { id = 53 };
        //    Target.UpdateOnlineData(OnlineDataService,LocalData, OnlineData, NewerTimeStamp);
        //    Assert.AreEqual(53, OnlineData.User.id);
        //}

        //[TestMethod]
        //public void ShouldNotUpdateOnlineUserId()
        //{
        //    OnlineData.User= new User {id=1};
        //    LocalData.User = new User {id = 0};
        //    Target.UpdateOnlineData(OnlineDataService,LocalData, OnlineData, NewerTimeStamp);
        //    Assert.AreEqual(1, OnlineData.User.id);
        //}
    }
}
