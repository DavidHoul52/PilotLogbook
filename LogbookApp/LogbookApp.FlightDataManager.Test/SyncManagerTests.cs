using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class SyncManagerTests
    {
        private SyncManager target;
        private MockFlightDataService _onlineFlightDataService;
        private FlightData _sourceFlightData;
        private FlightData _targetFlightData;
        private DateTime _newerTimeStamp = new DateTime(2013,10,1);
        private DateTime _olderTimeStamp = new DateTime(2013, 1, 1);



        [TestInitialize]
        public void Setup()
        {
            _targetFlightData = new FlightData();
            _onlineFlightDataService = new MockFlightDataService(DataType.OnLine, _targetFlightData);
            _sourceFlightData = new FlightData();
            target = new SyncManager(_onlineFlightDataService);
        }
        #region Aircraft

        [TestMethod]
        public void ShouldAddNewAircraft()
        {
            _sourceFlightData.Lookups.Aircraft.Add(new Aircraft());
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.IsNotNull(_onlineFlightDataService.GetLookups(0).Result.Aircraft.FirstOrDefault());
        }


        [TestMethod]
        public void ShouldUpdateExistingAircraft()
        {
            _targetFlightData.Lookups.Aircraft.Add(new Aircraft { id = 1, TimeStamp = _olderTimeStamp });
            _sourceFlightData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "G", TimeStamp = _newerTimeStamp });
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.AreEqual("G", _onlineFlightDataService.GetLookups(0).Result.Aircraft.First().Reg);
        }

        [TestMethod]
        public void ShouldNotUpdateExistingAircraft()
        {
            _targetFlightData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "A", TimeStamp = _newerTimeStamp });
            _sourceFlightData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "G", TimeStamp = _olderTimeStamp });
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.AreEqual("A", _onlineFlightDataService.GetLookups(0).Result.Aircraft.First().Reg);
        }

        [TestMethod]
        public void ShouldDeleteExistingAircraft()
        {

            _targetFlightData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "A" });
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.IsNull(_onlineFlightDataService.GetLookups(0).Result.Aircraft.FirstOrDefault());
        } 
        #endregion
        #region Airfields

        [TestMethod]
        public void ShouldAddNewAirfield()
        {
            _sourceFlightData.Lookups.Airfields.Add(new Airfield());
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.IsNotNull(_onlineFlightDataService.GetLookups(0).Result.Airfields.FirstOrDefault());
        }

        [TestMethod]
        public void ShouldUpdateExistingAirfield()
        {
            _targetFlightData.Lookups.Airfields.Add(new Airfield { id = 1, TimeStamp = _olderTimeStamp });
            _sourceFlightData.Lookups.Airfields.Add(new Airfield { id = 1, ICAOCode = "G", TimeStamp = _newerTimeStamp });
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.AreEqual("G", _onlineFlightDataService.GetLookups(0).Result.Airfields.First().ICAOCode);
        }


        [TestMethod]
        public void ShouldDeleteExistingAirfield()
        {

            _targetFlightData.Lookups.Airfields.Add(new Airfield { id = 1, ICAOCode = "A" });
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.IsNull(_onlineFlightDataService.GetLookups(0).Result.Airfields.FirstOrDefault());
        } 
        #endregion

        #region Flights

        [TestMethod]
        public void ShouldAddNewFlight()
        {
            _sourceFlightData.AddFlight(new Flight());
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.IsNotNull(_onlineFlightDataService.GetFlights(0).Result.FirstOrDefault());
        }

        [TestMethod]
        public void ShouldUpdateExistingFlight()
        {
            _targetFlightData.Flights.Add(new Flight { id = 1, TimeStamp = _olderTimeStamp });
            _sourceFlightData.Flights.Add(new Flight { id = 1, Remarks = "G", TimeStamp = _newerTimeStamp });
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.AreEqual("G", _onlineFlightDataService.GetFlights(0).Result.First().Remarks);
        }

        [TestMethod]
        public void ShouldDeleteExistingFlight()
        {

            _targetFlightData.Flights.Add(new Flight { id = 1, Remarks =  "A" });
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.IsNull(_onlineFlightDataService.GetFlights(0).Result.FirstOrDefault());
        } 
        #endregion
    }
}
