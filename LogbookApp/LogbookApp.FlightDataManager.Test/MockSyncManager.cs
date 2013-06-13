using System;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.FlightDataManagerTest
{
    public class MockSyncManager<T> : ISyncManager<T>
        where T:ISyncableData
    {
        public bool UpdateOnlineDataCalled { get; private set; }

      


        public async Task UpdateOnlineData(T sourceFlightData, DateTime now)
        {
            UpdateOnlineDataCalled = true;
        }
    }
}