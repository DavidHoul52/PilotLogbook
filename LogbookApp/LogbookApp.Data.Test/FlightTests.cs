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
        private InMemoryLookups _inMemoryLookups;
       

        [TestInitialize]
        public void Setup()
        {
            _target = new Flight();
            _inMemoryLookups = new InMemoryLookups();
        }

        [TestMethod]
        public void ShouldPopulateLookupsAircraft()
        {
            _target.AircraftId = 22;
            _target.PopulateLookups(new Lookups { Aircraft = new ObservableCollection<Aircraft> { new Aircraft {id = 22}}},
                _inMemoryLookups);
            Assert.IsNotNull(_target.Aircraft);
        }

        [TestMethod]
        public void ShouldNotPopulateLookupsAircraft()
        {
            _target.AircraftId = 23;
            _target.PopulateLookups(new Lookups { Aircraft = new ObservableCollection<Aircraft> { new Aircraft { id = 22 } } },
               _inMemoryLookups);
            Assert.IsNull(_target.Aircraft);
        }

        [TestMethod]
        public void ShouldPopulateLookupsCapacity()
        {
            _target.CapacityId = 1;
            _target.PopulateLookups(new Lookups (),_inMemoryLookups);
            Assert.IsNotNull(_target.Capacity);
        }

        [TestMethod]
        public void ShouldNotPopulateLookupsCapacity()
        {
            _target.CapacityId = -1;
            _target.PopulateLookups(new Lookups(), _inMemoryLookups);
            Assert.IsNull(_target.Capacity);
        }

        [TestMethod]
        public void ShouldPopulateLookupsFrom()
        {
            _target.AirfieldFromId = 11;
            _target.PopulateLookups(new Lookups { Airfields = new ObservableCollection<Airfield> { new Airfield { id = 11 } } },
                _inMemoryLookups);
            Assert.IsNotNull(_target.From);
        }

        [TestMethod]
        public void ShouldNotPopulateLookupsFrom()
        {
            _target.AirfieldFromId = 0;
            _target.PopulateLookups(new Lookups { Airfields = new ObservableCollection<Airfield> { new Airfield { id = 11 } } },
                _inMemoryLookups);
            Assert.IsNull(_target.From);
        }

        [TestMethod]
        public void ShouldPopulateLookupsTo()
        {
            _target.AirfieldToId = 11;
            _target.PopulateLookups(new Lookups { Airfields = new ObservableCollection<Airfield> { new Airfield { id = 11 } } },
                _inMemoryLookups);
            Assert.IsNotNull(_target.To);
        }

        [TestMethod]
        public void ShouldNotPopulateLookupsTo()
        {
            _target.AirfieldToId = 0;
            _target.PopulateLookups(new Lookups { Airfields = new ObservableCollection<Airfield> { new Airfield { id = 11 } } },
                _inMemoryLookups);
            Assert.IsNull(_target.To);
        }

        [TestMethod]
        public void ShouldPopulateLookups()
        {
            
            _target.PopulateLookups(new Lookups(),_inMemoryLookups);
            Assert.IsNotNull(_target.Lookups);
        }
    }
}
