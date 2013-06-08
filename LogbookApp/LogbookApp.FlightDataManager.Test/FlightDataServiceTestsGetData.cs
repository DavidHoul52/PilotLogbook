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
            Target.ConnectionStateChanged(true);
            Target.GetData();
        }


        [TestMethod]
        public void ShouldGetFlights()
        {
         
            Assert.IsNotNull(Target.FlightData.Flights);



        }



        [TestMethod]
        public void ShouldGetLookups()
        {
        
            Assert.IsNotNull(Target.FlightData.Lookups);



        }


        [TestMethod]
        public void ShouldGetUser()
        {
            
            Assert.IsNotNull(Target.FlightData.User);



        }
    }
}
