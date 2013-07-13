using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary.TestMocks
{
    public class MockOfflineDataService<TSyncableData, TUser> : MockBaseDataService<TSyncableData, TUser>,
        IOfflineDataService<TSyncableData, TUser>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>, new()
    {
        public MockOfflineDataService()
            : base( "")
        {
        }

      


        public DateTime? LastUpdated { get; set; }
        public bool LocalDataSaved { get; set; }




        public async Task SaveLocalData(TSyncableData data)
        {
            LocalDataSaved = true;
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
    }
}
