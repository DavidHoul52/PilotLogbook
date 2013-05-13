using LogbookApp.Data;
using LogbookApp.FlightDataManagement;

namespace LogbookApp.FlightDataManagerTest
{
    public class MockSyncManager : ISyncManager
    {
        public bool UpdateOnlineDataCalled { get; private set; }

        public void UpdateOnlineData(FlightData flightData)
        {
            UpdateOnlineDataCalled = true;
        }
    }
}