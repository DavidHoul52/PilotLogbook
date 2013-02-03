using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IFlightDataService
    {
        List<Flight> Flights { get; set; }
       
        Task<bool> GetFlights();
        Task GetLookups();
        ILookups Lookups { get; set; }
        Task<bool> InsertFlight(Flight flight);
        Task<bool> UpdateFlight(Flight flight);
        Task<bool> DeleteFlight(Flight flight);
        void SaveFlight(Flight flight);
        void SaveFlights();
        void SaveTest();
        Task InsertAircraft(Aircraft aircraft);
    }
}
