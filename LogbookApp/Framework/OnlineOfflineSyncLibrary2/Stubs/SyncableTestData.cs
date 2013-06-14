namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{
    public class SyncableTestData : ISyncableData<TestUser>
    {
        public TestUser User { get; set; }
    }
}