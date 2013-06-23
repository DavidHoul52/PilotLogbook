using System;
using System.Threading.Tasks;
using BaseData;
using OnlineOfflineSyncLibrary2.Stubs;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{
    public class MockOnlineDataService<TSyncableData, TUser> : MockBaseDataService<TSyncableData, TUser>, 
        IOnlineDataService<TSyncableData,TUser>
          where TUser : IUser,new()
        where TSyncableData : ISyncableData<TUser>,new()
    {


        public MockOnlineDataService(string userName)
            : base(userName)
        {
          
            
        }
    
        public bool IsConnected { get; set; }


        

      
        public DateTime? LastUpdated { get; set; }

       
    }
}