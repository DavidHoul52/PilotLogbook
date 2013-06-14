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
        private readonly IDataService _onlineDataService;
        private readonly IDataService _offlineDataService;
        private readonly IInternetTools _internet;

        public DataManager(ISyncableData<TUser> data, IDataService onlineDataService,
            IDataService offlineDataService, IInternetTools internet)
        {
            _onlineDataService = onlineDataService;
            _offlineDataService = offlineDataService;
            _internet = internet;
        }

        public void Startup(string userName)
        {
            if (_internet.IsConnected)
            {
                if (!_onlineDataService.LoadUserData(userName))
                    _onlineDataService.CreateUserData(userName);
            }
            else
            {
                if (!_offlineDataService.LoadUserData(userName))
                    _offlineDataService.CreateUserData(userName);
                
            }
        }

        
    }
}
