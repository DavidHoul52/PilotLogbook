using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary.TestMocks;

namespace OnlineOfflineSyncLibrary2.DataManagerTests
{
    public abstract class GenericDataManagerTestBase :
        DataManagerTestBase<
        // DataManager
        DataManager<SyncableTestData,
                    TestUser,
                    MockOnlineDataService<SyncableTestData, TestUser>,
                    MockOfflineDataService<SyncableTestData, TestUser>,
                    MockSyncManager<SyncableTestData, MockOnlineDataService<SyncableTestData, TestUser>,
                    TestUser>,IDataUpdateActions>,
        
        SyncableTestData,
        TestUser,
        MockOnlineDataService<SyncableTestData, TestUser>,
        MockOfflineDataService<SyncableTestData, TestUser>,
        MockSyncManager<SyncableTestData,MockOnlineDataService<SyncableTestData, TestUser>, TestUser>,
        IDataUpdateActions
        
        > 
        
        
    {
    }
}