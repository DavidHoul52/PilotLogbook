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
    public class MockLocalDataManager : LocalDataService
    {
        
        private TestLocalStorage _testLocalStorage;

        public MockLocalDataManager(TestLocalStorage localStorage, string flightsFileName, 
            string lookupsFileName, string userFileName) : 
            base(localStorage, flightsFileName, lookupsFileName, userFileName)
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


        public DateTime? LastUpdated
        {
            get { return FlightData.User.TimeStamp; }

            set
            {
                _testLocalStorage.SetTimeStamp(value);
                FlightData.User.TimeStamp = value;
            }

        }

        public override async Task<ObservableCollection<Flight>> GetFlights()
        {
            var lookups = _testLocalStorage.InternalFlightData.Lookups;
            var flights = _testLocalStorage.InternalFlightData.Flights;
            PopulateFlightLookups(flights,lookups);
            
            return flights;

        }

        public override async Task<Lookups> GetLookups()
        {
            return _testLocalStorage.InternalFlightData.Lookups;
            
        }
      
    }

}
