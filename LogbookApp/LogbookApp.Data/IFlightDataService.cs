using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public enum DataType
    {
        None,
        OffLine,
        OnLine
    };


    public interface IFlightDataService
    {
        DataType DataType { get; }

        List<Flight> Flights { get; set; }
       
   
        Task GetLookups();
        Lookups Lookups { get; set; }
        Task<bool> InsertFlight(Flight flight);
     
        Task<bool> DeleteFlight(Flight flight);
        Task<bool> SaveFlight(Flight flight);
      
        
        Task InsertAircraft(Aircraft aircraft);
        Task InsertAircraftType(AcType acType);
        Task InsertAirfield(Airfield from);

        Task UpdateAircraft(Aircraft aircraft);

        Task<bool> DeleteAircraft(Aircraft f);

        Task UpdateAirfield(Airfield airfield);

        Task<bool> DeleteAirfield(Airfield f);

        Task UpdateAcType(AcType acType);

        Task InsertAcType(AcType acType);

        Task<bool> Delete<T1>(T1 item);

        Task InsertUser(User user);


        Task GetUser(string displayName);

        User User { get; }
        bool FlightsChanged { get; set; }
        Task GetFlights();
        Task<bool> Available(string displayName);
        Task UpdateUser(DateTime upDateTime);
    }
}
