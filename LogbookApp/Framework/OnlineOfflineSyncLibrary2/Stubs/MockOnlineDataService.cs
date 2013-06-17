using System.Threading.Tasks;
using BaseData;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{
    public class MockOnlineDataService : MockBaseDataService, IOnlineDataService<TestUser>
    {
    

        public MockOnlineDataService( ISyncableData<TestUser> targetData, string userName) :base(targetData,userName)
        {
          
            
        }
    
        public bool IsConnected { get; set; }
      
    }
}