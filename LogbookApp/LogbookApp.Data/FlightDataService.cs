using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class FlightDataService
    {
        private MobileServiceClient _mobileService;

        public FlightDataService(MobileServiceClient mobileService)
        {

            _mobileService = mobileService;
            
        }

     


        public async Task<List<Flight>> GetAllFlights()
        {

            var flights = await _mobileService.GetTable<Flight>().ReadAsync() ;
            var AcTypes = await _mobileService.GetTable<AcType>().ReadAsync();
            return flights.Select(x => { x.AcType = AcTypes.Where(a => a.Id == x.AcTypeId).FirstOrDefault(); return x; }).ToList();
            
        }


    }
}
