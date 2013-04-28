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
            User = new User();
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

        public async Task UpdateAircraft(Aircraft Aircraft)
        {
            
        }

        public async Task<bool> DeleteAircraft(Aircraft f)
        {
            return true;
        }

        public async Task UpdateAirfield(Airfield Airfield)
        {
           
        }

        public async Task<bool> DeleteAirfield(Airfield f)
        {
            return true;
        }

        public async Task UpdateAcType(AcType AcType)
        {
          
        }

        public async Task InsertAcType(AcType AcType)
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

        public Task GetUser()
        {
            return default(Task);
        }

        public User User { get; private set; }
        public bool FlightsChanged { get; set; }
        public Task GetFlights()
        {
            return default(Task);
        }

        public async Task<bool> Available()
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
    }
}