using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Storage
{
    public class LocalDataService 
    {
        private readonly ILocalStorage _localStorage;
        private readonly string _flightsFileName;
        private readonly string _lookupsFileName;
        private readonly string _userFileName;
        


        public LocalDataService(ILocalStorage localStorage, string flightsFileName, string lookupsFileName,
            string userFileName)
        {
            _localStorage = localStorage;
            _flightsFileName = flightsFileName;
            _lookupsFileName = lookupsFileName;
            _userFileName = userFileName;
            
        }

        public DataType DataType { get; private set; }
        public async Task<Lookups> GetLookups()
        {
            return await _localStorage.Restore<Lookups>(_lookupsFileName);
        }


        
        public async Task<bool> InsertFlight(Flight flight, List<Flight> flights )
        {
            return await SaveFlights(flights);
            
        }

        private async Task<bool> SaveFlights(List<Flight> flights)
        {
            await _localStorage.Save(flights, _flightsFileName);
        }

        //private async Task<bool> SaveAll()
        //{
            
        //    await _localStorage.Save(_flightData.Lookups, _lookupsFileName);
        //    await _localStorage.Save(_flightData.User, _userFileName);
        //    return true;
        //}

        public async Task<bool> DeleteFlight(Flight flight, List<Flight> flights)
        {
          
            return await SaveFlights(flights);
            

        }

        public async Task<bool> SaveFlight(Flight flight, List<Flight> flights)
        {
            return await SaveFlights(flights);
            
        }

        public async Task InsertAircraft(Aircraft aircraft, Lookups lookups)
        {
            await SaveLookups(lookups);
        }

        private async Task LookupOperation(Action action, Lookups lookups)
        {
            action();
            await SaveLookups(lookups);
        }

        private async Task SaveLookups(Lookups lookups)
        {
            await _localStorage.Save(lookups, _lookupsFileName);
        }


        public async Task InsertAircraftType(AcType acType, Lookups lookups)
        {
            await SaveLookups(lookups);
        }

        public async Task InsertAirfield(Airfield @from, Lookups lookups)
        {
            await SaveLookups(lookups);
        }

        public async Task UpdateAircraft(Aircraft aircraft, Lookups lookups)
        {
            await SaveLookups(lookups);
        }

        public async Task DeleteAircraft(Aircraft aircraft, Lookups lookups)
        {
            await SaveLookups(lookups);
        }

        public async Task UpdateAirfield(Airfield airfield, Lookups lookups)
        {
            await SaveLookups(lookups);
        }

        public async Task<bool> DeleteAirfield(Airfield airfield, Lookups lookups)
        {
            await SaveLookups(lookups);
        }

        public async Task UpdateAcType(AcType acType, Lookups lookups)
        {
            await SaveLookups(lookups);
        }

        public async Task InsertAcType(AcType acType, Lookups lookups)
        {
            await SaveLookups(lookups);
        }


        public async Task InsertUser(User user, Lookups lookups)
        {
            await SaveLookups(lookups);
        }

        public virtual async Task<User> GetUser(string displayName)
        {
            return await _localStorage.Restore<User>(_userFileName);
        }

        
        public bool FlightsChanged { get; set; }
        public DateTime? LastUpdated { get; set; }


        public async  Task<List<Flight>>  GetFlights()
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
            //return _localStorage.Exists;
        }

        public async Task UpdateUser(User user, DateTime upDateTime)
        {
            LastUpdated = upDateTime;
            await _localStorage.Save(user, _userFileName);
            
        }

      
    }
}
