using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataServiceTestsInitialLoad : FlightDataServiceTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            
        }

        [TestMethod]
        public void IfOfflineAndNoLocalThenReturnFalse()
        {

            OnlineTestData.SetAvailable(false);
            TestLocalStorage.SetExists(false);
            var result=Target.GetData(Now);
            Assert.IsFalse(result.Result);
            
        }

        [TestMethod]
        public void IfOfflineAndLocalThenReturnTrue()
        {

            OnlineTestData.SetAvailable(false);
            TestLocalStorage.SetExists(true);
            var result = Target.GetData(Now);
            Assert.IsTrue(result.Result);

        }

        [TestMethod]
        public void IfOfflineAndLocalDataTypeOffline()
        {

            OnlineTestData.SetAvailable(false);
            TestLocalStorage.SetExists(true);
            Target.GetData(Now);
            Assert.AreEqual(DataType.OffLine,Target.DataType);

        }

        [TestMethod]
        public void IfOnLineDataTypeOnline()
        {

            OnlineTestData.SetAvailable(true);
            TestLocalStorage.SetExists(true);
            Target.GetData(Now);
            Assert.AreEqual(DataType.OnLine, Target.DataType);

        }

        [TestMethod]
        public void IfOnLineAndLocalDataOlderThenJustLoadOnline()
        {

            OnlineTestData.SetAvailable(true);
            TestLocalStorage.SetExists(true);
            SetLastUpdates(OldTime,NewerTime);
            Target.GetData(Now);
            Assert.AreEqual(DataType.OnLine, Target.DataType);

        }


        [TestMethod]
        public void IfOnLineAndLocalDataNewerLoadLocal()
        {

            OnlineTestData.SetAvailable(true);
            TestLocalStorage.SetExists(true);
            SetLastUpdates(NewerTime, OldTime);
            Target.GetData(Now);
            Assert.AreEqual(DataType.OffLine, Target.DataType);

        }

        [TestMethod]
        public void IfOnLineAndLocalDataNewerUpateOnlineFromLocal()
        {

            OnlineTestData.SetAvailable(true);
            TestLocalStorage.SetExists(true);
            SetLastUpdates(NewerTime, OldTime);
            Target.GetData(Now);
            Assert.IsTrue(OnlineDataUpdatedFromOffLine);
            

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
        public void IfOnlineDataIfAvailableAndNewerThanLocalThenShouldNotUpdateOnlineData()
        {

            OnlineTestData.SetAvailable(true);
            SetLastUpdates(OldTime, NewerTime);
            Target.GetData(Now);
            Assert.IsFalse(OnlineDataUpdatedFromOffLine);



        }
        [TestMethod]
        public void IfOnlineDataAvailableAndLastUpdatedNullThenShouldUpdateOnlineData()
        {

            OnlineTestData.SetAvailable(true);
            SetLastUpdates(NewerTime, null);
            Target.GetData(Now);
            Assert.IsTrue(OnlineDataUpdatedFromOffLine);



        }
    
    }
}
