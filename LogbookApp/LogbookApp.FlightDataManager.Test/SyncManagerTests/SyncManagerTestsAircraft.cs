using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using LogbookApp.Mocks;
using LogbookApp.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace LogbookApp.FlightDataManagerTest.SyncManagerTests
{
    [TestClass]
    public class SyncManagerTestsAircraft : SyncManagerTestsBase<FlightsSyncManager<MockOnlineFlightData>,
        FlightData, User, MockOnlineFlightData>
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
            LocalData.Lookups.Aircraft.Add(new Aircraft());
            Target.UpdateOnlineData(OnlineDataService,LocalData, OnlineData, NewerTimeStamp);
            Assert.IsNotNull(OnlineDataService.LoadLookups(0).Result.Aircraft.FirstOrDefault());
        }


        [TestMethod]
        public void ShouldUpdateExistingAircraft()
        {
            OnlineData.Lookups.Aircraft.Add(new Aircraft { id = 1, TimeStamp = OlderTimeStamp });
            LocalData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "G", TimeStamp = NewerTimeStamp });
            Target.UpdateOnlineData(OnlineDataService,LocalData, OnlineData, NewerTimeStamp);
            Assert.AreEqual("G", OnlineDataService.LoadLookups(0).Result.Aircraft.First().Reg);
        }

        [TestMethod]
        public void ShouldNotUpdateExistingAircraft()
        {
            var aircraft = new Aircraft {id = 1, Reg = "A", TimeStamp = NewerTimeStamp};
            
            OnlineDataService.InsertAircraft(aircraft);
            OnlineData = OnlineDataService.LoadUserData("").Result;
            LocalData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "G", TimeStamp = OlderTimeStamp });
            
            Target.UpdateOnlineData(OnlineDataService,LocalData, OnlineData, NewerTimeStamp);
            OnlineData = OnlineDataService.LoadUserData("").Result;
            Assert.AreEqual("A", OnlineData.Lookups.Aircraft.First().Reg);
        }

        [TestMethod]
        public void ShouldDeleteExistingAircraft()
        {

            OnlineData.Lookups.Aircraft.Add(new Aircraft { id = 1, Reg = "A" });
            Target.UpdateOnlineData(OnlineDataService,LocalData, OnlineData, NewerTimeStamp);
            Assert.IsNull(OnlineDataService.LoadLookups(0).Result.Aircraft.FirstOrDefault());
        }
        #endregion
    }
}
