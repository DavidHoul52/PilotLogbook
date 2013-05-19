using System;
using System.Linq;
using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{



    [TestClass]
    public class FlightDataServiceTestsSave : FlightDataServiceTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }


        [TestMethod]
        public void OnlineShouldSaveFlightOnlineAndSetLastupdated()
        {
            SetLastUpdates(null,OldTime);
            OnlineDataService.SetAvailable(true);
            Target.SaveFlight(new Flight(),NewerTime);
            Assert.AreEqual(NewerTime,OnlineDataService.LastUpdated);
        }


        [TestMethod]
        public void OnlineShouldSaveFlightLocalAndSetLastupdated()
        {
            SetLastUpdates(null, OldTime);
            OnlineDataService.SetAvailable(true);
            Target.SaveFlight(new Flight(), NewerTime);
            Assert.AreEqual(NewerTime, LocalTestData.LastUpdated);



        }

        [TestMethod]
        public void OfflineShouldSaveFlightLocalAndSetLastupdated()
        {


            SetLastUpdates(null, OldTime);
            OnlineDataService.SetAvailable(false);
            Target.SaveFlight(new Flight(), NewerTime);
            Assert.AreEqual(NewerTime, LocalTestData.LastUpdated);



        }



        [TestMethod] 
        public void ShouldNotSaveFlightOnlineIfOnlinenotAvailable()
        {

            OnlineDataService.LastUpdated = OldTime;
            OnlineDataService.SetAvailable(false);
            Target.SaveFlight(new Flight(), NewerTime);
            Assert.AreEqual(OldTime, OnlineDataService.LastUpdated);



        }


        [TestMethod]
        public void WhenSavingFlightShouldUpdateTimeStamp()
        {

            SetLastUpdates(null, OldTime);
            OnlineDataService.SetAvailable(true);
            var flight = new Flight {Date= new DateTime(2013,8,1)};
            Target.SaveFlight(flight, NewerTime);
            Assert.AreEqual(NewerTime,flight.TimeStamp);




        }


     

       




    }
}
