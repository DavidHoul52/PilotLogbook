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
            await _localStorage.Save(_flightData.Flights, _flightsFileName);
        }

        private async Task<bool> SaveAll()
        {
            
            await _localStorage.Save(_flightData.Lookups, _lookupsFileName);
            await _localStorage.Save(_flightData.User, _userFileName);
            return true;
        }

        public async Task<bool> DeleteFlight(Flight flight)
        {
          //  Flights.Remove(flight);
            return await SaveAll();
            

        }

        public async Task<bool> SaveFlight(Flight flight)
        {
            return await SaveAll();
            
        }

        public async Task InsertAircraft(Aircraft aircraft)
        {
            await LookupOperation(() => _flightData.Lookups.Aircraft.Add(aircraft));
        }

        private async Task LookupOperation(Action action)
        {
           
            action();
            await SaveAll();
        }

        

        public async Task InsertAircraftType(AcType acType)
        {
            await LookupOperation(() => _flightData.Lookups.AcTypes.Add(acType));
        }

        public async Task InsertAirfield(Airfield @from)
        {
            //_flightData.Lookups.Airfields.Add(from);
            await SaveAll();
        }

        public async Task UpdateAircraft(Aircraft aircraft)
        {
            await SaveAll();
        }

        public async Task<bool> DeleteAircraft(Aircraft aircraft)
        {
            //Lookups.Aircraft.Remove(aircraft);
            return await SaveAll();
        }

        public async Task UpdateAirfield(Airfield airfield)
        {
            await SaveAll();
        }

        public async Task<bool> DeleteAirfield(Airfield airfield)
        {
            //Lookups.Airfields.Remove(airfield);
            return await SaveAll();
        }

        public async Task UpdateAcType(AcType acType)
        {
            await SaveAll();
        }

        public async Task InsertAcType(AcType acType)
        {
            //Lookups.AcTypes.Add(acType);
            await SaveAll();
        }

        public Task<bool> Delete<T1>(T1 item)
        {
            throw new NotImplementedException();
        }

        public async Task InsertUser(User user)
        {
            //User = user;
            await SaveAll();
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

        public async Task UpdateUser(DateTime upDateTime)
        {
           // User.LastUpdated = upDateTime;
            await SaveAll();
            
        }

      
    }
}
