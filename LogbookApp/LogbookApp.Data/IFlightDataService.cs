using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.Data
{
    public interface IFlightDataService : IDataUpdateActions
    {
        Task InsertFlight(Flight flight);
        Task DeleteFlight(Flight flight);
        Task UpdateFlight(Flight flight);
        Task InsertAircraft(Aircraft aircraft);
        Task InsertAircraftType(AcType acType);
        Task InsertAirfield(Airfield from);
        Task UpdateAircraft(Aircraft aircraft);
        Task DeleteAircraft(Aircraft f);
        Task UpdateAirfield(Airfield airfield);
        Task DeleteAirfield(Airfield f);
        Task UpdateAcType(AcType acType);
        Task InsertAcType(AcType acType);

        Task DeleteAcType(AcType acType);

        Task<ObservableCollection<Flight>> GetFlights(int userId);
        Task<Lookups> LoadLookups(int userId);
    }
}