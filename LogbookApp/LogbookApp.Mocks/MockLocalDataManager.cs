using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.Storage;

namespace LogbookApp.Mocks
{
    public class MockLocalDataManager : LocalDataService
    {
        public MockLocalDataManager(ILocalStorage localStorage, string flightsFileName, 
            string lookupsFileName, string userFileName) : 
            base(localStorage, flightsFileName, lookupsFileName, userFileName)
        {
        }

        public async override  Task GetUser(string displayName)
        {
            if (User == null)
               await base.GetUser(displayName);

        }

        public void SetUser(User user)
        {
            User = user;

        }
    }

}
