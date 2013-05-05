using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{



    [TestClass]
    public class FlightDataServiceTests : FlightDataServiceTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void ShouldGetOnlineDataIfAvailable()
        {
            
            OnlineTestData.SetAvailable(true);
           Target.GetData(Now);
           Assert.AreEqual(DataType.OnLine, Target.DataType);
        }

        [TestMethod]
        public void OnlineDataShouldGetUser()
        {

            OnlineTestData.SetAvailable(true);
            Target.GetData(Now);
            Assert.IsNotNull((OnlineTestData.User));
        }

        [TestMethod]
        public void OnlineDataShouldGetUserLastUpdated()  // see also UserManagerTests
        {

            OnlineTestData.SetAvailable(true);
            Target.GetData(Now);
            Assert.IsNotNull((OnlineTestData.User.LastUpdated));


        }

        [TestMethod]
        public void ShouldGetOfflineDataIfOnLineNotAvailable()
        {
            OnlineTestData.SetAvailable(false);
            Target.GetData(Now);
            Assert.AreEqual(DataType.OffLine, Target.DataType);
        }

        [TestMethod]
        public void IfOnlineDataAvailableAndLocalNewerThenShouldUpdateOnlineData()
        {
            
            OnlineTestData.SetAvailable(true);
            
            SetLastUpdates(NewerTime, OldTime);
            Target.GetData(Now);
            Assert.IsTrue(OnlineDataUpdatedFromOffLine);
            


        }

    

        [TestMethod]
        public void IfOnlineDataIfAvailableAndNewerThanLocalThenShouldNotUpdateOnlineData()
        {

            OnlineTestData.SetAvailable(true);
            SetLastUpdates(OldTime,NewerTime);
            Target.GetData(Now);
            Assert.IsFalse(OnlineDataUpdatedFromOffLine);



        }
        [TestMethod]
        public void IfOnlineDataIfAvailableAndLastUpdatedNullThenShouldUpdateOnlineData()
        {

            OnlineTestData.SetAvailable(true);
            SetLastUpdates(NewerTime,null);
            Target.GetData(Now);
            Assert.IsTrue(OnlineDataUpdatedFromOffLine);



        }

        [TestMethod]
        public void ShouldSaveFlightOnlineDataIfAvailable()
        {

            
            SetLastUpdates(null,OldTime);
            OnlineTestData.SetAvailable(true);
            Target.SaveFlight(new Flight(),NewerTime);
            Assert.AreEqual(NewerTime,OnlineTestData.User.LastUpdated);
            


        }


        [TestMethod]
        public void ShouldSaveFlightToLocalDataIfOnlinenotAvailable() 
        {
           
            OnlineTestData.SetAvailable(false);

            SetLastUpdates(OldTime, OldTime);
            Target.SaveFlight(new Flight(), NewerTime);
            Assert.AreEqual(OldTime, LocalTestData.User.LastUpdated);
        }


        [TestMethod] 
        public void ShouldNotSaveFlightOnlineIfOnlinenotAvailable()
        {

            OnlineTestData.User.LastUpdated = OldTime;
            OnlineTestData.SetAvailable(false);
            Target.SaveFlight(new Flight(), NewerTime);
            Assert.AreEqual(OldTime, OnlineTestData.User.LastUpdated);



        }

        [TestMethod]
        public void ShouldInsertAircraftIfOnlineDataIfAvailable()
        {

            OnlineTestData.User.LastUpdated = OldTime;
            OnlineTestData.SetAvailable(true);
            
            Target.InsertAircraft(new Aircraft(), NewerTime);
            Assert.AreEqual(NewerTime, OnlineTestData.User.LastUpdated);



        }




    }
}
