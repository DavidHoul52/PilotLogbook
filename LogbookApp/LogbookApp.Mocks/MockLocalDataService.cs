using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.Storage;

namespace LogbookApp.Mocks
{
    public class MockLocalDataService : LocalDataService
    {
        
        private TestLocalStorage _testLocalStorage;

        public MockLocalDataService(TestLocalStorage localStorage, string flightsFileName, 
            string lookupsFileName, string userFileName, string displayName) : 
            base(localStorage, flightsFileName, lookupsFileName, userFileName, displayName)
        {
            _testLocalStorage = localStorage;
            
        }

        public FlightData FlightData
        {
            get { return _testLocalStorage.InternalFlightData; }
            set
            {
              
                _testLocalStorage.InternalFlightData = value;
            }
        }



        protected async override Task<User> GetUserInternal(string displayName)
        {
            _testLocalStorage.InternalFlightData.User.DisplayName = displayName;
            return _testLocalStorage.InternalFlightData.User;
            
        }


        public override async Task<ObservableCollection<Flight>> GetFlights(int userId)
        {
            var lookups = _testLocalStorage.InternalFlightData.Lookups;
            var flights = _testLocalStorage.InternalFlightData.Flights;
            PopulateFlightLookups(flights,lookups);
            
            return flights;

        }

        public override async Task<Lookups> GetLookups(int userId)
        {
            return _testLocalStorage.InternalFlightData.Lookups;
            
        }

        public void SetLastUpdated(DateTime? local)
        {
            _testLocalStorage.InternalFlightData.User.TimeStamp = local;
            LastUpdated = local; // belts and braces


        }

        protected async override Task UpdateUserInternal(User user)
        {
            _testLocalStorage.InternalFlightData.User = user;
        }

      
    }

}
