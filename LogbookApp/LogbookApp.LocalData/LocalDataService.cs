using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;
using Windows.System.UserProfile;
using Windows.UI.Xaml.Controls;
using LogbookApp.Data;

namespace LogbookApp.Storage
{
    public class LocalDataService : IFlightDataService
    {
        private readonly ILocalStorage _localStorage;
        private readonly string _flightsFileName;
        private readonly string _lookupsFileName;
        private readonly string _userFileName;
        
        private Lookups _lookups;


        public LocalDataService(ILocalStorage localStorage, string flightsFileName, string lookupsFileName,
            string userFileName)
        {
            _localStorage = localStorage;
            _flightsFileName = flightsFileName;
            _lookupsFileName = lookupsFileName;
            _userFileName = userFileName;
            
            
        }

        public DataType DataType { get
        {
            return DataType.OffLine;
        }}

        

        public virtual async Task<Lookups> GetLookups()
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


     
        public async Task CreateUserData(FlightData flightData, DateTime now)
        {
            User = flightData.User;
            await _localStorage.Save(flightData.User, _userFileName);
            await SaveLookups(flightData.Lookups);
            await SaveFlights(flightData.Flights);
        }

        public virtual async Task<User> GetUser(string displayName)
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
        


        public virtual async  Task<ObservableCollection<Flight>> GetFlights()
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

                flight.PopulateLookups(lookups);
            }
        }
      

        public async Task<bool> UserDataExists(string displayName)
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

        public DateTime? LastUpdated { get { return User.TimeStamp; }  }

        public async Task UpdateUser(User user)
        {
            
            await _localStorage.Save(user, _userFileName);
            
        }

        public async Task SetUserData(string displayName)
        {
            User = GetUser(displayName).Result; 
        }

        public User User { get; private set; }


        public async Task SaveFlightData(FlightData flightData)
        {
            await SaveFlights(flightData.Flights);
            await SaveLookups(flightData.Lookups);
           
        }

       
    }
}
