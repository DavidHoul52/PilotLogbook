using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data.Validation;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.Data.Test
{
    [TestClass]
    public class FlightValidationTests
    {
        private Flight flight;

        [TestInitialize]
        public void Setup()
        {
            flight= new Flight();
        }

        [TestMethod]
        public void ShouldReturnValidFalse()
        {
            Assert.IsFalse(flight.Valid());
        }
    }
}
