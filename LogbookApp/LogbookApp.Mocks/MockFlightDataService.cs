using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using BaseData;
using LogbookApp.Data;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.Mocks
{
    public class MockFlightDataService : DataService<FlightData>, IOnlineFlightData
    {

        private bool _exists;
        private FlightData _internalFlightData;
        private DateTime? _lastUpdated;

        public MockFlightDataService(FlightData internalFlightData)
            : base()
        {

            _exists = false;
            _internalFlightData = internalFlightData;

        }

        public MockFlightDataService()
            : this(new FlightData())
        {

        }





        public async Task<Lookups> GetLookups(int userId)
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
            _internalFlightData.User = new User(flightData.User);
            

        }

       


        public bool FlightsChanged { get; set; }





        public async Task<ObservableCollection<Flight>> GetFlights(int userId)
        {
            return _internalFlightData.Flights;
        }

        public Task<Lookups> LoadLookups(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserDataExists(string displayName)
        {
            return _exists;
        }

        public async Task DeleteAcType(AcType acType)
        {

        }


        public Task<User> GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        Task IDataService<FlightData, User>.Update<T>(T item)
        {
            return Update(item);
        }

        public override async Task Update<T>(T item) 
        {
            var items = GetItems<T>();
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

        public Task<bool> GetUserDataExists(string userName)
        {
            throw new NotImplementedException();
        }

        private ObservableCollection<T> GetItems<T>() where T : IEntity
        {
            Type type = typeof(T);
            if (type == typeof(Aircraft))
            {
                return _internalFlightData.Lookups.Aircraft as ObservableCollection<T>;
            }
            if (type == typeof(Airfield))
            {
                return _internalFlightData.Lookups.Airfields as ObservableCollection<T>;
            }

            if (type == typeof(Flight))
            {
                return _internalFlightData.Flights as ObservableCollection<T>;
            }
            return null;
        }



       

        protected async override Task<User> GetUserInternal(string displayName)
        {
            return _internalFlightData.User;
        }


    


        public void SetExists(bool exists)
        {
            _exists = exists;

        }


        public void SetLastUpdated(DateTime? updated)
        {
            _internalFlightData.User.TimeStamp = updated;
            LastUpdated = updated;
        }

        protected async override Task UpdateUserInternal(User user)
        {
            _internalFlightData.User = user;
        }

        protected override Task<FlightData> InternalLoadUserData(string userName)
        {
            throw new NotImplementedException();
        }

        protected override Task InternalCreateUserData(string userName)
        {
            throw new NotImplementedException();
        }

        public bool IsConnected { get; set; }
    }
}