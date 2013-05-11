using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        Task<Lookups> GetLookups(int userId);
        Task<User> GetUser(string displayName);
        Task<ObservableCollection<Flight>> GetFlights(int userId);
        Task InsertFlight(Flight flight);
        Task DeleteFlight(Flight flight);
        Task SaveFlight(Flight flight);
        Task InsertAircraft(Aircraft aircraft);
        Task InsertAircraftType(AcType acType);
        Task InsertAirfield(Airfield from);
        Task UpdateAircraft(Aircraft aircraft);
        Task DeleteAircraft(Aircraft f);
        Task UpdateAirfield(Airfield airfield);
        Task DeleteAirfield(Airfield f);
        Task UpdateAcType(AcType acType);
        Task InsertAcType(AcType acType);
        Task InsertUser(User user);
        
        bool FlightsChanged { get; set; }
        DateTime? LastUpdated { get; set; }

        Task<bool> Available(string displayName);
        Task UpdateUser(User user);


        Task DeleteAcType(AcType acType);
      
    }
}
