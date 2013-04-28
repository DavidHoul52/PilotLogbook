using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class MobileFlightDataService : IFlightDataService
    {
        private MobileServiceClient _mobileService;
        private readonly string _displayName;
        private IUserManager _userManager;
        private bool _connected;
        private bool _available;

        public MobileFlightDataService(MobileServiceClient mobileService, string displayName)
        {

            _mobileService = mobileService;
            _displayName = displayName;
            _userManager = new UserManager();
            
        }


        public DataType DataType
        {
            get { return DataType.OnLine; }
        }

        public List<Flight> Flights { get; set; }

        public Action OnDisconnectedAction { get; set; }
        
          
      

        public async Task GetData()
        {
            _userManager.DisplayName = _displayName;
            await _userManager.GetUser(this);
            User = _userManager.User;
            await GetLookups();
            await GetFlights();
            
          
            

            
        }

        public async Task GetFlights()
        {
            FlightsChanged = false;
            var flights = await _mobileService.GetTable<Flight>().Where(x => x.UserId == User.Id).Take(500).ToListAsync();
            Flights = flights.Select(x =>
            {
                x.Capacity = Lookups.Capacity.Where(c => c.Id == x.CapacityId).FirstOrDefault();
                x.From = Lookups.Airfields.Where(airfield => airfield.Id == x.AirfieldFromId).FirstOrDefault();
                x.To = Lookups.Airfields.Where(airfield => airfield.Id == x.AirfieldToId).FirstOrDefault();
                x.Aircraft =
                    Lookups.Aircraft.Where(aircraft => aircraft.id == x.AircraftId)
                        .FirstOrDefault();
                x.Lookups = Lookups;
                x.DataService = this;
                return x;
            }).ToList();

         
        }

        public async Task<bool> Available()
        {
            await GetUser();

            return User != null;
        }

        public async Task UpdateUser(DateTime upDateTime)
        {
            User.LastUpdated = upDateTime;
            await Update(User);
        }

        public async Task GetLookups()
        {
            Lookups = new Lookups();
            await LoadLookups(User.Id);
            

        }

        private async Task LoadLookups(int userId)
        {
            Lookups.AcTypes = new ObservableCollection<AcType>(await _mobileService.GetTable<AcType>().Take(500).OrderBy(x => x.Code).ToListAsync());
            Lookups.Capacity = new ObservableCollection<Capacity>(await _mobileService.GetTable<Capacity>().Take(500).OrderBy(x => x.Description).ToListAsync());
            var aircraft = await
                _mobileService.GetTable<Aircraft>()
                    .Where(x => x.UserId == userId)
                    .Take(500)
                    .OrderBy(x => x.Reg)
                    .ToListAsync();

            Lookups.Aircraft = new ObservableCollection<Aircraft>(aircraft.
               Select(ac =>
               {
                   ac.AcType = Lookups.AcTypes.FirstOrDefault(x => x.Id == ac.AcTypeId);
                   return ac;
               }));
            Lookups.Airfields = new ObservableCollection<Airfield>(await _mobileService.GetTable<Airfield>().Where(x => x.UserId == userId).Take(500).OrderBy(x => x.Name).
               ToListAsync());
          
        }

        public Lookups Lookups { get; set; }

        public async Task<bool> InsertFlight(Flight flight)
        {
           await _mobileService.GetTable<Flight>().InsertAsync(flight);
         
           return true;
        }



        public async Task<bool> DeleteFlight(Flight flight)
        {
            await _mobileService.GetTable<Flight>().DeleteAsync(flight);
            
            FlightsChanged = true;
            return true;
        }

        public async Task<bool> SaveFlight(Flight flight)
        {

            if (!flight.Valid())
                return flight.IsNew; // simply cancel if new and not complete

            if (flight.IsNew)
            {
                flight.UserId = User.Id;
                await InsertFlight(flight);
            }

            else
            {
                await _mobileService.GetTable<Flight>().UpdateAsync(flight);
            }
            FlightsChanged = true;
            return true;
        }

      
      


        public async Task InsertAircraft(Aircraft aircraft)
        {
            aircraft.UserId = User.Id;
            await Insert<Aircraft>(aircraft);
            
            
        }

        public async Task InsertAircraftType(AcType acType)
        {
            
            await Insert(acType);
            
        }

        public async Task InsertAirfield(Airfield airfield)
        {
            airfield.UserId = User.Id;
            await Insert(airfield);
            
        }


        public async Task UpdateAircraft(Aircraft Aircraft)
        {
            await Update(Aircraft);
            
        }

        public async Task<bool> DeleteAircraft(Aircraft f)
        {
            await Delete(f);
            
            return true;
        }


        public async Task<bool> ClearAcTypes()
        {
            var actypes= await _mobileService.GetTable<AcType>().ReadAsync();
            foreach (var actype in actypes)
            {
                await _mobileService.GetTable<AcType>().DeleteAsync(actype);
            }
            return true;

        }

        

            public async Task<bool> ClearTable<T>()
        {
            var items= await _mobileService.GetTable<T>().ReadAsync();
            foreach (var item in items)
            {
                await _mobileService.GetTable<T>().DeleteAsync(item);
            }
            return true;

        }


        public async Task Insert<T>(T item)
        {
            await _mobileService.GetTable<T>().InsertAsync(item);
        }

        public async Task Update<T>(T item)
        {
            await _mobileService.GetTable<T>().UpdateAsync(item);
        }

        public async Task<bool> Delete<T>(T item)
        {
            await _mobileService.GetTable<T>().DeleteAsync(item);
            return true;
        }


        public async Task UpdateAirfield(Airfield Airfield)
        {
             await Update(Airfield);
        }


        public async Task<bool> DeleteAirfield(Airfield f)
        {
            bool result =await Delete(f);
            await OnDataUpdated();
            return result;

        }


        public async Task UpdateAcType(AcType AcType)
        {
            await Update(AcType);
            await OnDataUpdated();
        }


        public async Task InsertAcType(AcType AcType)
        {
            await Insert(AcType);
            await OnDataUpdated();
        }


        public async Task InsertUser(User user)
        {
            User = user;
            User.LastUpdated=DateTime.UtcNow;
            await Insert(user);
        }

        public User User { get; private set; }
        public bool FlightsChanged { get; set; }


        public async Task GetUser()
        {
            List<User> users;
         
                try
                {
                    users = await _mobileService.GetTable<User>()
                        .Where(x => x.DisplayName == _displayName)
                        .ToListAsync();     
                    
                }
                catch (Exception)
                {
                    
                    OnDisconnectedAction();
                    return;

                }
          

            User = users.FirstOrDefault();

        }
    }
}
