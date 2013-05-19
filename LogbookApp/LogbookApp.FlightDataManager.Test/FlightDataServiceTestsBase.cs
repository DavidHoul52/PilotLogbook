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
        protected MockFlightDataService OnlineDataService;
        protected MockLocalDataManager LocalTestData;
        protected bool OnlineDataUpdatedFromOffLine;
        protected DateTime OldTime;
        protected DateTime NewerTime;
        protected DateTime Now;
        protected TestLocalStorage TestLocalStorage;
        protected User User;
        protected MockSyncManager MockSyncManager;
        protected FlightData OnLineDataServiceFlightData;
        
        public virtual void Setup()
        {
            OnLineDataServiceFlightData = new FlightData();
            OnlineDataService = new MockFlightDataService(DataType.OnLine, OnLineDataServiceFlightData);
            
            TestLocalStorage = new TestLocalStorage { };
            TestLocalStorage.SetExists(true);
            LocalTestData = new MockLocalDataManager(TestLocalStorage, "", "", "");
            OnlineDataUpdatedFromOffLine = false;
            User = new User();
            OldTime = new DateTime(2012, 1, 1);
            NewerTime = new DateTime(2013, 1, 1);
            Now = new DateTime(2013, 5, 5);
            MockSyncManager = new MockSyncManager();
            Target = new FlightDataManager(OnlineDataService, LocalTestData, "david", MockSyncManager);
        }

        protected void SetLastUpdates(DateTime? local, DateTime? online)
        {
            LocalTestData.LastUpdated = local ;
            OnlineDataService.LastUpdated = online;
        }
    }
}
