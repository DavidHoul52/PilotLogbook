using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataServiceTestsStartupLoadDataServiceLastUpdates : FlightDataServiceTestsBase
    {
        private string _displayName;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            _displayName = "fred";
            TestLocalStorage.SetUserName(_displayName);

        }

     
     

        [TestMethod]
        public void IfOnLineAndLocalDataOlderThenJustLoadOnline()
        {
            MockInternetTools.SetConnected(true);
            OnlineDataService.SetExists(true);
            TestLocalStorage.SetExists(true);
            SetLastUpdatesLocalOnline(OldTime,NewerTime);
            Target.StartUp(_displayName);
            Assert.AreEqual(DataType.OnLine, Target.DataService.DataType);

        }


        [TestMethod]
        public void IfOnLineAndLocalDataNewerLoadLocal()
        {
            base.SetupDataType(DataType.OnLine);
            SetLastUpdatesLocalOnline(NewerTime, OldTime);
            Target.StartUp(_displayName);
            Assert.AreEqual(Target.DataService.DataType, Target.DataService.DataType);

        }

        [TestMethod]
        public void IfOnLineAndLocalDataNewerUpateOnlineFromLocal()
        {
        
            base.SetupDataType(DataType.OnLine);
            SetLastUpdatesLocalOnline(NewerTime, OldTime);
            Target.StartUp(_displayName);
            Assert.IsTrue(MockSyncManager.UpdateOnlineDataCalled);
            

        }


     

      

        //[TestMethod]
        //public void OnlineDataShouldGetUserLastUpdated()  // see also UserManagerTests
        //{
        //    base.SetupDataType(DataType.OnLine);
        //    Target.StartUp(_displayName);
        //    Assert.IsNotNull((OnlineDataService.LastUpdated));


        //}

     

    



        [TestMethod]
        public void IfOnlineDataIfAvailableAndNewerThanLocalThenShouldNotUpdateOnlineData()
        {
            MockInternetTools.SetConnected(true);
            OnlineDataService.SetExists(true);
            SetLastUpdatesLocalOnline(OldTime, NewerTime);
            Target.StartUp(_displayName);
            Assert.IsFalse(MockSyncManager.UpdateOnlineDataCalled);
            



        }
        [TestMethod]
        public void IfOnlineDataAvailableAndLastUpdatedNullThenShouldUpdateOnlineData()
        {
            MockInternetTools.SetConnected(true);
            OnlineDataService.SetExists(true);
            SetLastUpdatesLocalOnline(NewerTime, null);
            Target.StartUp(_displayName);
            Assert.IsTrue(MockSyncManager.UpdateOnlineDataCalled);



        }

        [TestMethod]
        public void IfOnlineDataLastUpdatedNullAndLocalLastUpdatedNullThenNotUpdateOnlineData()
        {
            MockInternetTools.SetConnected(true);
            OnlineDataService.SetExists(true);
            SetLastUpdatesLocalOnline(null, null);
            Target.StartUp(_displayName);
            Assert.IsFalse(MockSyncManager.UpdateOnlineDataCalled);



        }
    
    }
}
