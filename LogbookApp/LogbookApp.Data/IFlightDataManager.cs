using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IFlightDataManager
    {
        FlightData FlightData { get; set; }
        DataType DataType { get; }
        bool FlightsChanged { get; set; }
        Task<bool> GetData(DateTime now);
        Task GetLookups();
        Task InsertFlight(Flight flight,DateTime updateTime);
        Task DeleteFlight(Flight flight, DateTime updateTime);
        Task SaveFlight(Flight flight, DateTime saveTime);
        Task InsertAircraft(Aircraft aircraft, DateTime upDateTime);
        Task InsertAirfield(Airfield @from, DateTime upDateTime);
        Task UpdateAircraft(Aircraft aircraft, DateTime upDateTime);
        Task DeleteAircraft(Aircraft aircraft, DateTime upDateTime);
        Task UpdateAirfield(Airfield airfield, DateTime upDateTime);
        Task DeleteAirfield(Airfield airfield, DateTime upDateTime);
        Task UpdateAcType(AcType acType, DateTime upDateTime);
        Task InsertAcType(AcType acType, DateTime upDateTime);
        Task InsertUser(User user, DateTime upDateTime);
        Task GetUser();
        Task GetFlights();
        Task<bool> Available();

        Task DeleteAcType(AcType item, DateTime upDateTime);
    }
}