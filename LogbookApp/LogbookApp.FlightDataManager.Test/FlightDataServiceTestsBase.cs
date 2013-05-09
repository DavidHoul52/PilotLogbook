using System;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using LogbookApp.Mocks;
using LogbookApp.Storage;

namespace LogbookApp.FlightDataManagerTest
{
    public class FlightDataServiceTestsBase
    {
        protected FlightDataManager Target;
        protected MockFlightDataService OnlineTestData;
        protected MockLocalDataManager LocalTestData;
        protected bool OnlineDataUpdatedFromOffLine;
        protected DateTime OldTime;
        protected DateTime NewerTime;
        protected DateTime Now;
        protected TestLocalStorage TestLocalStorage;
        protected User User;
        
        public virtual void Setup()
        {
            OnlineTestData = new MockFlightDataService(DataType.OnLine);
            
            TestLocalStorage = new TestLocalStorage { };
            TestLocalStorage.SetExists(true);
            LocalTestData = new MockLocalDataManager(TestLocalStorage, "", "", "");
            OnlineDataUpdatedFromOffLine = false;
            User = new User();
            OldTime = new DateTime(2012, 1, 1);
            NewerTime = new DateTime(2013, 1, 1);
            Now = new DateTime(2013, 5, 5);
            Target = new FlightDataManager(OnlineTestData, LocalTestData, "david");
        }

        protected void SetLastUpdates(DateTime? local, DateTime? online)
        {
            LocalTestData.LastUpdated = local ;
            OnlineTestData.LastUpdated = online;
        }
    }
}
