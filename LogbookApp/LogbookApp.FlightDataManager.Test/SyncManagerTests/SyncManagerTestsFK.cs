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

            OnlineDataService.InsertAircraft(new Aircraft { id = 1 });
            OnlineDataService.InsertAircraft(new Aircraft { id = 2 });
            OnlineData = OnlineDataService.LoadUserData("").Result;

            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);
            OnlineData = OnlineDataService.LoadUserData("").Result;
            Assert.IsTrue(OnlineData.Lookups.Aircraft.Count == 0);

        }

        [TestMethod]
        public void ShouldDeleteTwoButNotIfLocal()
        {

            OnlineDataService.InsertAircraft(new Aircraft { id = 1 });
            OnlineDataService.InsertAircraft(new Aircraft { id = 2 });
            OnlineDataService.InsertAircraft(new Aircraft { id = 3 });
            LocalData.Lookups.Aircraft.Add(new Aircraft {id=2});
            OnlineData = OnlineDataService.LoadUserData("").Result;

            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);

            OnlineData = OnlineDataService.LoadUserData("").Result;
            Assert.IsTrue(OnlineData.Lookups.Aircraft.First().id == 2);

        }

       


        [TestMethod]
        public void ShouldDeleteAircraftAndFlightWhereFK()
        {
            Aircraft aircraft = new Aircraft {id =1};
            OnlineDataService.InsertAircraft(aircraft);
            OnlineDataService.InsertFlight(new Flight {id = 1, Aircraft = aircraft});
            OnlineData = OnlineDataService.LoadUserData("").Result;
            
            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);

            OnlineData = OnlineDataService.LoadUserData("").Result;
            Assert.IsTrue(OnlineData.Flights.Count == 0 && OnlineData.Lookups.Aircraft.Count == 0);
            
        }



        [TestMethod]
        public void ShouldDeleteAirfieldAndFlightFromWhereFK()
        {
            Airfield airfield = new  Airfield { id = 1 };
            OnlineDataService.InsertAirfield(airfield);
            Flight flight = new Flight { id = 1, From =  airfield };
            OnlineDataService.InsertFlight(flight);
            OnlineData = OnlineDataService.LoadUserData("").Result;
            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);

            OnlineData = OnlineDataService.LoadUserData("").Result;
            Assert.IsTrue(OnlineData.Flights.Count == 0 && OnlineData.Lookups.Airfields.Count == 0);

        }

        [TestMethod]
        public void ShouldDeleteAirfieldAndFlightToWhereFK()
        {
            Airfield airfield = new Airfield { id = 1 };
            OnlineDataService.InsertAirfield(airfield);
            Flight flight = new Flight { id = 1, To = airfield };
            OnlineDataService.InsertFlight(flight);
            OnlineData = OnlineDataService.LoadUserData("").Result;
            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);

            OnlineData = OnlineDataService.LoadUserData("").Result;
            Assert.IsTrue(OnlineData.Flights.Count == 0 && OnlineData.Lookups.Airfields.Count == 0);

        }

        [TestMethod]
        public void ShouldDeleteAcTypeAndAircraftWhereFK()
        {
            AcType acType = new AcType { id = 1 };
            OnlineDataService.InsertAcType(acType);
            Aircraft aircraft = new Aircraft { id = 1, AcType  = acType};
            OnlineDataService.InsertAircraft(aircraft);
            OnlineData = OnlineDataService.LoadUserData("").Result;
            
            Target.UpdateOnlineData(OnlineDataService, LocalData, OnlineData, NewerTimeStamp);
            OnlineData = OnlineDataService.LoadUserData("").Result;
            Assert.IsTrue(OnlineData.Lookups.Airfields.Count == 0 && OnlineData.Lookups.AcTypes.Count==0);

        }
    }
}
