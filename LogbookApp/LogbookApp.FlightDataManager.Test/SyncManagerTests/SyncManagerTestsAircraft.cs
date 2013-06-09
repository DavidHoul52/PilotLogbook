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
    public class SyncManagerTestsAircraft : SyncManagerTestsBase
    {
        #region Aircraft

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

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
    }
}
