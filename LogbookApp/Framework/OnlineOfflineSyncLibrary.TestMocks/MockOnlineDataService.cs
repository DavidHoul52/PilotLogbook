using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary.TestMocks
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



        public async override Task Update<T>(T item)
        {
            
        }

        public async override Task Insert<T>(T item)
        {
            
        }

        public async override Task Delete<T>(T item)
        {
            
        }

       

        protected async override Task InternalUpdateUserTimeStamp(DateTime? timeStamp)
        {
            
        }
    }
}