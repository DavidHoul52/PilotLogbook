using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary.TestMocks;

namespace OnlineOfflineSyncLibrary2.DataManagerTests
{
    public abstract class GenericDataManagerTestBase :
        DataManagerTestBase<DataManager<SyncableTestData, TestUser, MockOnlineDataService<SyncableTestData, TestUser>, IOfflineDataService<SyncableTestData, TestUser>, MockSyncManager<SyncableTestData, MockOnlineDataService<SyncableTestData, TestUser>, TestUser>>,
            SyncableTestData, TestUser,
            MockOnlineDataService<SyncableTestData, TestUser>, MockSyncManager<SyncableTestData,
                MockOnlineDataService<SyncableTestData, TestUser>, TestUser>> 
        
        
    {
    }
}