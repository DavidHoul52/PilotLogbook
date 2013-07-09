using System;
using System.Threading.Tasks;
using BaseData;
using InternetDetection;

namespace OnlineOfflineSyncLibrary
{
    public class DataManager<TSyncableData,TUser, TOnlineDataService, TOffLineDataService
        , TSyncManager>
        where TUser: IUser
        where TSyncableData : ISyncableData<TUser>
        where TOnlineDataService : IOnlineDataService<TSyncableData, TUser>
        where TOffLineDataService : IOfflineDataService<TSyncableData, TUser>
        where TSyncManager : ISyncManager<TSyncableData,TOnlineDataService, TUser> 
    {

        private TOnlineDataService _onlineDataService;
        private TOffLineDataService _offlineDataService;
        private IInternetTools _internet;
        private string _userName;
        private  TSyncManager _syncManager;

        public DataManager()
        {
            
        }

        public DataManager(TOnlineDataService onlineDataService,
            TOffLineDataService offlineDataService, IInternetTools internet,
            TSyncManager syncManager
            )
        {
            SetConstructorParams(onlineDataService,offlineDataService,internet,syncManager);
          
        }

        public void SetConstructorParams(TOnlineDataService onlineDataService,
            TOffLineDataService offlineDataService, IInternetTools internet,
            TSyncManager syncManager)
        {
            _onlineDataService = onlineDataService;
            _offlineDataService = offlineDataService;
            _internet = internet;
            _syncManager = syncManager;
        }
        public TSyncableData Data { get; protected set; }

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
                if (DetectNeedForSyncUpdate(Data, unSyncedData)) // TODO: this should part of the sync?
                    await _syncManager.UpdateOnlineData(_onlineDataService, Data,unSyncedData, DateTime.Now);
                else
                    Data = unSyncedData; // TODO: hmmm.....


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
