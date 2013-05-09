using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Storage
{
    public class LocalDataService : IFlightDataService
    {
        private readonly ILocalStorage _localStorage;
        private readonly string _flightsFileName;
        private readonly string _lookupsFileName;
        private readonly string _userFileName;
        private  FlightData _flightData;


        public LocalDataService(ILocalStorage localStorage, string flightsFileName, string lookupsFileName,
            string userFileName)
        {
            _localStorage = localStorage;
            _flightsFileName = flightsFileName;
            _lookupsFileName = lookupsFileName;
            _userFileName = userFileName;
            
        }

        public DataType DataType { get; private set; }
        public async Task<Lookups> GetLookups(int userId)
        {
            return await _localStorage.Restore<Lookups>(_lookupsFileName);
        }

      
        
        public async Task InsertFlight(Flight flight )
        {
            await SaveFlights();
            
        }

        private async Task SaveFlights()
        {
            await _localStorage.Save(_flightData.Flights, _flightsFileName);
        }

        

        public async Task DeleteFlight(Flight flight)
        {
          
            await SaveFlights();
            

        }

        public async Task SaveFlight(Flight flight)
        {
            await SaveFlights();
            
        }

        public async Task InsertAircraft(Aircraft aircraft)
        {
            await SaveLookups();
        }

        

        private async Task SaveLookups()
        {
            await _localStorage.Save(_flightData.Lookups, _lookupsFileName);
        }


        public async Task InsertAircraftType(AcType acType)
        {
            await SaveLookups();
        }

        public async Task InsertAirfield(Airfield @from)
        {
            await SaveLookups();
        }

        public async Task UpdateAircraft(Aircraft aircraft)
        {
            await SaveLookups();
        }

        public async Task DeleteAircraft(Aircraft aircraft)
        {
            await SaveLookups();
        }

        public async Task UpdateAirfield(Airfield airfield)
        {
            await SaveLookups();
        }

        public async Task DeleteAirfield(Airfield airfield)
        {
            await SaveLookups();
        }

        public async Task UpdateAcType(AcType acType)
        {
            await SaveLookups();
        }

        public async Task InsertAcType(AcType acType)
        {
            await SaveLookups();
        }


        public async Task InsertUser(User user)
        {
            await _localStorage.Save(user, _userFileName);
        }

        public virtual async Task<User> GetUser(string displayName)
        {
            return await _localStorage.Restore<User>(_userFileName);
        }

        
        public bool FlightsChanged { get; set; }
        public DateTime? LastUpdated { get; set; }


        public async  Task<List<Flight>>  GetFlights(int userId)
        {
            return await _localStorage.Restore<List<Flight>>(_flightsFileName);
        }

        public async Task<bool> Available(string displayName)
        {
            User tryUser = null;
            try
            {
                tryUser =await GetUser(displayName);
            }
            catch (Exception)
            {

                return false;
            }


            return tryUser != null;
            
        }

        public async Task UpdateUser(User user)
        {
            LastUpdated = user.LastUpdated;
            await _localStorage.Save(user, _userFileName);
            
        }

        public async Task DeleteAcType(AcType acType)
        {
           await SaveLookups();
        }

        public void SetFlightData(FlightData flightData)
        {
            _flightData = flightData;
        }
    }
}
