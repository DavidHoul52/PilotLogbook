using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataServiceTestsSyncLocalData  : FlightDataServiceTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            TestLocalStorage.SetUserName("");
        }

        
        [TestMethod]

        public void ShouldUpdateOnlineDataCalled() // ????
        {
            SetupDataType(DataType.OnLine);
            LocalTestData.FlightData.Flights = new ObservableCollection<Flight> { new Flight() };
            OnLineDataServiceFlightData.Flights = new ObservableCollection<Flight> { new Flight() };
            SetLastUpdatesLocalOnline(NewerTime, OldTime);
            Target.StartUp("");
            

            Assert.IsTrue(MockSyncManager.UpdateOnlineDataCalled);

        }


        //[TestMethod]
        //public void ShouldUpdateLocalDataWithOnlineFlights()
        //{

        //    OnlineTestData.Flights = new List<Flight> { new Flight()};
        //    SetLastUpdates(null, OldTime);
        //    OnlineTestData.SetAvailable(true);
        //    Target.SaveFlight(new Flight(), NewerTime);
        //    Assert.AreEqual(1,LocalTestData.Flights.Count);



        //}


        //[TestMethod]
        //public void ShouldUpdateLocalDataWithOnlineLookups()
        //{

        //    OnlineTestData.Lookups = new Lookups{ Aircraft = new ObservableCollection<Aircraft> {new Aircraft()}};
        //    SetLastUpdates(null, OldTime);
        //    OnlineTestData.SetAvailable(true);
        //    Target.SaveFlight(new Flight(), NewerTime);
        //    Assert.AreEqual(1, LocalTestData.Lookups.Aircraft.Count);



        //}


        //[TestMethod]
        //public void ShouldUpdateLocalDataWithOnlineUser()
        //{

            
        //    SetLastUpdates(null, OldTime);
        //    OnlineTestData.SetAvailable(true);
        //    OnlineTestData.User.Id = 1;
        //    Target.SaveFlight(new Flight(), NewerTime);
        //    Assert.AreEqual(1, LocalTestData.User.Id);



        //}
    }
}
