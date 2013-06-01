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
            Assert.IsFalse(flight.ValidationResult().Valid);
        }


        [TestMethod]
        public void ShouldReturnValidFalseIfNoAircraftSelected()
        {

            Assert.IsFalse(flight.ValidationResult().Valid);
        }

        
          [TestMethod]
        public void ShouldReturnMessageifNoAircraftSelected()
        {
           
            Assert.AreEqual("Please select an aircraft (or add a new one)",flight.ValidationResult().Message);
        }


          [TestMethod]
          public void ShouldReturnMessageifNoCapacitySelected()
          {
              flight.Aircraft = new Aircraft();
              Assert.AreEqual("Please select the Captain's capacity", flight.ValidationResult().Message);
          }

          [TestMethod]
          public void ShouldReturnMessageifNoAirfieldFromSelected()
          {
              flight.Aircraft= new Aircraft();
              flight.Capacity= new Capacity();
              Assert.AreEqual("Please select a departure airfield (or add a new one)", flight.ValidationResult().Message);
          }



          [TestMethod]
          public void ShouldReturnMessageifNoAirfieldToSelected()
          {
              flight.Aircraft = new Aircraft();
              flight.Capacity = new Capacity();
              flight.From= new Airfield();
              Assert.AreEqual("Please select an arrival airfield (or add a new one)", flight.ValidationResult().Message);
          }

          [TestMethod]
          public void ShouldReturnMessageifDurationInvalid()
          {
              flight.Aircraft = new Aircraft();
              flight.Capacity = new Capacity();
              flight.From = new Airfield();
              flight.To= new Airfield();
              Assert.AreEqual("Please select an arrival time later than the departure time", flight.ValidationResult().Message);
          }


          [TestMethod]
          public void ShouldReturnValid()
          {
              flight.Aircraft = new Aircraft();
              flight.Capacity = new Capacity();
              flight.From = new Airfield();
              flight.To = new Airfield();
              flight.Depart= new DateTime(2013,1,1);
              flight.Arrival = new DateTime(2013, 1, 2);
              Assert.IsTrue(flight.ValidationResult().Valid);
          }
    }
}
