using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LogbookApp.Data;
using OnlineOfflineSyncLibrary.TestMocks;

namespace LogbookApp.Mocks
{
    public class MockOnlineFlightData : MockOnlineDataService<FlightData, User>, IOnlineFlightData
    {
      

        public MockOnlineFlightData() : base("")
        {
        }

       public async Task InsertFlight(Flight flight)
       {
           await Insert(flight);
       }

       public async Task DeleteFlight(Flight flight)
       {
           await Delete(flight);
       }

       public async Task SaveFlight(Flight flight)
        {
            
        }

       public async Task InsertAircraft(Aircraft aircraft)
       {
           await Insert(aircraft);
       }

       public async Task InsertAircraftType(AcType acType)
        {
            
        }

       public async Task InsertAirfield(Airfield @from)
       {
           await Insert(@from);
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
            if (typeof(T) == typeof(AcType))
                InternalData.AddAcType(item as AcType);
            if (typeof(T) == typeof(Flight))
                InternalData.AddFlight(item as Flight);
        }

        public async override Task Delete<T>(T item) 
        {
            if (typeof(T) == typeof(Aircraft))
                InternalData.DeleteAircraft(item as Aircraft);
            if (typeof(T) == typeof(Airfield))
                InternalData.DeleteAirfield(item as Airfield);
            if (typeof(T) == typeof(AcType))
                InternalData.DeleteAcType(item as AcType);
            if (typeof(T) == typeof(Flight))
                InternalData.DeleteFlight(item as Flight);
        }

        public async override Task Update<T>(T item)
        {
            if (typeof(T) == typeof(Aircraft))
                InternalData.UpdateAircraft(item as Aircraft);
            if (typeof(T) == typeof(Airfield))
                InternalData.UpdateAirfield(item as Airfield);
            if (typeof(T) == typeof(AcType))
                InternalData.UpdateAcType(item as AcType);
            if (typeof(T) == typeof(Flight))
                InternalData.UpdateFlight(item as Flight);
        }

     
    }
}