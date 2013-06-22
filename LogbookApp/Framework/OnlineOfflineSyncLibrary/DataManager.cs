using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using BaseData;
using InternetDetection;

namespace OnlineOfflineSyncLibrary
{
    public class DataManager<TSyncableData,TUser>
        where TUser: IUser
        where TSyncableData : ISyncableData<TUser>
    {
        private TSyncableData _data;
        private readonly IOnlineDataService<TSyncableData, TUser> _onlineDataService;
        private readonly IOfflineDataService<TSyncableData, TUser> _offlineDataService;
        private readonly IInternetTools _internet;
        private string _userName;
        private ISyncManager<TSyncableData, TUser> _syncManager;


        public DataManager(
            IOnlineDataService<TSyncableData, TUser> onlineDataService,
            IOfflineDataService<TSyncableData, TUser> offlineDataService, IInternetTools internet, 
            ISyncManager<TSyncableData,TUser>  syncManager)
        {
            
            _onlineDataService = onlineDataService;
            _offlineDataService = offlineDataService;
            _internet = internet;
            _syncManager = syncManager;
        }

        public async void Startup(string userName)
        {
            _userName = userName;
            await CheckConnectionState();
     
        }

     

        private async Task CheckConnectionState()
        {
            _onlineDataService.IsConnected = _internet.IsConnected;
            if (_onlineDataService.IsConnected)
            {
                
                var unSyncedData=await LoadUserData(_onlineDataService);
                if (DetectNeedForSyncUpdate(_data, unSyncedData))
                    await _syncManager.UpdateTargetData(_data, unSyncedData, DateTime.Now);
                else
                    _data = unSyncedData;


            }
            else if (!_offlineDataService.Loaded)
            {
                _data =await LoadUserData(_offlineDataService);
            }
        }

        private async Task<TSyncableData> LoadUserData(IDataService<TSyncableData,TUser> dataService)
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

        public async Task PerformDataUpdateAction(Func<IOnlineDataService<TSyncableData, TUser>,
         Task> updateAction, IEntity entity, DateTime upDateTime)
        {
            entity.TimeStamp = upDateTime;

            await CheckConnectionState();
            _data.User.TimeStamp = upDateTime;
            if (_onlineDataService.IsConnected)
            {
                await updateAction(_onlineDataService);

            }

            await (_offlineDataService.SaveLocalData(_data));


        }
    }
}
