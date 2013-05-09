using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Mocks
{
    public class MockFlightDataService : IFlightDataService
    {
        
        private bool _available;

        public MockFlightDataService(DataType dataType)
        {
            DataType = dataType;
            _available = false;
            
        }
        public DataType DataType { get; private set; }



        public async Task<Lookups> GetLookups(int userId)
        {
            return new Lookups();
        }

        
        public Task InsertFlight(Flight flight)
        {
            return default(Task); 
        }

        public Task DeleteFlight(Flight flight)
        {
            return default(Task); 
        }

        public async Task SaveFlight(Flight flight)
        {
        }

        public void SaveFlights()
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

        public async Task Delete<T1>(T1 item)
        {
            
        }

        public Task InsertUser(User user)
        {
            return default(Task);
        }

        public async Task<User> GetUser(string displayName)
        {
               return new User {DisplayName = displayName};
        }

        
        public bool FlightsChanged { get; set; }


        public async Task<List<Flight>> GetFlights(int userId)
        {
            return new List<Flight>();
        }

        public async Task<bool> Available(string displayName)
        {
            return _available;
        }

        public async Task DeleteAcType(AcType acType)
        {
            
        }

        public async Task UpdateUser(User user)
        {
            LastUpdated = user.LastUpdated;
        }

        public DateTime? LastUpdated { get; set; }


        public void SetAvailable(bool available)
        {
            _available = available;
            if (LastUpdated == null)
                LastUpdated = DateTime.MinValue;
        }

        

       
    }
}