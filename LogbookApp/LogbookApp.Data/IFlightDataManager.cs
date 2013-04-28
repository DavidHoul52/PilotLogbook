using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IFlightDataManager
    {
        DataType DataType { get; }
        List<Flight> Flights { get; set; }
        Lookups Lookups { get; set; }
        User User { get; }
        bool FlightsChanged { get; set; }
        Task GetData();
        Task GetLookups();
        Task<bool> InsertFlight(Flight flight);
        Task<bool> DeleteFlight(Flight flight);
        Task<bool> SaveFlight(Flight flight, DateTime saveTime);
        void SaveFlights();
        Task InsertAircraft(Aircraft aircraft, DateTime upDateTime);
        Task InsertAircraftType(AcType acType,DateTime upDateTime);
        Task InsertAirfield(Airfield @from, DateTime upDateTime);
        Task UpdateAircraft(Aircraft aircraft, DateTime upDateTime);
        Task<bool> DeleteAircraft(Aircraft aircraft, DateTime upDateTime);
        Task UpdateAirfield(Airfield airfield, DateTime upDateTime);
        Task<bool> DeleteAirfield(Airfield airfield, DateTime upDateTime);
        Task UpdateAcType(AcType acType, DateTime upDateTime);
        Task InsertAcType(AcType acType, DateTime upDateTime);
        Task InsertUser(User user, DateTime upDateTime);
        Task GetUser();
        Task GetFlights();
        Task<bool> Available();
        Task<bool> Delete<T>(T item, DateTime deleteTime);
    }
}