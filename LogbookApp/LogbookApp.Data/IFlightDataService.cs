using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IFlightDataService
    {
        List<Flight> Flights { get; set; }
       
        Task GetData(string displayName);
        Task GetLookups();
        Lookups Lookups { get; set; }
        Task<bool> InsertFlight(Flight flight);
     
        Task<bool> DeleteFlight(Flight flight);
        Task<bool> SaveFlight(Flight flight);
        void SaveFlights();
        
        Task InsertAircraft(Aircraft aircraft);
        Task InsertAircraftType(AcType acType);
        Task InsertAirfield(Airfield from);

        Task UpdateAircraft(Aircraft Aircraft);

        Task<bool> DeleteAircraft(Aircraft f);

        Task UpdateAirfield(Airfield Airfield);

        Task<bool> DeleteAirfield(Airfield f);

        Task UpdateAcType(AcType AcType);

        Task InsertAcType(AcType AcType);

        Task<bool> Delete<T1>(T1 item);

        Task InsertUser(User user);


        Task GetUser(string displayName);

        User User { get; }
        bool FlightsChanged { get; set; }
        Task GetFlights();
    }
}
