using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using LogbookApp.Data;

namespace LogbookApp.Mocks
{
    public class MockFlightDataService : IOnlineFlightData
    {
        
        private bool _exists;
        private FlightData _internalFlightData;
        private DateTime? _lastUpdated;

        public MockFlightDataService(DataType dataType, FlightData internalFlightData)
        {
            
            _exists = false;
            _internalFlightData = internalFlightData;

        }

        public MockFlightDataService(DataType dataType) : this(dataType, new FlightData())
        {
            
        }
        public DataType DataType { get
        {
            return DataType.OnLine;
        }}

        


        public async Task<Lookups> GetLookups()
        {
            return _internalFlightData.Lookups;
        }

        
        public Task InsertFlight(Flight flight)
        {
            return default(Task); 
        }

        public Task DeleteFlight(Flight flight)
        {
            return default(Task); 
        }

        public async Task SaveFlight(Flight flight)
        {
        }

        public void SaveFlights()
        {
           
        }

        public async Task InsertAircraft(Aircraft aircraft)
        {
           
        }

        public async Task InsertAircraftType(AcType acType)
        {
        }

        public async Task InsertAirfield(Airfield @from)
        {
          
        }

        public async Task UpdateAircraft(Aircraft aircraft)
        {
            
        }

        public async Task DeleteAircraft(Aircraft f)
        {
            
        }

        public async Task UpdateAirfield(Airfield airfield)
        {
           
        }

        public async Task DeleteAirfield(Airfield f)
        {
            
        }

        public async Task UpdateAcType(AcType acType)
        {
          
        }

        public async Task InsertAcType(AcType acType)
        {
           _internalFlightData.AddAcType(acType);
        }

        

        public async Task CreateUserData(FlightData flightData, DateTime now)
        {
            _internalFlightData.User = flightData.User;
            
        }

        //public async Task<User> GetUser()
        //{
        //    if (_exists)
        //    {
        //        _internalFlightData.User.DisplayName = displayName;
        //        return _internalFlightData.User;
        //    }
        //    return null;
        //}

        
        public bool FlightsChanged { get; set; }
        public User User { get { return _internalFlightData.User; }
             set { _internalFlightData.User = value; }}


        public async Task<ObservableCollection<Flight>> GetFlights()
        {
            return _internalFlightData.Flights;
        }

        public async Task<bool> UserDataExists(string displayName)
        {
            return _exists;
        }

        public async Task DeleteAcType(AcType acType)
        {
            
        }

        public async Task Update<T>(T item) where T : IEntity
        {
            var items= GetItems<T>();
            if (items != null)
            {
                items.Remove(items.First(x => x.id == item.id));
                items.Add(item);
            }
        }

       

        public async Task Insert<T>(T item) where T : IEntity
        {
            GetItems<T>().Add(item);
        }


        public async Task Delete<T>(T item) where T : IEntity
        {
             GetItems<T>().Remove(item);
        }

        private ObservableCollection<T> GetItems<T>()  where T : IEntity
        {
             Type type = typeof(T);
            if (type == typeof(Aircraft))
            {
                return _internalFlightData.Lookups.Aircraft as ObservableCollection<T>;
            }
            if (type == typeof(Airfield))
            {
               return  _internalFlightData.Lookups.Airfields as ObservableCollection<T>;
            }

            if (type == typeof(Flight))
            {
               return  _internalFlightData.Flights as ObservableCollection<T>;
            }
            return null;
        }


        public async Task UpdateUser(User user)
        {
            _internalFlightData.User=user;
        }

        public async Task SetUserData(string displayName)
        {
            _internalFlightData.User.DisplayName = displayName;
        }

        public DateTime? LastUpdated

        {
            get { return _internalFlightData.User.TimeStamp; }
           
        }


        public void SetExists(bool exists)
        {
            _exists = exists;
           
        }

        

       
    }
}