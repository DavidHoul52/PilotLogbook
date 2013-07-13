using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.Data.Test
{
    [TestClass]
    public class FlightTests
    {
        private Flight _target;
        private FlightData flightData;
        
       

        [TestInitialize]
        public void Setup()
        {
            
            flightData = new FlightData();
            _target = new FlightFactory().CreateFlight(flightData);

        }

        [TestMethod]
        public void ShouldPopulateLookupsAircraft()
        {
            _target.AircraftId = 22;
            _target.PopulateLookups(new Lookups { Aircraft = new ObservableCollection<Aircraft> 
            { new Aircraft {id = 22}}},
                flightData.InMemoryLookups);
            Assert.IsNotNull(_target.Aircraft);
        }

        [TestMethod]
        public void ShouldNotPopulateLookupsAircraft()
        {
            _target.AircraftId = 23;
            _target.PopulateLookups(new Lookups { Aircraft = new ObservableCollection<Aircraft> { new Aircraft { id = 22 } } },
               flightData.InMemoryLookups);
            Assert.IsNull(_target.Aircraft);
        }

        [TestMethod]
        public void ShouldPopulateLookupsCapacity()
        {
            _target.CapacityId = 1;
            _target.PopulateLookups(new Lookups(), flightData.InMemoryLookups);
            Assert.IsNotNull(_target.Capacity);
        }

        [TestMethod]
        public void ShouldNotPopulateLookupsCapacity()
        {
            _target.CapacityId = -1;
            _target.PopulateLookups(new Lookups(), flightData.InMemoryLookups);
            Assert.IsNull(_target.Capacity);
        }

        [TestMethod]
        public void ShouldPopulateLookupsFrom()
        {
            _target.AirfieldFromId = 11;
            _target.PopulateLookups(new Lookups { Airfields = new ObservableCollection<Airfield> { new Airfield { id = 11 } } },
                flightData.InMemoryLookups);
            Assert.IsNotNull(_target.From);
        }

        [TestMethod]
        public void ShouldNotPopulateLookupsFrom()
        {
            _target.AirfieldFromId = 0;
            _target.PopulateLookups(new Lookups { Airfields = new ObservableCollection<Airfield> { new Airfield { id = 11 } } },
                flightData.InMemoryLookups);
            Assert.IsNull(_target.From);
        }

        [TestMethod]
        public void ShouldPopulateLookupsTo()
        {
            _target.AirfieldToId = 11;
            _target.PopulateLookups(new Lookups { Airfields = new ObservableCollection<Airfield> { new Airfield { id = 11 } } },
                flightData.InMemoryLookups);
            Assert.IsNotNull(_target.To);
        }

        [TestMethod]
        public void ShouldNotPopulateLookupsTo()
        {
            _target.AirfieldToId = 0;
            _target.PopulateLookups(new Lookups { Airfields = new ObservableCollection<Airfield> { new Airfield { id = 11 } } },
                flightData.InMemoryLookups);
            Assert.IsNull(_target.To);
        }

        [TestMethod]
        public void ShouldPopulateLookups()
        {

            _target.PopulateLookups(new Lookups(), flightData.InMemoryLookups);
            Assert.IsNotNull(_target.Lookups);
        }


        [TestMethod]
        public void NewFlightShouldHavePopulatedLookups()
        {
            Assert.IsNotNull(_target.Lookups);

        }
       
    }
}
