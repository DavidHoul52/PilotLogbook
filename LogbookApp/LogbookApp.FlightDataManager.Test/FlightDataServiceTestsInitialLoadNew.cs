using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataServiceTestsInitialLoadNew : FlightDataServiceTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenCreateNewUser()
        {

            
            MockInternetTools.SetConnected(false);
            Target.StartUp("jack");
            Assert.AreEqual("jack",Target.FlightData.User.DisplayName);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsShouldBeAvailable()
        {


            MockInternetTools.SetConnected(false);
            Target.StartUp("jack");
            Assert.IsNotNull(Target.FlightData.Lookups);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsAcTypesShouldBeAvailable()
        {


            MockInternetTools.SetConnected(false);
            Target.StartUp("jack");
            Assert.IsNotNull(Target.FlightData.Lookups.AcTypes);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsAircraftShouldBeAvailable()
        {


            MockInternetTools.SetConnected(false);
            Target.StartUp("jack");
            Assert.IsNotNull(Target.FlightData.Lookups.Aircraft);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsAirfieldsShouldBeAvailable()
        {


            MockInternetTools.SetConnected(false);
            Target.StartUp("jack");
            Assert.IsNotNull(Target.FlightData.Lookups.Airfields);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupCapacityShouldBeAvailable()
        {


            MockInternetTools.SetConnected(false);
            Target.StartUp("jack");
            Assert.IsNotNull(Target.FlightData.Lookups.Capacity);

        }
    }
}
