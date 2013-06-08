using System;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;

namespace LogbookApp.FlightDataManagerTest
{
    public class MockSyncManager : ISyncManager
    {
        public bool UpdateOnlineDataCalled { get; private set; }

        public async Task UpdateOnlineData(FlightData flightData, DateTime now)
        {
            UpdateOnlineDataCalled = true;
        }

      
    }
}