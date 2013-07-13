using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;
using Windows.System.UserProfile;
using Windows.UI.Xaml.Controls;
using BaseData;
using LogbookApp.Data;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.Storage
{
    public class LocalDataService : DataService<FlightData,User>, IOfflineDataService<FlightData,User>,
        IFlightDataService
    {
        private readonly ILocalStorage _localStorage;
        private readonly string _flightsFileName;
        private readonly string _lookupsFileName;
        private readonly string _userFileName;
        
        
        protected User _user;


        public LocalDataService(ILocalStorage localStorage, string flightsFileName, string lookupsFileName,
            string userFileName)
            : base()
        {
            _localStorage = localStorage;
            _flightsFileName = flightsFileName;
            _lookupsFileName = lookupsFileName;
            _userFileName = userFileName;
            
            
        }

    


        public virtual async Task<Lookups> GetLookups(int userId)
        {
            return await _localStorage.Restore<Lookups>(_lookupsFileName);
            
        }


        private async Task SaveFlights(ObservableCollection<Flight> flights )
        {
            await _localStorage.Save(flights, _flightsFileName);
        }

       

        private async Task SaveLookups(Lookups lookups)
        {
            await _localStorage.Save(lookups, _lookupsFileName);
            
        }


     
        

        Task<User> IDataService<FlightData, User>.GetUser(string userName)
        {
            return GetUser(userName);
        }

        public async Task Update<T>(T item) where T : IEntity
        {
            if (typeof(T) == typeof(Flight))
            {
                var flights = await GetFlights(User.id);
                FlightData.Update(item as Flight,flights);
                await SaveFlights(flights);
            }
            else
            {
                await UpdateLookup(item);
            }


        }

        private async Task UpdateLookup<T>(T item) where T : IEntity
        {

            var lookups = await GetLookups(User.id);

            if (typeof(T) == typeof(Aircraft))
            {
                FlightData.Update(item as Aircraft,lookups.Aircraft);
            }
            if (typeof(T) == typeof(Airfield))
                FlightData.Update(item as Airfield, lookups.Airfields);
            if (typeof(T) == typeof(AcType))
                FlightData.Update(item as AcType, lookups.AcTypes);

            await SaveLookups(lookups);

        }


        public async Task Insert<T>(T item) where T : IEntity
        {
            if (typeof(T) == typeof(Flight))
            {
                var flights = await GetFlights(User.id);
                flights.Add(item as Flight);
                await SaveFlights(flights);
            }
            else
            {
                await InsertLookup(item);
            }


        
        }

        private async Task InsertLookup<T>(T item) where T : IEntity
        {
            
            var lookups = await GetLookups(User.id);

            if (typeof(T) == typeof(Aircraft))
            {
                lookups.Aircraft.Add(item as Aircraft);

            }
            if (typeof(T) == typeof(Airfield))
                lookups.Airfields.Add(item as Airfield);
            if (typeof(T) == typeof(AcType))
                lookups.AcTypes.Add(item as AcType);

            await SaveLookups(lookups);

        }

        public async Task Delete<T>(T item) where T : IEntity
        {
            if (typeof(T) == typeof(Flight))
            {
                var flights = await GetFlights(User.id);
                flights.Remove(item as Flight);
                await SaveFlights(flights);
            }
            else
            {
                await DeleteLookup(item);
            }
        }


        private async Task DeleteLookup<T>(T item) where T : IEntity
        {

            var lookups = await GetLookups(User.id);

            if (typeof(T) == typeof(Aircraft))
            {
                lookups.Aircraft.Remove(item as Aircraft);

            }
            if (typeof(T) == typeof(Airfield))
                lookups.Airfields.Remove(item as Airfield);
            if (typeof(T) == typeof(AcType))
                lookups.AcTypes.Remove(item as AcType);

            await SaveLookups(lookups);

        }

        public async Task<bool> GetUserDataExists(string userName)
        {

            User tryUser = null;
            try
            {
                tryUser = await GetUser(userName);
            }
            catch (Exception)
            {

                return false;
            }


            return tryUser != null;
        }

       


        protected async Task<User> GetUser(string displayName)
        {

            try
            {
                User userx = await _localStorage.RestoreUser(_userFileName);
                if (userx != null && userx.DisplayName == displayName)
                    return userx;
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool FlightsChanged { get; set; }


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
            await Insert(flight);
        }

        public async Task InsertAircraft(Aircraft aircraft)
        {
            await Insert(aircraft);
        }

        public async Task InsertAircraftType(AcType acType)
        {
            await Insert(acType);
        }

        public async Task InsertAirfield(Airfield @from)
        {
            await Insert(@from);
        }


        public async Task UpdateAircraft(Aircraft aircraft)
        {
            await Update(aircraft);
        }

        public async Task DeleteAircraft(Aircraft f)
        {
            await Delete(f);
        }

        public async Task UpdateAirfield(Airfield airfield)
        {
            await Update(airfield);
        }

        public async Task DeleteAirfield(Airfield f)
        {
            await DeleteAirfield(f);
        }

        public async Task UpdateAcType(AcType acType)
        {
            await Update(acType);
        }

        public async Task InsertAcType(AcType acType)
        {
            await Insert(acType);
        }

        public async Task DeleteAcType(AcType acType)
        {
            await Delete(acType);
        }

        public async Task<Lookups> LoadLookups(int userId)
        {
            return await GetLookups(userId);
        }

        public virtual async Task<ObservableCollection<Flight>> GetFlights(int userId)
        {
            var flights = await _localStorage.Restore<ObservableCollection<Flight>>(_flightsFileName);
            var lookups = await GetLookups(userId);

            if (flights != null)
            {
                PopulateFlightLookups(flights, lookups);
               
            }
            return flights;
        }

      

        protected void PopulateFlightLookups(ObservableCollection<Flight> flights, Lookups lookups)
        {
            foreach (var flight in flights)
            {

                flight.PopulateLookups(lookups, new InMemoryLookups());
            }
        }
      

        protected async Task UpdateUserInternal(User user)
        {
            await _localStorage.Save(user, _userFileName);
        }


      


        protected async override Task<FlightData> InternalLoadUserData(string userName)
        {
            var flightData = new FlightData();
            flightData.User = await GetUser(userName);
            flightData.Lookups = await GetLookups(flightData.User.id);
            flightData.Flights = await GetFlights(flightData.User.id);
            return flightData;
        }

        protected async override Task InternalCreateUserData(string userName)
        {
            
            var flightData = new FlightData();
            flightData.User.DisplayName = userName;
            await _localStorage.Save(flightData.User, _userFileName);
            await SaveLookups(flightData.Lookups);
            await SaveFlights(flightData.Flights);
        }

        protected async override Task InternalUpdateUserTimeStamp(DateTime? timeStamp)
        {
            
        }

        public async Task SaveLocalData(FlightData flightData)
        {
            await SaveFlights(flightData.Flights);
            await SaveLookups(flightData.Lookups);
        }

    
    }
}
