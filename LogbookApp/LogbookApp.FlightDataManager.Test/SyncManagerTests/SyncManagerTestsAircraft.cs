using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace LogbookApp.FlightDataManagerTest.SyncManagerTests
{
    [TestClass]
    public class SyncManagerTestsAircraft : SyncManagerTestsBase<FlightData, User, MockOnlineFlightData>
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
            SourceData.Lookups.Aircraft.Add(new Aircraft());
            Target.UpdateTargetData(SourceData, TargetData, NewerTimeStamp);
            Assert.IsNotNull(OnlineDataService.LoadLookups(0).Result.Aircraft.FirstOrDefault());
        }


        [TestMethod]
        public void ShouldUpdateExistingAircraft()
        {
            TargetData.Lookups.Aircraft.Add(new Aircraft { id = 1, TimeStamp = OlderTimeStamp });
            SourceData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "G", TimeStamp = NewerTimeStamp });
            Target.UpdateTargetData(SourceData, TargetData, NewerTimeStamp);
            Assert.AreEqual("G", OnlineDataService.LoadLookups(0).Result.Aircraft.First().Reg);
        }

        [TestMethod]
        public void ShouldNotUpdateExistingAircraft()
        {
            TargetData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "A", TimeStamp = NewerTimeStamp });
            SourceData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "G", TimeStamp = OlderTimeStamp });
            Target.UpdateTargetData(SourceData, TargetData, NewerTimeStamp);
            Assert.AreEqual("A", OnlineDataService.LoadLookups(0).Result.Aircraft.First().Reg);
        }

        [TestMethod]
        public void ShouldDeleteExistingAircraft()
        {

            TargetData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "A" });
            Target.UpdateTargetData(SourceData, TargetData, NewerTimeStamp);
            Assert.IsNull(OnlineDataService.LoadLookups(0).Result.Aircraft.FirstOrDefault());
        }
        #endregion
    }
}
