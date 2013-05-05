using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Mocks
{
    public class MockFlightDataService : IFlightDataService
    {
        private DateTime? _lastUpdate;
        private bool _available;

        public MockFlightDataService(DataType dataType)
        {
            DataType = dataType;
            _lastUpdate = null;
            _available = false;
            Flights = new List<Flight>();
        }
        public DataType DataType { get; private set; }
        public List<Flight> Flights { get; set; }
       

        public async Task<Lookups> GetLookups()
        {
            return new Lookups();
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
            User = new User {DisplayName = displayName, LastUpdated = _lastUpdate};
        }

        public User User { get; private set; }
        public bool FlightsChanged { get; set; }
        

        public async Task<List<Flight>>  GetFlights()
        {
            return new List<Flight>();
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

        public void SetLastUpdated(DateTime lastupdated)
        {
            _lastUpdate = lastupdated;
        }
    }
}