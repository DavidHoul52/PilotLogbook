using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.Data.Test
{



    [TestClass]
    public class FlightDataServiceTests
    {
        private FlightDataManager target;
        private TestFlightDataService _onlineTestData;
        private TestFlightDataService _localTestData;
        private bool _onlineDataUpdatedFromOffLine;
        private DateTime _oldTime;
        private DateTime _newerTime;

        [TestInitialize]
        public void Setup()
        {
            _onlineTestData = new TestFlightDataService(DataType.OnLine);
            _localTestData = new TestFlightDataService(DataType.OffLine);
            _localTestData.SetAvailable(true);
            _onlineDataUpdatedFromOffLine = false;
            _oldTime = new DateTime(2012, 1, 1);
            _newerTime = new DateTime(2013,1,1);
            target = new FlightDataManager(_onlineTestData, _localTestData, () =>
            {
                _onlineDataUpdatedFromOffLine = true;
            },"david");
        }

        [TestMethod]
        public void ShouldGetOnlineDataIfAvailable()
        {
            
            _onlineTestData.SetAvailable(true);
           target.GetData();
           Assert.AreEqual(DataType.OnLine, target.DataType);


        }

        [TestMethod]
        public void ShouldGetOfflineDataIfOnLineNotAvailable()
        {

            _onlineTestData.SetAvailable(false);
            target.GetData();
            Assert.AreEqual(DataType.OffLine, target.DataType);


        }

        [TestMethod]
        public void IfOnlineDataIfAvailableAndLocalNewerThenShouldUpdateOnlineData()
        {
            
            _onlineTestData.SetAvailable(true);
            _localTestData.User.LastUpdated = _newerTime ;
            _onlineTestData.User.LastUpdated = _oldTime;
            target.GetData();
            Assert.IsTrue(_onlineDataUpdatedFromOffLine);
            


        }

        [TestMethod]
        public void IfOnlineDataIfAvailableAndNewerThanLocalThenShouldNotUpdateOnlineData()
        {

            _onlineTestData.SetAvailable(true);
            _localTestData.User.LastUpdated = _oldTime;
            _onlineTestData.User.LastUpdated = _newerTime;
            target.GetData();
            Assert.IsFalse(_onlineDataUpdatedFromOffLine);



        }
        [TestMethod]
        public void IfOnlineDataIfAvailableAndLastUpdatedNullThenShouldUpdateOnlineData()
        {

            _onlineTestData.SetAvailable(true);
            _localTestData.User.LastUpdated = _newerTime;
            _onlineTestData.User.LastUpdated = null;
            target.GetData();
            Assert.IsTrue(_onlineDataUpdatedFromOffLine);



        }

        [TestMethod]
        public void ShouldSaveFlightOnlineDataIfAvailable()
        {

            _onlineTestData.User.LastUpdated = _oldTime;
            _onlineTestData.SetAvailable(true);
            target.SaveFlight(new Flight(),_newerTime);
            Assert.AreEqual(_newerTime,_onlineTestData.User.LastUpdated);
            


        }


        [TestMethod]
        public void ShouldSaveFlightToLocalDataIfOnlinenotAvailable()
        {

            _onlineTestData.User.LastUpdated = _oldTime;
            _onlineTestData.SetAvailable(false);
            target.SaveFlight(new Flight(), _newerTime);
            Assert.AreEqual(_newerTime, _localTestData.User.LastUpdated);



        }


        [TestMethod] 
        public void ShouldNotSaveFlighOnlineIfOnlinenotAvailable()
        {

            _onlineTestData.User.LastUpdated = _oldTime;
            _onlineTestData.SetAvailable(false);
            target.SaveFlight(new Flight(), _newerTime);
            Assert.AreEqual(_oldTime, _onlineTestData.User.LastUpdated);



        }

        [TestMethod]
        public void ShouldInsertAircraftIfOnlineDataIfAvailable()
        {

            _onlineTestData.User.LastUpdated = _oldTime;
            _onlineTestData.SetAvailable(true);
            
            target.InsertAircraft(new Aircraft(), _newerTime);
            Assert.AreEqual(_newerTime, _onlineTestData.User.LastUpdated);



        }




    }
}
