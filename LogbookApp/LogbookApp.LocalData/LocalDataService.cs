using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class LocalDataService : DataService<FlightData>, IOfflineDataService<FlightData,User>
    {
        private readonly ILocalStorage _localStorage;
        private readonly string _flightsFileName;
        private readonly string _lookupsFileName;
        private readonly string _userFileName;
        
        private Lookups _lookups;
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
            _lookups= await _localStorage.Restore<Lookups>(_lookupsFileName);
            return _lookups;
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

        public Task Update<T>(T item) where T : IEntity
        {
            throw new NotImplementedException();
        }


        public Task Insert<T>(T item) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public Task Delete<T>(T item) where T : IEntity
        {
            throw new NotImplementedException();
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



        public virtual async Task<ObservableCollection<Flight>> GetFlights(int userId)
        {
            var flights = await _localStorage.Restore<ObservableCollection<Flight>>(_flightsFileName);

            if (flights != null)
            {
                PopulateFlightLookups(flights, _lookups);
               
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

        public async Task SaveLocalData(FlightData flightData)
        {
            await SaveFlights(flightData.Flights);
            await SaveLookups(flightData.Lookups);
        }

    
    }
}
