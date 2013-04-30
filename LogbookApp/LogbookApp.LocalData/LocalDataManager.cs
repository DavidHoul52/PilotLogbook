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
      

        public async Task GetLookups()
        {
            await RestoreLookups();
        }

        private async Task RestoreLookups()
        {
            Lookups=await _localStorage.Restore<Lookups>(_lookupsFileName);
        }

        public Lookups Lookups { get; set; }
        public async Task<bool> InsertFlight(Flight flight)
        {
            Flights.Add(flight);
            return await SaveFlights();
            
        }

        private async Task<bool> SaveFlights()
        {
            await _localStorage.Save(Flights, _flightsFileName);
            return true;
        }

        public async Task<bool> DeleteFlight(Flight flight)
        {
            Flights.Remove(flight);
            return await SaveFlights();
            

        }

        public async Task<bool> SaveFlight(Flight flight)
        {
            return await SaveFlights();
            
        }

        public async Task InsertAircraft(Aircraft aircraft)
        {
            Lookups.Aircraft.Add(aircraft);
            await SaveLookups();
        }

        private async Task<bool> SaveLookups()
        {
            await _localStorage.Save(Lookups, _lookupsFileName);
            return true;
        }

        public async Task InsertAircraftType(AcType acType)
        {
            Lookups.AcTypes.Add(acType);
            await SaveLookups();
        }

        public async Task InsertAirfield(Airfield @from)
        {
            Lookups.Airfields.Add(from);
            await SaveLookups();
        }

        public async Task UpdateAircraft(Aircraft aircraft)
        {
            await SaveLookups();
        }

        public async Task<bool> DeleteAircraft(Aircraft aircraft)
        {
            Lookups.Aircraft.Remove(aircraft);
            return await SaveLookups();
        }

        public async Task UpdateAirfield(Airfield airfield)
        {
            await SaveLookups();
        }

        public async Task<bool> DeleteAirfield(Airfield airfield)
        {
            Lookups.Airfields.Remove(airfield);
            return await SaveLookups();
        }

        public async Task UpdateAcType(AcType acType)
        {
            await SaveLookups();
        }

        public async Task InsertAcType(AcType acType)
        {
            Lookups.AcTypes.Add(acType);
            await SaveLookups();
        }

        public Task<bool> Delete<T1>(T1 item)
        {
            throw new NotImplementedException();
        }

        public async Task InsertUser(User user)
        {
            User = user;
            await SaveUser();
        }

        public async Task GetUser(string displayName)
        {
            User = await _localStorage.Restore<User>(_userFileName);
        }

        public User User { get; private set; }
        public bool FlightsChanged { get; set; }
        public async  Task GetFlights()
        {
            await _localStorage.Restore<List<Flight>>(_flightsFileName);
        }

        public async Task<bool> Available(string displayName)
        {
            return true;
        }

        public async Task UpdateUser(DateTime upDateTime)
        {
            await SaveUser();
        }

        private async Task SaveUser()
        {
            await _localStorage.Save(User, _userFileName);
        }
    }
}
