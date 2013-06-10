using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataServiceTestsStartupNewUserConnected :  FlightDataServiceTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            MockInternetTools.SetConnected(true);
            OnlineDataService.SetExists(false);
            TestLocalStorage.SetExists(false);
            Target.StartUp("jack");
        }


        [TestMethod]
        public void FlightDataServiceShouldBeOnline()
        {

            Assert.AreEqual(DataType.OnLine, Target.DataService.DataType);

        }

        [TestMethod]
        public void ShouldCreateNewUser()
        {

            Assert.AreEqual("jack", Target.FlightData.User.DisplayName);

        }

        [TestMethod]
        public void LookupsShouldBeAvailable()
        {
            Assert.IsNotNull(Target.FlightData.Lookups);

        }

        [TestMethod]
        public void LookupsAcTypesShouldBeAvailable()
        {



            Assert.IsNotNull(Target.FlightData.Lookups.AcTypes);

        }

        [TestMethod]
        public void LookupsAircraftShouldBeAvailable()
        {



            Assert.IsNotNull(Target.FlightData.Lookups.Aircraft);

        }

        [TestMethod]
        public void LookupsAirfieldsShouldBeAvailable()
        {



            Assert.IsNotNull(Target.FlightData.Lookups.Airfields);

        }

        [TestMethod]
        public void LookupCapacityShouldBeAvailable()
        {



            Assert.IsNotNull(Target.FlightData.InMemoryLookups.Capacities);

        }
    }
}
