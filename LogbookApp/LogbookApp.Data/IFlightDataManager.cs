using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IFlightDataManager
    {
        DataType DataType { get; }
        bool FlightsChanged { get; set; }
        Task<bool> GetData(DateTime now);
        Task GetLookups();
        Task<bool> InsertFlight(Flight flight,DateTime updateTime);
        Task<bool> DeleteFlight(Flight flight, DateTime updateTime);
        Task<bool> SaveFlight(Flight flight, DateTime saveTime);
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