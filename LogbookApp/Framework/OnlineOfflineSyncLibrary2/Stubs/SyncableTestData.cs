using BaseData;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{
    public class SyncableTestData : ISyncableData<TestUser>
    {
        public SyncableTestData()
        {
            User = new TestUser();
        }

        public TestUser User { get; set; }
        public bool CanDelete<T>(T item) where T : IEntity
        {
            return true;
        }
    }
}