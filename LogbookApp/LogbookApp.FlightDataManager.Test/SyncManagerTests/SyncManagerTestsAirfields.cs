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
    public class SyncManagerTestsAirfields : SyncManagerTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

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
    }
}
