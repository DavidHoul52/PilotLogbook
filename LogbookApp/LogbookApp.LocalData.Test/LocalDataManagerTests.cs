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
        private FlightData _flightData;
        private string displayName = "";

        [TestInitialize]
        public void Setup()
        {
            testLocalStorage = new TestLocalStorage();
            testLocalStorage.SetExists(true); // default for these tests
            testLocalStorage.AllSaved = false;
            _flightData = new FlightData();

            target = new LocalDataService(testLocalStorage, _flightsFileName, _lookupsFileName, "");
            
           
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
            target.SaveLocalData(_flightData);
            Assert.IsTrue(testLocalStorage.AllSaved);
        }


    
        

    }
}
