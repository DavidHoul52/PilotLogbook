using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using LogbookApp.Mocks;
using OnlineOfflineSyncLibrary;
using OnlineOfflineSyncLibrary2.DataManagerTests;

namespace LogbookApp.FlightDataManagerTest
{
    public class FlightDataManagerTestBase :
        DataManagerTestBase
            <FlightDataManager<MockOnlineFlightData, IOfflineDataService<FlightData, User>, 
              MockFlightSyncManager>,
                FlightData, User, MockOnlineFlightData, MockFlightSyncManager>
    {
        
    }
}