using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;
using InternetDetection;

namespace OnlineOfflineSyncLibrary
{
    public class DataManager<TUser>
        where TUser: IUser
    {
        private readonly ISyncableData<TUser> _data;
        private readonly IOnlineDataService<ISyncableData<TUser>,TUser> _onlineDataService;
        private readonly IOfflineDataService<ISyncableData<TUser>,TUser> _offlineDataService;
        private readonly IInternetTools _internet;
        private string _userName;
        private ISyncManager<ISyncableData<TUser>,TUser> _syncManager;
        private DataServiceState _onLineState;
        private DataServiceState _offLineState;

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
            await CheckConnectionState();
     
        }

        public async Task PerformDataUpdateAction(Func<IOnlineDataService<ISyncableData<TUser>,TUser>,
            Task> updateAction, IEntity entity,DateTime upDateTime)
        {
            entity.TimeStamp = upDateTime;

            await CheckConnectionState();
            _data.User.TimeStamp = upDateTime;
            if (_onlineDataService.IsConnected)
            {
                await updateAction(_onlineDataService);
               // await _onlineDataService.UpdateUser(FlightData.User);
            }

            await (_offlineDataService.SaveLocalData(_data));
           // await LocalDataService.UpdateUser(FlightData.User);




        }

        private async Task CheckConnectionState()
        {
            _onlineDataService.IsConnected = _internet.IsConnected;
            if (_onlineDataService.IsConnected)
            {
                // although DataServiceState belongs to dataservice we want to check and manipulate it here
                _onLineState=await GetState(_onlineDataService);
                if (DetectNeedForSyncUpdate())
                    await _syncManager.UpdateTargetData(_data, DateTime.Now); // TODO: source and target data will be different  
            }
            else
            {
                _offLineState = await GetState(_offlineDataService);
            }
        }

        private async Task<DataServiceState> GetState(IDataService<ISyncableData<TUser>,TUser> dataService
            )
        {
            DataServiceState state = await dataService.GetServiceState(_userName);
            if (state.UserDataExists)
            {
                await dataService.LoadUserData(_userName, _data);
                state.LastUpdated = _data.User.TimeStamp;
            }
            else
            {
                await dataService.CreateUserData(_userName);
                state.UserDataExists = true;
                state.LastUpdated = DateTime.Now;
            }
        }

     
        private bool DetectNeedForSyncUpdate()
        {
            return (_onLineState.LastUpdated == null && _offLineState.LastUpdated != null)
                  || (_offLineState.LastUpdated > _onLineState.LastUpdated);
        }
    }
}
