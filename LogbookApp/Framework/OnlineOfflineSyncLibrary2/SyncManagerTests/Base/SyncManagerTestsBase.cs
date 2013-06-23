using System;
using BaseData;
using OnlineOfflineSyncLibrary2;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{

    public abstract class SyncManagerTestsBase<TSyncableData, TUser, TOnlineDataService, TOfflineDataService>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>,new()
        where TOnlineDataService : MockOnlineDataService<TSyncableData, TUser> 
    {
        protected SyncManager<TSyncableData, MockOnlineDataService<TSyncableData, TUser>,
            TUser> Target;
        protected MockOnlineDataService<TSyncableData, TUser> OnlineDataService;
        protected TSyncableData SourceData;
        protected TSyncableData TargetData;
        protected DateTime NewerTimeStamp = new DateTime(2013, 10, 1);
        protected DateTime OlderTimeStamp = new DateTime(2013, 1, 1);



       
        public virtual void Setup()
        {
            TargetData = new TSyncableData();
            OnlineDataService = new MockOnlineDataService<TSyncableData, TUser>("");
            SourceData = new TSyncableData();
            Target = new TestSyncManager<TSyncableData, TUser>(OnlineDataService);
        }
    
    

    
    }
}
