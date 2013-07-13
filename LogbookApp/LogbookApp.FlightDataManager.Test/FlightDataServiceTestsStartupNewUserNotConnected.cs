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
    public class FlightDataServiceTestsStartupNewUserNotConnected : FlightDataManagerTestBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            StartupAsNotConnectedNewUser(TestDates.NowLess1);
        }

      

    

        [TestMethod]
        public void IfOfflineAndNoLocalThenCreateNewUser()
        {

            Assert.AreEqual(UserName, Target.Data.User.DisplayName);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsShouldBeAvailable()
        {


        
            Assert.IsNotNull(Target.Data.Lookups);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsAcTypesShouldBeAvailable()
        {


       
            Assert.IsNotNull(Target.Data.Lookups.AcTypes);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsAircraftShouldBeAvailable()
        {



            Assert.IsNotNull(Target.Data.Lookups.Aircraft);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsAirfieldsShouldBeAvailable()
        {



            Assert.IsNotNull(Target.Data.Lookups.Airfields);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupCapacityShouldBeAvailable()
        {



            Assert.IsNotNull(Target.Data.InMemoryLookups.Capacities);

        }
    }
}
