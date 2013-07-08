using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest.MockFrameworkTests
{
    [TestClass]
    public class MockOnLineDataTests
    {
        private MockOnlineFlightData OnlineDataService;
        private FlightData OnlineData;

        [TestInitialize]
        public void Setup()
        {
            OnlineDataService= new MockOnlineFlightData();
            OnlineData = new FlightData();
            OnlineDataService.CreateUserData("", DateTime.Now);


        }


        [TestMethod]
        public void MockOnLineDataShouldNotKeepRefToFlightData()
        {
            Aircraft aircraft = new Aircraft { id = 1 };
            OnlineDataService.InsertAircraft(aircraft);
            //OnlineData.Lookups.Aircraft.Add(aircraft);
            Flight flight = new Flight { id = 1, Aircraft = aircraft };
            OnlineDataService.InsertFlight(flight);
            OnlineData = OnlineDataService.LoadUserData("").Result;
            Assert.AreNotEqual(OnlineData, OnlineDataService.LoadUserData("").Result);


        }
    }
}
