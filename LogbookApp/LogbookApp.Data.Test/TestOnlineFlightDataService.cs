using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data.Test
{
    public class TestFlightDataService : IFlightDataService
    {
        private bool _available;

        public TestFlightDataService(DataType dataType)
        {
            DataType = dataType;
            _available = false;
            
        }

        public DataType DataType { get; private set; }
        public List<Flight> Flights { get; set; }
        public Task GetData()
        {
            
            return default(Task);
        }

        public Task GetLookups()
        {
            return default(Task);
        }

        public Lookups Lookups { get; set; }
        public Task<bool> InsertFlight(Flight flight)
        {
            return default(Task<bool>); 
        }

        public Task<bool> DeleteFlight(Flight flight)
        {
            return default(Task<bool>); 
        }

        public async Task<bool> SaveFlight(Flight flight)
        {
            return true;
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

        public async Task<bool> DeleteAircraft(Aircraft f)
        {
            return true;
        }

        public async Task UpdateAirfield(Airfield airfield)
        {
           
        }

        public async Task<bool> DeleteAirfield(Airfield f)
        {
            return true;
        }

        public async Task UpdateAcType(AcType acType)
        {
          
        }

        public async Task InsertAcType(AcType acType)
        {
          
        }

        public async Task<bool> Delete<T1>(T1 item)
        {
            return true;
        }

        public Task InsertUser(User user)
        {
            return default(Task);
        }

        public async Task GetUser(string displayName)
        {
            User= new User {DisplayName = displayName};
        }

        public User User { get; private set; }
        public bool FlightsChanged { get; set; }
        public Task GetFlights()
        {
            return default(Task);
        }

        public async Task<bool> Available(string displayName)
        {
            return _available;
        }

        public async Task UpdateUser(DateTime upDateTime)
        {
            User.LastUpdated = upDateTime;
        }

        public void SetAvailable(bool available)
        {
            _available = available;
        }

        public void SetUser(User user)
        {
            User = user;
        }
    }
}