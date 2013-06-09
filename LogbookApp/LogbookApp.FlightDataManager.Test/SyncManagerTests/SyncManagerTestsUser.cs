using System.Linq;
using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest.SyncManagerTests
{
    [TestClass]
    public class SyncManagerTestsUser : SyncManagerTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void ShouldUpdateOnlineUserIdIfNotExists()
        {

            _sourceFlightData.User = new User { id = 53 };
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.AreEqual(53, _targetFlightData.User.id);
        }

        [TestMethod]
        public void ShouldNotUpdateOnlineUserId()
        {
            _targetFlightData.User= new User {id=1};
            _sourceFlightData.User = new User {id = 0};
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.AreEqual(1, _targetFlightData.User.id);
        }
    }
}
