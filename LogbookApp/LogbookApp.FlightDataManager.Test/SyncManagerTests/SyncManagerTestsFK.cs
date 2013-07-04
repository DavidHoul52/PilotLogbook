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
    public class SyncManagerTestsFk : SyncManagerTestsBase<FlightsSyncManager<MockOnlineFlightData>,
        FlightData, User, MockOnlineFlightData>
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }


        [TestMethod]
        public void ShouldDeleteTwo()
        {
            
            OnlineData.Lookups.Aircraft.Add(new Aircraft {id =1});
            OnlineData.Lookups.Aircraft.Add(new Aircraft { id = 2 });
            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);
            Assert.IsTrue(OnlineData.Lookups.Aircraft.Count == 0);

        }

        [TestMethod]
        public void ShouldDeleteTwoButNotIfLocal()
        {

            OnlineData.Lookups.Aircraft.Add(new Aircraft { id = 1 });
            OnlineData.Lookups.Aircraft.Add(new Aircraft { id = 2 });
            OnlineData.Lookups.Aircraft.Add(new Aircraft { id = 3 });
            LocalData.Lookups.Aircraft.Add(new Aircraft {id=2});
            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);
            Assert.IsTrue(OnlineData.Lookups.Aircraft.First().id == 2);

        }

        [TestMethod]
        public void ShouldDeleteLookupAndFlightWhereFK()
        {
            Aircraft aircraft = new Aircraft {id =1};
            OnlineData.Lookups.Aircraft.Add(aircraft);
            Flight flight = new Flight {id = 1, Aircraft = aircraft};
            OnlineData.Flights.Add(flight);
            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);
            
            Assert.IsTrue(OnlineData.Flights.Count == 0 && OnlineData.Lookups.Aircraft.Count == 0);
            
        }
    }
}
