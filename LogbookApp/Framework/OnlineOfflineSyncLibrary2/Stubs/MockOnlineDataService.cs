using System;
using System.Threading.Tasks;
using BaseData;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{
    public class MockOnlineDataService : MockBaseDataService, IOnlineDataService<SyncableTestData, TestUser>
    {


        public MockOnlineDataService(string userName)
            : base(userName)
        {
          
            
        }
    
        public bool IsConnected { get; set; }


        

      
        public DateTime? LastUpdated { get; set; }

       
    }
}