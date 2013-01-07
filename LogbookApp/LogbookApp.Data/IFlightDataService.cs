using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IFlightDataService
    {
        List<Flight> Flights { get; set; }
        IEnumerable<AcType> AcTypes { get; set; }
        IEnumerable<Airfield> Airfields { get; set; }
        IEnumerable<Capacity> Capacitys { get; set; }
        Task<bool> GetFlights();
        Task GetLookups();
        ILookups Lookups { get; set; }
        Task<bool> InsertFlight(Flight flight);
        Task<bool> UpdateFlight(Flight flight);
        Task<bool> DeleteFlight(Flight flight);
        void SaveFlight(Flight flight);
        void SaveFlights();
    }
}
