using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace LogbookApp.FlightDataManagerTest.SyncManagerTests
{
    [TestClass]
    public class SyncManagerTestsAirfields : SyncManagerTestsBase<FlightsSyncManager<MockOnlineFlightData>,
        FlightData, User, MockOnlineFlightData>
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
            LocalData.Lookups.Airfields.Add(new Airfield());
            Target.UpdateOnlineData(OnlineDataService,LocalData,OnlineData, NewerTimeStamp);
            Assert.IsNotNull(OnlineDataService.LoadLookups(0).Result.Airfields.FirstOrDefault());
        }

        [TestMethod]
        public void ShouldUpdateExistingAirfield()
        {
            OnlineData.Lookups.Airfields.Add(new Airfield { id = 1, TimeStamp = OlderTimeStamp });
            LocalData.Lookups.Airfields.Add(new Airfield { id = 1, ICAOCode = "G", TimeStamp = NewerTimeStamp });
            Target.UpdateOnlineData(OnlineDataService,LocalData, OnlineData, NewerTimeStamp);
            Assert.AreEqual("G", OnlineDataService.LoadLookups(0).Result.Airfields.First().ICAOCode);
        }


        [TestMethod]
        public void ShouldDeleteExistingAirfield()
        {

            OnlineData.Lookups.Airfields.Add(new Airfield { id = 1, ICAOCode = "A" });
            Target.UpdateOnlineData(OnlineDataService,LocalData, OnlineData, NewerTimeStamp);
            Assert.IsNull(OnlineDataService.LoadLookups(0).Result.Airfields.FirstOrDefault());
        }
        #endregion
    }
}
