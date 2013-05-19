using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataServiceTestsGetData : FlightDataServiceTestsBase
    {

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }


        [TestMethod]
        public void ShouldGetFlights()
        {
            OnlineDataService.SetAvailable(true);
            Target.GetData(Now);
            Assert.IsNotNull(Target.FlightData.Flights);



        }



        [TestMethod]
        public void ShouldGetLookups()
        {
            OnlineDataService.SetAvailable(true);
            Target.GetData(Now);
            Assert.IsNotNull(Target.FlightData.Lookups);



        }


        [TestMethod]
        public void ShouldGetUser()
        {
            OnlineDataService.SetAvailable(true);
            Target.GetData(Now);
            Assert.IsNotNull(Target.FlightData.User);



        }
    }
}
