using LogbookApp.Data;

namespace LogbookApp.FlightDataManagement
{
    public interface ISyncManager
    {
        void UpdateOnlineData(FlightData flightData);
    }
}