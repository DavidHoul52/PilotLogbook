using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.Data.Test
{
    [TestClass]
    public class FlightFactoryTests
    {
        private FlightFactory _target;

        [TestInitialize]
        public void Setup()
        {
            _target= new FlightFactory();
        }

        [TestMethod]
        public void ShouldCreateFlight()
        {
            var result=_target.CreateFlight(new FlightData(),TestDates.Now);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void ShouldAddUserId()
        {
            var flight = _target.CreateFlight(new FlightData { User = new User { id=54}},TestDates.Now);
            Assert.AreEqual(54,flight.UserId);

        }


        [TestMethod]
        public void ShouldAddLookups()
        {
            var lookups = new Lookups();
            var flight = _target.CreateFlight(new FlightData { Lookups = lookups }, TestDates.Now);
            Assert.IsNotNull(flight.Lookups);

        }


        [TestMethod]
        public void ShouldAddCapacities()
        {
            var inMemoryLookups = new InMemoryLookups {};
            var flight = _target.CreateFlight(new FlightData { InMemoryLookups = inMemoryLookups }, TestDates.Now);
            Assert.IsNotNull(flight.Capacities);

        }

        [TestMethod]
        public void ShouldBeIsNew()
        {

            var flight = _target.CreateFlight(new FlightData { }, TestDates.Now);
            Assert.IsTrue(flight.IsNew);

        }

     

    }
}
