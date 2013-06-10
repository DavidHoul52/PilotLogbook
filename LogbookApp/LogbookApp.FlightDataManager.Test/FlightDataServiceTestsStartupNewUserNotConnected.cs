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
    public class FlightDataServiceTestsStartupNewUserNotConnected : FlightDataServiceTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            base.SetupDataType(DataType.OffLine);
            
            //TestLocalStorage.SetExists(false);
            Target.StartUp(DisplayName);
        }

        [TestMethod]
        public void FlightDataServiceShouldBeOffline()
        {

            Assert.AreEqual(DataType.OffLine, Target.DataService.DataType);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenCreateNewUser()
        {

            Assert.AreEqual(DisplayName, Target.FlightData.User.DisplayName);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsShouldBeAvailable()
        {


        
            Assert.IsNotNull(Target.FlightData.Lookups);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsAcTypesShouldBeAvailable()
        {


       
            Assert.IsNotNull(Target.FlightData.Lookups.AcTypes);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsAircraftShouldBeAvailable()
        {


          
            Assert.IsNotNull(Target.FlightData.Lookups.Aircraft);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupsAirfieldsShouldBeAvailable()
        {


         
            Assert.IsNotNull(Target.FlightData.Lookups.Airfields);

        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenLookupCapacityShouldBeAvailable()
        {


        
            Assert.IsNotNull(Target.FlightData.InMemoryLookups.Capacities);

        }
    }
}
