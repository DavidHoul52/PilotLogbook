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
    public class DataManager<TUser>
        where TUser: IUser
    {
        private  ISyncableData<TUser> _data;
        private readonly IOnlineDataService<ISyncableData<TUser>,TUser> _onlineDataService;
        private readonly IOfflineDataService<ISyncableData<TUser>,TUser> _offlineDataService;
        private readonly IInternetTools _internet;
        private string _userName;
        private ISyncManager<ISyncableData<TUser>,TUser> _syncManager;
        

        public DataManager(ISyncableData<TUser> data, 
            IOnlineDataService<ISyncableData<TUser>,TUser> onlineDataService,
            IOfflineDataService<ISyncableData<TUser>,TUser> offlineDataService, IInternetTools internet)
        {
            _data = data;
            _onlineDataService = onlineDataService;
            _offlineDataService = offlineDataService;
            _internet = internet;
        }

        public async void Startup(string userName)
        {
            _userName = userName;
            await CheckConnectionState(true);
     
        }

        public async Task PerformDataUpdateAction(Func<IOnlineDataService<ISyncableData<TUser>,TUser>,
            Task> updateAction, IEntity entity,DateTime upDateTime)
        {
            entity.TimeStamp = upDateTime;

            await CheckConnectionState(false);
            _data.User.TimeStamp = upDateTime;
            if (_onlineDataService.IsConnected)
            {
                await updateAction(_onlineDataService);
                
            }

            await (_offlineDataService.SaveLocalData(_data));


        }

        private async Task CheckConnectionState(bool isStartup)
        {
            _onlineDataService.IsConnected = _internet.IsConnected;
            if (_onlineDataService.IsConnected)
            {
                
                var unSyncedData=await LoadUserData(_onlineDataService);
                if (DetectNeedForSyncUpdate(_data.User.TimeStamp,unSyncedData.User.TimeStamp))
                    await _syncManager.UpdateTargetData(_data,unSyncedData, DateTime.Now);
            }
            else if (isStartup)
            {
                _data =await LoadUserData(_offlineDataService);
            }
        }

        private async Task<ISyncableData<TUser>> LoadUserData(IDataService<ISyncableData<TUser>, TUser> dataService
            )
        {
            bool userDataExists = await dataService.GetUserDataExists(_userName);
            if (userDataExists)
            {
               return await dataService.LoadUserData(_userName); 
                                                                 
                
              
                
            }
            else
            {
                return await dataService.CreateUserData(_userName);  
                
            }

            
        }

     
        private bool DetectNeedForSyncUpdate(DateTime? sourceLastUpdated, DateTime? targetLastUpdated)
        {
            return (targetLastUpdated == null && sourceLastUpdated != null)
                  || (sourceLastUpdated > targetLastUpdated);
        }
    }
}
