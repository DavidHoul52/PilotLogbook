using System;
using BaseData;
using OnlineOfflineSyncLibrary2;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{
  
    public abstract class SyncManagerTestsBase
    {
        protected SyncManager<SyncableTestData, MockDataService, TestUser> Target;
        protected MockDataService OnlineDataService;
        protected SyncableTestData SourceData;
        protected SyncableTestData TargetData;
        protected DateTime NewerTimeStamp = new DateTime(2013, 10, 1);
        protected DateTime OlderTimeStamp = new DateTime(2013, 1, 1);



       
        public virtual void Setup()
        {
            TargetData = new SyncableTestData();
            OnlineDataService = new MockDataService(DataType.OnLine, TargetData, "");
            SourceData = new SyncableTestData();
            Target = new TestSyncManager(OnlineDataService);
        }
    
    

    
    }
}
