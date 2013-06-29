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
            LocalData.AddFlight(new Flight());
            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);
            Assert.IsNotNull(OnlineDataService.GetFlights(0).Result.FirstOrDefault());
        }

        [TestMethod]
        public void ShouldUpdateExistingFlight()
        {
            OnlineData.Flights.Add(new Flight { id = 1, TimeStamp = OlderTimeStamp });
            LocalData.Flights.Add(new Flight { id = 1, Remarks = "G", TimeStamp = NewerTimeStamp });
            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);
              Assert.AreEqual("G", OnlineDataService.GetFlights(0).Result.First().Remarks);
        }

        [TestMethod]
        public void ShouldDeleteExistingFlight()
        {

            OnlineData.Flights.Add(new Flight { id = 1, Remarks = "A" });
            Target.UpdateOnlineData(OnlineDataService,LocalData, OnlineData, NewerTimeStamp);
             Assert.IsNull(OnlineDataService.GetFlights(0).Result.FirstOrDefault());
        }
        #endregion
    }
}
