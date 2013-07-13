using System;
using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary.TestMocks
{
    public class MockOnlineDataService<TSyncableData, TUser> : MockBaseDataService<TSyncableData, TUser>, 
        IOnlineDataService<TSyncableData,TUser>
          where TUser : IUser,new()
        where TSyncableData : ISyncableData<TUser>,new()
    {

        public MockOnlineDataService()
            : this("")
        {
            

        }


        public MockOnlineDataService(string userName)
            : base(userName)
        {

            ConnectionTracker = new ConnectionTracker();
        }
    
        


        public async override Task Update<T>(T item)
        {
            
        }

        public async override Task Insert<T>(T item)
        {
            
        }

        public async override Task Delete<T>(T item)
        {
            
        }

        protected override TSyncableData CopyOfInternalData()
        {
            return InternalData;

        }


        protected async override Task InternalUpdateUserTimeStamp(DateTime? timeStamp)
        {
            InternalData.User.TimeStamp = timeStamp;
        }

        public ConnectionTracker ConnectionTracker { get; private set; }
    }
}