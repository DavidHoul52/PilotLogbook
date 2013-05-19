using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Mocks
{
    public class TestLocalStorage : LocalStorageBase, ILocalStorage
    {
        private User _user;
        private DateTime? _timeStamp;
        private FlightData _internalFlightData;
        

        public TestLocalStorage()
            : base()
        {
            _internalFlightData = new FlightData();

        }

        public FlightData InternalFlightData
        {
            get { return _internalFlightData; }
            set { _internalFlightData = value; }
        }


        public override async Task Save<T>(T data, string filename)
        {
            SavedFileName = filename;
            base.Save(data, filename);
            AllSaved = true;
            if (typeof (T) == typeof (ObservableCollection<Flight>))
                _internalFlightData.Flights = data as ObservableCollection<Flight>;
            if (typeof(T) == typeof(Lookups))
                _internalFlightData.Lookups = data as Lookups;
            if (typeof(T) == typeof(User))
                _internalFlightData.User = data as User;


        }

        public async Task<T> Restore<T>(string filename)
            where T : new()
        {

            AllSaved = false;
            if (Exists)
            {
                
                return new T();
            }
            else
            {
                return default(T);
            }
        }


      

        public async Task<User> RestoreUser(string filename)
        {
            return _internalFlightData.User;
        }

        public bool Exists { get; set; }


        public string SavedFileName { get; set; }
        public bool AllSaved { get; set; }

        public void SetExists(bool exists)
        {
            Exists = exists;
        }

        public void SetTimeStamp(DateTime? timeStamp)
        {
            _timeStamp = timeStamp;

        }
    }
}