using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class FlightDataService : IFlightDataService
    {
        private MobileServiceClient _mobileService;
        private IUserManager _userManager;

        public FlightDataService(MobileServiceClient mobileService)
        {

            _mobileService = mobileService;
            _userManager = new UserManager();
            
        }

     
        public List<Flight> Flights { get; set; }

        
        

        public async Task<bool> GetFlights(string displayName)
        {
            _userManager.DisplayName = displayName;
            await _userManager.GetUser(this);
            User = _userManager.User;
            await GetLookups();
            
            var flights = await _mobileService.GetTable<Flight>().Where(x=>x.UserId==User.Id).Take(500).ToListAsync() ;
            Flights = flights.Select(x => {
              
                
                x.Capacity = Lookups.Capacity.Where(c=>c.Id==x.CapacityId).FirstOrDefault();
                x.From = Lookups.Airfields.Where(airfield=>airfield.Id==x.AirfieldFromId).FirstOrDefault();
                x.To = Lookups.Airfields.Where(airfield=>airfield.Id==x.AirfieldToId).FirstOrDefault();
                x.Aircraft =
                                                  Lookups.Aircraft.Where(aircraft => aircraft.id == x.AircraftId)
                                                         .FirstOrDefault();
                x.Lookups = Lookups;
                x.DataService = this;
                return x;
            }).ToList();

            return true;

            
        }

        public async Task GetLookups()
        {
            Lookups = new Lookups(_mobileService);
            await Lookups.Load(User.Id);
            

        }

        public ILookups Lookups { get; set; }

        public async Task<bool> InsertFlight(Flight flight)
        {
           await _mobileService.GetTable<Flight>().InsertAsync(flight);
           return true;
        }

        public async Task<bool> UpdateFlight(Flight flight)
        {
            await _mobileService.GetTable<Flight>().UpdateAsync(flight);
            return true;
        }


        public async Task<bool> DeleteFlight(Flight flight)
        {
            await _mobileService.GetTable<Flight>().DeleteAsync(flight);
            return true;
        }

        public async Task<bool> SaveFlight(Flight flight)
        {

            if (!flight.Valid)
                return flight.IsNew; // simply cancel if new and not complete

            if (flight.IsNew)
            {
                flight.UserId = User.Id;
                await InsertFlight(flight);
            }

            else
                await _mobileService.GetTable<Flight>().UpdateAsync(flight);
            return true;
        }

        public void SaveFlights()
        {
            foreach (var flight in Flights)
            {
                SaveFlight(flight);
            }
            
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
            return await Delete(f);
            
        }


        public async Task UpdateAcType(AcType AcType)
        {
            await Update(AcType);
        }


        public async Task InsertAcType(AcType AcType)
        {
            await Insert(AcType);
        }


        public async Task InsertUser(User user)
        {
            User = user;
            await Insert(user);
        }

        public User User { get; private set; }
    

        public async Task GetUser(string displayName)
        {
           var users= await _mobileService.GetTable<User>()
                              .Where(x => x.DisplayName == displayName)
                              .ToListAsync();
            User = users.FirstOrDefault();

        }
    }
}
