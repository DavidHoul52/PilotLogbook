using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Storage
{
    public class LocalDataManager : IFlightDataService
    {
        private readonly ILocalStorage _localStorage;
        private readonly string _flightsFileName;
        private readonly string _lookupsFileName;
        private readonly string _userFileName;
        

        public LocalDataManager(ILocalStorage localStorage, string flightsFileName, string lookupsFileName,
            string userFileName)
        {
            _localStorage = localStorage;
            _flightsFileName = flightsFileName;
            _lookupsFileName = lookupsFileName;
            _userFileName = userFileName;
            
            
           
        }

        public DataType DataType { get; private set; }
        public List<Flight> Flights { get; set; }


        public async Task<Lookups> GetLookups()
        {
            await RestoreLookups();
            return Lookups;
        }

        private async Task RestoreLookups()
        {
            Lookups=await _localStorage.Restore<Lookups>(_lookupsFileName);
        }

        public Lookups Lookups { get; set; }
        public async Task<bool> InsertFlight(Flight flight)
        {
            if (Flights==null)
                Flights = new List<Flight>();
            Flights.Add(flight);
            return await SaveAll();
            
        }

        private async Task<bool> SaveAll()
        {
            await _localStorage.Save(Flights, _flightsFileName);
            await _localStorage.Save(Lookups, _lookupsFileName);
            await _localStorage.Save(User, _userFileName);
            return true;
        }

        public async Task<bool> DeleteFlight(Flight flight)
        {
            Flights.Remove(flight);
            return await SaveAll();
            

        }

        public async Task<bool> SaveFlight(Flight flight)
        {
            return await SaveAll();
            
        }

        public async Task InsertAircraft(Aircraft aircraft)
        {
            await LookupOperation(() => Lookups.Aircraft.Add(aircraft));
        }

        private async Task LookupOperation(Action action)
        {
            if (Lookups == null)
                await GetLookups();
            action();
            await SaveAll();
        }

        

        public async Task InsertAircraftType(AcType acType)
        {
            await LookupOperation(() => Lookups.AcTypes.Add(acType));
        }

        public async Task InsertAirfield(Airfield @from)
        {
            Lookups.Airfields.Add(from);
            await SaveAll();
        }

        public async Task UpdateAircraft(Aircraft aircraft)
        {
            await SaveAll();
        }

        public async Task<bool> DeleteAircraft(Aircraft aircraft)
        {
            Lookups.Aircraft.Remove(aircraft);
            return await SaveAll();
        }

        public async Task UpdateAirfield(Airfield airfield)
        {
            await SaveAll();
        }

        public async Task<bool> DeleteAirfield(Airfield airfield)
        {
            Lookups.Airfields.Remove(airfield);
            return await SaveAll();
        }

        public async Task UpdateAcType(AcType acType)
        {
            await SaveAll();
        }

        public async Task InsertAcType(AcType acType)
        {
            Lookups.AcTypes.Add(acType);
            await SaveAll();
        }

        public Task<bool> Delete<T1>(T1 item)
        {
            throw new NotImplementedException();
        }

        public async Task InsertUser(User user)
        {
            User = user;
            await SaveAll();
        }

        public virtual async Task GetUser(string displayName)
        {
            User = await _localStorage.Restore<User>(_userFileName);
        }

        public User User { get; protected set; }
        public bool FlightsChanged { get; set; }
        

        public async  Task<List<Flight>>  GetFlights()
        {
            return await _localStorage.Restore<List<Flight>>(_flightsFileName);
        }

        public async Task<bool> Available(string displayName)
        {
            await GetUser(displayName);

            return User != null;
            //return _localStorage.Exists;
        }

        public async Task UpdateUser(DateTime upDateTime)
        {
            User.LastUpdated = upDateTime;
            await SaveAll();
            
        }

      

        
    }
}
