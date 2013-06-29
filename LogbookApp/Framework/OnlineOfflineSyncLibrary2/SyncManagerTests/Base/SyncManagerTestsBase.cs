using System;
using BaseData;
using OnlineOfflineSyncLibrary.TestMocks;
using OnlineOfflineSyncLibrary2;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{

    public abstract class SyncManagerTestsBase<TSyncManager,TSyncableData, TUser, TOnlineDataService>
        where TUser : IUser, new()
        where TSyncableData : ISyncableData<TUser>,new()
        where TSyncManager : SyncManager<TSyncableData, TOnlineDataService, TUser>, new()
        where TOnlineDataService : MockOnlineDataService<TSyncableData, TUser>, new()
    {
        protected TSyncManager Target;
        protected TOnlineDataService OnlineDataService;
        protected TSyncableData LocalData;
        protected TSyncableData OnlineData;
        protected DateTime NewerTimeStamp = new DateTime(2013, 10, 1);
        protected DateTime OlderTimeStamp = new DateTime(2013, 1, 1);

     

       
        public virtual void Setup()
        {
            OnlineData = new TSyncableData();
            OnlineDataService = new TOnlineDataService{ };
            OnlineDataService.CreateUserData("", DateTime.Now);
            OnlineDataService.UpdateMockInternalData(OnlineData);
            LocalData = new TSyncableData();
            Target = new TSyncManager {  };
        }

     

    
    }
}
