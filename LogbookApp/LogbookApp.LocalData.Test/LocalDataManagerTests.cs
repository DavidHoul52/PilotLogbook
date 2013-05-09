using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogbookApp.Data;
using LogbookApp.Mocks;
using LogbookApp.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.LocalData.Test
{
    [TestClass]
    public class LocalDataManagerTests
    {
        private LocalDataService target;
        private TestLocalStorage testLocalStorage;
        private string _flightsFileName = "flights";
        private string _lookupsFileName = "lookups";

        [TestInitialize]
        public void Setup()
        {
            testLocalStorage = new TestLocalStorage();
            testLocalStorage.SetExists(true); // default for these tests
            testLocalStorage.AllSaved = false;
            target = new LocalDataService(testLocalStorage, _flightsFileName, _lookupsFileName, "");
            target.SetFlightData(new FlightData());
           
        }

        [TestMethod]
        public void ShouldGetLookups()
        {
            var lookups= target.GetLookups(0);
            Assert.IsNotNull(lookups);
        }

        
        [TestMethod]
        public void ShouldInsertFlightandSave()
        {
            target.InsertFlight(new Flight());
            Assert.IsTrue(testLocalStorage.AllSaved);
        }


        [TestMethod]
        public void ShouldDeleteFlightAndSave()
        {
            var flight = new Flight();
            target.InsertFlight(flight);
            target.DeleteFlight(flight);
            Assert.IsTrue(testLocalStorage.AllSaved);
        }


        [TestMethod]
        public void ShouldSaveFlight()
        {
            var flight = new Flight();
            target.SaveFlight(flight);
            Assert.IsTrue(testLocalStorage.AllSaved);
        }


        [TestMethod]
        public void ShouldInsertAircraftAndSave()
        {
            target.InsertAircraft(new Aircraft());
            Assert.IsTrue(testLocalStorage.AllSaved);

        }


        [TestMethod]
        public void ShouldInsertAircraftTypeAndSave()
        {
            target.InsertAircraftType(new AcType());
            Assert.IsTrue(testLocalStorage.AllSaved);

        }

        

    }
}
