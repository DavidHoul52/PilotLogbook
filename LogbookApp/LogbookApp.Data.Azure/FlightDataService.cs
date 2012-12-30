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

     
        public IEnumerable<Flight> Flights { get; set; }
        public IEnumerable<AcType> AcTypes {get; set;}
        public IEnumerable<Airfield> Airfields { get; set; }
        public IEnumerable<Capacity> Capacitys { get; set; }


        public async Task<IEnumerable<Flight>> GetFlights()
        {

 
            AcTypes = await _mobileService.GetTable<AcType>().ReadAsync();
            Airfields = await _mobileService.GetTable<Airfield>().ReadAsync();
            Capacitys = await _mobileService.GetTable<Capacity>().ReadAsync();
            Lookups = await GetLookups();
            var flights = await _mobileService.GetTable<Flight>().ReadAsync() ;
            Flights = flights.Select(x => {
              
                x.AcType = AcTypes.Where(a => a.Id == x.AcTypeId).FirstOrDefault();
                x.Capacity = Capacitys.Where(c=>c.Id==x.CapacityId).FirstOrDefault();
                x.From = Airfields.Where(airfield=>airfield.Id==x.FromAirfieldId).FirstOrDefault();
                x.To = Airfields.Where(airfield=>airfield.Id==x.ToAirfieldId).FirstOrDefault();
                x.Lookups = Lookups;
                return x;
            });

            return Flights;

            
        }

        public async Task<Lookups> GetLookups()
        {
            var lookups = new Lookups();
            lookups.Load();
            return lookups;

        }

        public Lookups Lookups { get; set; }

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




    }
}
