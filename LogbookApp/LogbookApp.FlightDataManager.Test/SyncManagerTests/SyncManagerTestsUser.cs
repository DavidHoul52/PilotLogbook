using System.Linq;
using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace LogbookApp.FlightDataManagerTest.SyncManagerTests
{
    [TestClass]
    public class SyncManagerTestsUser : SyncManagerTestsBase<FlightData, User, MockOnlineFlightData>
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void ShouldUpdateOnlineUserIdIfNotExists()
        {

            SourceData.User = new User { id = 53 };
            Target.UpdateTargetData(SourceData, TargetData, NewerTimeStamp);
            Assert.AreEqual(53, TargetData.User.id);
        }

        [TestMethod]
        public void ShouldNotUpdateOnlineUserId()
        {
            TargetData.User= new User {id=1};
            SourceData.User = new User {id = 0};
            Target.UpdateTargetData(SourceData, TargetData, NewerTimeStamp);
            Assert.AreEqual(1, TargetData.User.id);
        }
    }
}
