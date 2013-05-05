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
            OnlineTestData.SetAvailable(true);
            Target.GetData(Now);
            Assert.IsNotNull(Target.Flights);



        }



        [TestMethod]
        public void ShouldGetLookups()
        {
            OnlineTestData.SetAvailable(true);
            Target.GetData(Now);
            Assert.IsNotNull(Target.Lookups);



        }


        [TestMethod]
        public void ShouldGetUser()
        {
            OnlineTestData.SetAvailable(true);
            Target.GetData(Now);
            Assert.IsNotNull(Target.User);



        }
    }
}
