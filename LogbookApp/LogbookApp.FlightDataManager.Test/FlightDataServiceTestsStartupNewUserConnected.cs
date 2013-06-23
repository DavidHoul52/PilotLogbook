using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary2.DataManagerTests;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataServiceTestsStartupNewUserConnected :  DataManagerTestBase<FlightData,User>
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            StartupAsNewUserConnected();
        }

    


        [TestMethod]
        public void ShouldCreateNewUser()
        {

            Assert.AreEqual(UserName, Target.Data.User.DisplayName);

        }

        [TestMethod]
        public void LookupsShouldBeAvailable()
        {
            Assert.IsNotNull(Target.Data.Lookups);

        }

        [TestMethod]
        public void LookupsAcTypesShouldBeAvailable()
        {



            Assert.IsNotNull(Target.Data.Lookups.AcTypes);

        }

        [TestMethod]
        public void LookupsAircraftShouldBeAvailable()
        {



            Assert.IsNotNull(Target.Data.Lookups.Aircraft);

        }

        [TestMethod]
        public void LookupsAirfieldsShouldBeAvailable()
        {



            Assert.IsNotNull(Target.Data.Lookups.Airfields);

        }

        [TestMethod]
        public void LookupCapacityShouldBeAvailable()
        {



            Assert.IsNotNull(Target.Data.InMemoryLookups.Capacities);

        }
    }
}
