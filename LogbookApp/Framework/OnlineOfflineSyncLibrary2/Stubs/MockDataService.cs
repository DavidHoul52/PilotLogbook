using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{
    public class MockDataService : IDataService
    {
        private readonly ISyncableData<TestUser> _targetData;
        private readonly string _userName;

        public MockDataService(DataType dataType, ISyncableData<TestUser> targetData, string userName)
        {
            _targetData = targetData;
            _userName = userName;
            DataType = dataType;
        }

        public DataType DataType { get; private set; }
        

        public async Task Update<T>(T item) where T : IEntity
        {
            
        }

        public async Task Insert<T>(T item) where T : IEntity
        {
            
        }

        public async Task Delete<T>(T item) where T : IEntity
        {
            
        }

      

        public void CreateUserData(string userName)
        {
            CreateUserDataCalled = true;
        }

        public bool LoadUserData(string userName)
        {
           LoadUserDataCalled = true;
           return _targetData.User != null;



        }

        public void SetupGetUser(TestUser testUser)
        {
            _targetData.User = testUser;
        }

        public bool LoadUserDataCalled { get; set; }
        public bool CreateUserDataCalled { get; set; }
    }
}