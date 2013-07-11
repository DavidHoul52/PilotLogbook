using LogbookApp.Data;
using LogbookApp.Mocks;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;
using OnlineOfflineSyncLibrary.TestMocks;

namespace LogbookApp.FlightDataManagerTest
{
    public class MockFlightSyncManager : MockSyncManager<FlightData,
        MockOnlineFlightData,User>
    {
    }
}