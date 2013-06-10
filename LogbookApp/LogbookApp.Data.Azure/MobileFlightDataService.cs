using System.Collections.ObjectModel;
using System.Linq.Expressions;
using LogbookApp.Data;
using LogbookApp.Data.Validation;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class MobileFlightDataService : BaseOnlineFlightDataService, IOnlineFlightData
    {
        private MobileServiceClient _mobileService;
     
        private bool _connected;
        private bool _available;

        public MobileFlightDataService(MobileServiceClient mobileService, string displayName): base(displayName)
        {

            _mobileService = mobileService;
          
            
        }


        public DataType DataType
        {
            get { return DataType.OnLine; }
        }


       

        public async Task<ObservableCollection<Flight>> GetFlights(int userId)
        {
            FlightsChanged = false;
            var flights = await _mobileService.GetTable<Flight>().Where(x => x.UserId == userId).Take(500).ToListAsync();
            return new ObservableCollection<Flight>(flights);



        }

        public async Task<bool> UserDataExists(string displayName)
        {
           var user = await GetUser(displayName);

            return user != null;
        }

        
       

       

      

    

        public async Task DeleteAcType(AcType acType)
        {
            await Delete(acType);
        }

      


        public async Task<Lookups> GetLookups(int userId)
        {

            return await LoadLookups(userId);
            


        }

        private async Task<Lookups> LoadLookups(int userId)
        {
            var lookups = new Lookups();
            lookups.AcTypes = new ObservableCollection<AcType>(await _mobileService.GetTable<AcType>()
                .Where(x => x.UserId == userId)
                .Take(500).OrderBy(x => x.Code).ToListAsync());
            
            var aircraft = await
                _mobileService.GetTable<Aircraft>()
                    .Where(x => x.UserId == userId)
                    .Take(500)
                    .OrderBy(x => x.Reg)
                    .ToListAsync();

            lookups.Aircraft = new ObservableCollection<Aircraft>(aircraft.
               Select(ac =>
               {
                   ac.AcType = lookups.AcTypes.FirstOrDefault(x => x.id == ac.AcTypeId);
                   return ac;
               }));
            lookups.Airfields = new ObservableCollection<Airfield>(await _mobileService.GetTable<Airfield>().
                Where(x => x.UserId == userId).Take(500).OrderBy(x => x.Name).
               ToListAsync());
            return lookups;

        }

        

        public async Task InsertFlight(Flight flight)
        {
          
                await _mobileService.GetTable<Flight>().InsertAsync(flight);
           
        }



        public async Task DeleteFlight(Flight flight)
        {
            await _mobileService.GetTable<Flight>().DeleteAsync(flight);
            
            FlightsChanged = true;
            
        }

        public async Task SaveFlight(Flight flight)
        {

           // if (!flight.Valid())
           //     return ; // simply cancel if new and not complete

            if (flight.IsNew)
            {
                
                await InsertFlight(flight);
            }

            else
            {
                await _mobileService.GetTable<Flight>().UpdateAsync(flight);
            }
            FlightsChanged = true;
            
        }

      
      


        public async Task InsertAircraft(Aircraft aircraft)
        {
            await Insert<Aircraft>(aircraft);
            
        }

        public async Task InsertAircraftType(AcType acType)
        {
            
            await Insert(acType);
            
        }

        public async Task InsertAirfield(Airfield airfield)
        {
            await Insert(airfield);
            
        }


        public async Task UpdateAircraft(Aircraft aircraft)
        {
            await Update(aircraft);
            
        }

        public async Task DeleteAircraft(Aircraft f)
        {
            await Delete(f);
            
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
            where T: IEntity
        {
            await _mobileService.GetTable<T>().InsertAsync(item);
        }

        protected async override Task UpdateUserInternal(User user)
        {
           await Update(user);
        }

        public override async Task Update<T>(T item)
          
        {
            await _mobileService.GetTable<T>().UpdateAsync(item);
        }

        public async Task Delete<T>(T item)
            where T : IEntity
        {
            await _mobileService.GetTable<T>().DeleteAsync(item);
            
        }


        public async Task UpdateAirfield(Airfield airfield)
        {
             await Update(airfield);
        }


        public async Task DeleteAirfield(Airfield f)
        {
            await Delete(f);
        }


        public async Task UpdateAcType(AcType acType)
        {
            await Update(acType);
            
        }


        public async Task InsertAcType(AcType acType)
        {
            await Insert(acType);
            
        }


        public async Task CreateUserData(FlightData flightData, DateTime now)
        {


            await Insert(flightData.User);
            // rest of data will sync later
        }

        


        public bool FlightsChanged { get; set; }


        protected async override Task<User> GetUserInternal(string displayName)
        {
            try
            {
                var users = await _mobileService.GetTable<User>()
                    .Where(x => x.DisplayName == displayName)
                    .ToListAsync();
                return users.FirstOrDefault();

            }
            catch (Exception)
            {
                return null;


            }
        }
    }
}
