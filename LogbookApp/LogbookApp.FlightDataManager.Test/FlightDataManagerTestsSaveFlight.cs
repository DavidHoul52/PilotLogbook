using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary2.DataManagerTests;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataManagerTestsSaveFlight : FlightDataManagerTestBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            
        }

        [TestMethod]
        public void ShouldUpdateDateTimeStampWhenSynced()
        {
            StartupAsConnected(TestDates.NowLess1);
            FlightData flightData = new FlightData();

            Target.SaveFlight(new FlightFactory().CreateFlight(flightData, TestDates.Now), TestDates.Now);
            Assert.AreEqual(TestDates.Now,Target.Data.User.TimeStamp);
        }

        [TestMethod]
        public void ShouldUpdateDateTimeStampWhenNotSyncedButUptodate()
        {
            StartupAsOfflineExistingUser(TestDates.NowLess1,TestDates.NowLess1);
            Internet.SetConnected(true);
            FlightData flightData = new FlightData();
            Target.SaveFlight(new FlightFactory().CreateFlight(flightData, TestDates.Now), TestDates.Now);
            Assert.AreEqual(TestDates.Now, Target.Data.User.TimeStamp);
        }


        [TestMethod]
        public void ShouldUpdateDateTimeStampWhenNotSyncedButNotUptodate()
        {
            StartupAsOfflineExistingUser(TestDates.NowLess1, TestDates.NowLess2);
            Internet.SetConnected(true);
            FlightData flightData = new FlightData();
            Target.SaveFlight(new FlightFactory().CreateFlight(flightData, TestDates.Now), TestDates.Now);
            Assert.AreEqual(TestDates.Now, Target.Data.User.TimeStamp);
        }


        [TestMethod]
        public void ShouldSaveNewFlightInOffLineData()
        {
            StartupAsOfflineExistingUser(TestDates.NowLess1, TestDates.NowLess2);
            Internet.SetConnected(true);
            FlightData flightData = new FlightData();
            Target.SaveFlight(new FlightFactory().CreateFlight(flightData, TestDates.Now), TestDates.Now);
            FlightData data= OfflineDataService.LoadUserData("").Result;
            Assert.AreEqual(1,data.Flights.Count);
            
        }



        [TestMethod]
        public void ShouldSaveEditedFlightInOffLineData()
        {
            StartupAsOfflineExistingUser(TestDates.NowLess1, TestDates.NowLess2);
            Internet.SetConnected(true);
            FlightData flightData = new FlightData();
            Target.SaveFlight(new FlightFactory().CreateFlight(flightData, TestDates.Now), TestDates.Now);
            FlightData data = OfflineDataService.LoadUserData("").Result;
            var flight = data.Flights.First();
            flight.Reg = "ABCD";
            flight.IsNew = false;
            Target.SaveFlight(flight,TestDates.Now);
            FlightData data2 = OfflineDataService.LoadUserData("").Result;
            Assert.AreEqual("ABCD",data2.Flights.First().Reg);

        }
        
    }
}
