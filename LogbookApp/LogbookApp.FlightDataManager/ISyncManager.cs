using System;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.FlightDataManagement
{
    public interface ISyncManager
    {
        Task UpdateOnlineData(FlightData flightData, DateTime now);
      
    }
}