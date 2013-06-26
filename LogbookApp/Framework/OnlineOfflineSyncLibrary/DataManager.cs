using System;
using System.Threading.Tasks;
using BaseData;
using InternetDetection;

namespace OnlineOfflineSyncLibrary
{
    public class DataManager<TSyncableData,TUser, TOnlineDataService, TOffLineDataService>
        where TUser: IUser
        where TSyncableData : ISyncableData<TUser>
        where TOnlineDataService : IOnlineDataService<TSyncableData, TUser>
        where TOffLineDataService : IOfflineDataService<TSyncableData, TUser>
    {

        private readonly TOnlineDataService _onlineDataService;
        private readonly TOffLineDataService _offlineDataService;
        private readonly IInternetTools _internet;
        private string _userName;
        private readonly ISyncManager<TSyncableData,TOnlineDataService, TUser> _syncManager;


        public DataManager(
            TOnlineDataService onlineDataService,
            TOffLineDataService offlineDataService, IInternetTools internet,
            ISyncManager<TSyncableData, TOnlineDataService,TUser> syncManager)
        {
            
            _onlineDataService = onlineDataService;
            _offlineDataService = offlineDataService;
            _internet = internet;
            _syncManager = syncManager;
        }

        public TSyncableData Data { get; private set; }

        public async Task Startup(string userName)
        {
            _userName = userName;
            await CheckConnectionState();
     
        }

     

        private async Task CheckConnectionState()
        {
            if (!_offlineDataService.Loaded)
                Data =await LoadUserData(_offlineDataService);

            _onlineDataService.IsConnected = _internet.IsConnected;
            if (_onlineDataService.IsConnected)
            {
                
                var unSyncedData=await LoadUserData(_onlineDataService);
                if (DetectNeedForSyncUpdate(Data, unSyncedData))
                    await _syncManager.UpdateTargetData(_onlineDataService, Data, unSyncedData, DateTime.Now);
                else
                    Data = unSyncedData;


            }
      
        }

        protected async Task<TSyncableData> LoadUserData(IDataService<TSyncableData,TUser> dataService)
        {
            bool userDataExists = await dataService.GetUserDataExists(_userName);
            if (userDataExists)
               return await dataService.LoadUserData(_userName); 
            return await dataService.CreateUserData(_userName, null);  
                

            
        }

     
        private bool DetectNeedForSyncUpdate(TSyncableData source, TSyncableData target)
        {
            if (source.Equals(default(TSyncableData)) || target.Equals(default(TSyncableData)))
                return false;
            return ((target.User.TimeStamp==null &&source.User.TimeStamp != null )
                  || (source.User.TimeStamp > target.User.TimeStamp));
        }

        public async Task PerformDataUpdateAction(Func<TOnlineDataService,
         Task> updateAction, IEntity entity, DateTime upDateTime)
        {
            entity.TimeStamp = upDateTime;

            await CheckConnectionState();
            Data.User.TimeStamp = upDateTime;
            if (_onlineDataService.IsConnected)
            {
                await updateAction(_onlineDataService);

            }

            await (_offlineDataService.SaveLocalData(Data));


        }
    }
}
