using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LogbookApp.Data;
using OnlineOfflineSyncLibrary.TestMocks;

namespace LogbookApp.FlightDataManagerTest
{
    public class MockOffLineFlightData : MockOfflineDataService<FlightData, User>,IFlightDataService
    {
        public Task InsertFlight(Flight flight)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteFlight(Flight flight)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveFlight(Flight flight)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAircraft(Aircraft aircraft)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAircraftType(AcType acType)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAirfield(Airfield @from)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAircraft(Aircraft aircraft)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAircraft(Aircraft f)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAirfield(Airfield airfield)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAirfield(Airfield f)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAcType(AcType acType)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAcType(AcType acType)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAcType(AcType acType)
        {
            throw new System.NotImplementedException();
        }

        public Task<ObservableCollection<Flight>> GetFlights(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Lookups> LoadLookups(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}