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
    public class LocalDataService : BaseOnlineFlightDataService, IFlightDataService
    {
        private readonly ILocalStorage _localStorage;
        private readonly string _flightsFileName;
        private readonly string _lookupsFileName;
        private readonly string _userFileName;
        
        private Lookups _lookups;
        protected User _user;


        public LocalDataService(ILocalStorage localStorage, string flightsFileName, string lookupsFileName,
            string userFileName, string displayName)
            : base(displayName)
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


     
        public async Task CreateUserData(FlightData flightData, DateTime now)
        {
        
            await _localStorage.Save(flightData.User, _userFileName);
            await SaveLookups(flightData.Lookups);
            await SaveFlights(flightData.Flights);
        }

        public override Task Update<T>(T item)
        {
            throw new NotImplementedException();
        }

      

        protected async override Task<User> GetUserInternal(string displayName)
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

     
       

      

        protected override async Task UpdateUserInternal(User user)
        {
            await _localStorage.Save(user, _userFileName);
        }


        public async Task SaveFlightData(FlightData flightData)
        {
            await SaveFlights(flightData.Flights);
            await SaveLookups(flightData.Lookups);
           
        }

       
    }
}
