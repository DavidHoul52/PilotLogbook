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
    public class SyncManagerTestsFlights : SyncManagerTestsBase<FlightsSyncManager<MockOnlineFlightData>,
        FlightData, User, MockOnlineFlightData>
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
            SourceData.AddFlight(new Flight());
            Target.UpdateTargetData(OnlineDataService, SourceData, TargetData, NewerTimeStamp);
            Assert.IsNotNull(OnlineDataService.GetFlights(0).Result.FirstOrDefault());
        }

        [TestMethod]
        public void ShouldUpdateExistingFlight()
        {
            TargetData.Flights.Add(new Flight { id = 1, TimeStamp = OlderTimeStamp });
            SourceData.Flights.Add(new Flight { id = 1, Remarks = "G", TimeStamp = NewerTimeStamp });
            Target.UpdateTargetData(OnlineDataService, SourceData, TargetData, NewerTimeStamp);
              Assert.AreEqual("G", OnlineDataService.GetFlights(0).Result.First().Remarks);
        }

        [TestMethod]
        public void ShouldDeleteExistingFlight()
        {

            TargetData.Flights.Add(new Flight { id = 1, Remarks = "A" });
            Target.UpdateTargetData(OnlineDataService,SourceData, TargetData, NewerTimeStamp);
             Assert.IsNull(OnlineDataService.GetFlights(0).Result.FirstOrDefault());
        }
        #endregion
    }
}
