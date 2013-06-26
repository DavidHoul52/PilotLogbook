using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LogbookApp.Data;
using OnlineOfflineSyncLibrary.Test.SyncManagerTests;

namespace LogbookApp.FlightDataManagerTest.SyncManagerTests
{
    public class MockOnlineFlightData : MockOnlineDataService<FlightData, User>, IOnlineFlightData
    {
      

        public MockOnlineFlightData() : base("")
        {
        }

       public async Task InsertFlight(Flight flight)
        {
            
        }

       public async Task DeleteFlight(Flight flight)
        {
            
        }

       public async Task SaveFlight(Flight flight)
        {
            
        }

       public async Task InsertAircraft(Aircraft aircraft)
        {
            
        }

       public async Task InsertAircraftType(AcType acType)
        {
            
        }

       public async Task InsertAirfield(Airfield @from)
        {
            
        }

       public async Task UpdateAircraft(Aircraft aircraft)
        {
            
        }

       public async Task DeleteAircraft(Aircraft f)
        {
            
        }

       public async Task UpdateAirfield(Airfield airfield)
        {
            
        }

       public async Task DeleteAirfield(Airfield f)
        {
            
        }

       public async Task UpdateAcType(AcType acType)
        {
            
        }

       public async Task InsertAcType(AcType acType)
        {
            
        }

       public async Task DeleteAcType(AcType acType)
        {
            
        }

       public async Task<ObservableCollection<Flight>> GetFlights(int userId)
       {
           return InternalData.Flights;
       }

       public async Task<Lookups> LoadLookups(int userId)
       {
           return InternalData.Lookups;
       }

        protected async override Task InternalCreateUserData(string userName)
        {
            base.InternalCreateUserData(userName);
            
        }

        public async override Task Insert<T>(T item)
        {
            if (typeof(T)==typeof(Aircraft))
              InternalData.AddAircraft(item as Aircraft);
            if (typeof(T) == typeof(Airfield))
                InternalData.AddAirfield(item as Airfield);
            if (typeof(T) == typeof(Flight))
                InternalData.AddFlight(item as Flight);
        }
    }
}