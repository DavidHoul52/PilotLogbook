using System;
using BaseData;
using OnlineOfflineSyncLibrary2;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{

    public abstract class SyncManagerTestsBase<TSyncableData, TUser, TOnlineDataService>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>,new()
        where TOnlineDataService : MockOnlineDataService<TSyncableData, TUser> , new()
    {
        protected SyncManager<TSyncableData, TOnlineDataService,
            TUser> Target;
        protected TOnlineDataService OnlineDataService;
        protected TSyncableData SourceData;
        protected TSyncableData TargetData;
        protected DateTime NewerTimeStamp = new DateTime(2013, 10, 1);
        protected DateTime OlderTimeStamp = new DateTime(2013, 1, 1);



       
        public virtual void Setup()
        {
            TargetData = new TSyncableData();
            OnlineDataService = new TOnlineDataService{ };
            SourceData = new TSyncableData();
            Target = new TestSyncManager<TSyncableData, TOnlineDataService,TUser>(OnlineDataService);
        }
    
    

    
    }
}
