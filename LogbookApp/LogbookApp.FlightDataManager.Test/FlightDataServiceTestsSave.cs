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
            SetupDataType(DataType.OnLine);
            SetLastUpdates(null,OldTime);
            Target.StartUp("");
            Target.SaveFlight(new Flight(),NewerTime);
            Assert.AreEqual(NewerTime,OnlineDataService.LastUpdated);
        }


        [TestMethod]
        public void OnlineShouldSaveFlightLocalAndSetLastupdated()
        {
            SetupDataType(DataType.OnLine);
            SetLastUpdates(null, OldTime);
            Target.StartUp("");
            Target.SaveFlight(new Flight(), NewerTime);
            Assert.AreEqual(NewerTime, LocalTestData.LastUpdated);



        }

        [TestMethod]
        public void OfflineShouldSaveFlightLocalAndSetLastupdated()
        {
            
            SetupDataType(DataType.OffLine);
            SetLastUpdates(null, OldTime);
            Target.StartUp("");
            Target.SaveFlight(new Flight(), NewerTime);
            Assert.AreEqual(NewerTime, LocalTestData.LastUpdated);



        }

      

        [TestMethod] 
        public void ShouldNotSaveFlightOnlineIfOnlinenotAvailable()
        {
            SetupDataType(DataType.OffLine);
            OnlineDataService.LastUpdated = OldTime;
            Target.StartUp("");
            Target.SaveFlight(new Flight(), NewerTime);
            Assert.AreEqual(OldTime, OnlineDataService.LastUpdated);



        }


        [TestMethod]
        public void WhenSavingFlightShouldUpdateTimeStamp()
        {
            SetupDataType(DataType.OnLine);
            
            SetLastUpdates(null, OldTime);
            Target.StartUp("");
            var flight = new Flight {Date= new DateTime(2013,8,1)};
            Target.SaveFlight(flight, NewerTime);
            Assert.AreEqual(NewerTime,flight.TimeStamp);




        }


     

       




    }
}
