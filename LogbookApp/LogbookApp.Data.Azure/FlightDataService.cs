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

        public FlightDataService(MobileServiceClient mobileService)
        {

            _mobileService = mobileService;
            
        }

     
        public List<Flight> Flights { get; set; }
     


        public async Task<bool> GetFlights()
        {

 
            //AcTypes = await _mobileService.GetTable<AcType>().ReadAsync();
            //Airfields = await _mobileService.GetTable<Airfield>().ReadAsync();
            //Capacitys = await _mobileService.GetTable<Capacity>().ReadAsync();
            await GetLookups();
            var flights = await _mobileService.GetTable<Flight>().ReadAsync() ;
            Flights = flights.Select(x => {
              
                x.AcType = Lookups.AcTypes.Where(a => a.Id == x.AcTypeId).FirstOrDefault();
                x.Capacity = Lookups.Capacity.Where(c=>c.Id==x.CapacityId).FirstOrDefault();
                x.From = Lookups.Airfields.Where(airfield=>airfield.Id==x.AirfieldFromId).FirstOrDefault();
                x.To = Lookups.Airfields.Where(airfield=>airfield.Id==x.AirfieldToId).FirstOrDefault();
                x.Lookups = Lookups;
                x.DataService = this;
                return x;
            }).ToList();

            return true;

            
        }

        public async Task GetLookups()
        {
            Lookups = new Lookups(_mobileService);
            await Lookups.Load();
            

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

        public async void SaveFlight(Flight flight)
        {
         
            if (flight.IsNew)
               await InsertFlight(flight);
            else
                await _mobileService.GetTable<Flight>().UpdateAsync(flight);
        }

        public void SaveFlights()
        {
            foreach (var flight in Flights)
            {
                SaveFlight(flight);
            }
            
        }

        public async void SaveTest()
        {
            //await ClearTable<AcType>();
            //await Insert(new AcType { Code = "C-152" });
            //await ClearTable<Airfield>();
            //await Insert(new Airfield { ICAOCode = "EGHR", Name = "Goodwood" });
            //await ClearTable<Capacity>();
            //await Insert(new Capacity { Description = "P1" });
            //await ClearTable<Aircraft>();
            //await Insert(new Aircraft { Reg = "C-152" });
         //   await GetLookups();
            await Insert(new Flight
            {
                AcType = Lookups.AcTypes.FirstOrDefault(),
                Arrival = DateTime.Now,
                Date = DateTime.Today,
                Depart = DateTime.Now,
                From = Lookups.Airfields.FirstOrDefault(),
                To = Lookups.Airfields.FirstOrDefault(),
                Capacity = Lookups.Capacity.FirstOrDefault(),
                Captain = "haddock",
                Aircraft = Lookups.Aircraft.FirstOrDefault()
            });
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
    }
}
