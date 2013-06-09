using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest.SyncManagerTests
{
    [TestClass]
    public class SyncManagerTestsFlights :SyncManagerTestsBase
    {

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

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

            _targetFlightData.Flights.Add(new Flight { id = 1, Remarks = "A" });
            target.UpdateOnlineData(_sourceFlightData, _newerTimeStamp);
            Assert.IsNull(_onlineFlightDataService.GetFlights(0).Result.FirstOrDefault());
        }
        #endregion
    }
}
