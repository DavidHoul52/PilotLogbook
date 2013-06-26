using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
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
            SourceData.Lookups.Airfields.Add(new Airfield());
            Target.UpdateTargetData(OnlineDataService,SourceData,TargetData, NewerTimeStamp);
            Assert.IsNotNull(OnlineDataService.LoadLookups(0).Result.Airfields.FirstOrDefault());
        }

        [TestMethod]
        public void ShouldUpdateExistingAirfield()
        {
            TargetData.Lookups.Airfields.Add(new Airfield { id = 1, TimeStamp = OlderTimeStamp });
            SourceData.Lookups.Airfields.Add(new Airfield { id = 1, ICAOCode = "G", TimeStamp = NewerTimeStamp });
            Target.UpdateTargetData(OnlineDataService,SourceData, TargetData, NewerTimeStamp);
            Assert.AreEqual("G", OnlineDataService.LoadLookups(0).Result.Airfields.First().ICAOCode);
        }


        [TestMethod]
        public void ShouldDeleteExistingAirfield()
        {

            TargetData.Lookups.Airfields.Add(new Airfield { id = 1, ICAOCode = "A" });
            Target.UpdateTargetData(OnlineDataService,SourceData, TargetData, NewerTimeStamp);
            Assert.IsNull(OnlineDataService.LoadLookups(0).Result.Airfields.FirstOrDefault());
        }
        #endregion
    }
}
