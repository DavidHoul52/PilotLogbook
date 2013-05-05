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
        private LocalDataManager target;
        private TestLocalStorage testLocalStorage;
        private string _flightsFileName = "flights";
        private string _lookupsFileName = "lookups";

        [TestInitialize]
        public void Setup()
        {
            testLocalStorage = new TestLocalStorage();
            testLocalStorage.SetExists(true); // default for these tests
            target = new LocalDataManager(testLocalStorage, _flightsFileName, _lookupsFileName, "");
           
        }

        [TestMethod]
        public void ShouldGetLookups()
        {
            target.GetLookups();
            Assert.IsNotNull(target.Lookups);
        }

        [TestMethod]
        public void ShouldInsertFlight()
        {
            target.InsertFlight(new Flight());
            Assert.IsNotNull(target.Flights.FirstOrDefault());
        }

        [TestMethod]
        public void ShouldInsertFlightandSave()
        {
            target.InsertFlight(new Flight());
            Assert.AreEqual(_flightsFileName, testLocalStorage.SavedFileName);
        }


        [TestMethod]
        public void ShouldDeleteFlight()
        {
            var flight = new Flight();
            target.InsertFlight(flight);
            target.DeleteFlight(flight);
            Assert.IsNull(target.Flights.FirstOrDefault());
        }


        [TestMethod]
        public void ShouldDeleteFlightAndSave()
        {
            var flight = new Flight();
            target.InsertFlight(flight);
            target.DeleteFlight(flight);
            Assert.AreEqual(_flightsFileName, testLocalStorage.SavedFileName);
        }


        [TestMethod]
        public void ShouldSaveFlight()
        {
            var flight = new Flight();
            target.SaveFlight(flight);
            Assert.AreEqual(_flightsFileName,testLocalStorage.SavedFileName);
        }

        [TestMethod]
        public void ShouldInsertAircraft()
        {
            target.InsertAircraft(new Aircraft());
            Assert.IsNotNull(target.Lookups.Aircraft.FirstOrDefault());
        
        }

        [TestMethod]
        public void ShouldInsertAircraftAndSave()
        {
            target.InsertAircraft(new Aircraft());
            Assert.AreEqual(_lookupsFileName, testLocalStorage.SavedFileName);

        }

        [TestMethod]
        public void ShouldInsertAircraftType()
        {
            target.InsertAircraftType(new AcType());
            Assert.IsNotNull(target.Lookups.AcTypes.FirstOrDefault());

        }

        [TestMethod]
        public void ShouldInsertAircraftTypeAndSave()
        {
            target.InsertAircraftType(new AcType());
            Assert.AreEqual(_lookupsFileName, testLocalStorage.SavedFileName);

        }

        [TestMethod]
        public void ShouldCreateUserIfNull()
        {
            testLocalStorage.SetExists(false);
            target.GetUser("ducckie");
            Assert.IsNotNull(target.User);

        }

        //[TestMethod]
        //public void ShouldSaveFlight()
        //{

        //    target.SaveFlight(new Flight());
        //    Assert.IsNotNull(target.Flights.FirstOrDefault());
        //}

    }
}
