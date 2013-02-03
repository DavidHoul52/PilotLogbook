using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data.LocalOData
{
    public class LocalFlightDataService : IFlightDataService
    {



        public List<Flight> Flights { get; set; }
       
        public IEnumerable<Capacity> Capacitys  {get; set;}
        

        public async Task<bool> GetFlights()
        {
            var _serviceUri = @"http://localhost:49960/PilotsLogBookData.svc/";
            var _oDataClient = new ServiceReference.PilotsLogBookContainer(new Uri(_serviceUri));

            var query = (DataServiceQuery<ServiceReference.Flight>)_oDataClient.Flights;
            var data = await query.ExecuteAsync<ServiceReference.Flight>(); // extention class below

            
            this.Flights= data.Select(x => new Flight { id = (int)x.Id  }).ToList();
            return true;

            // TODO: complete the object graph

        }

        public Task GetLookups()
        {
            throw new NotImplementedException();
        }

        public ILookups Lookups { get; set; }
        

        public Task<bool> InsertFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public void SaveFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public void SaveFlights()
        {
            throw new NotImplementedException();
        }

        public void SaveTest()
        {
            throw new NotImplementedException();
        }

        public Task InsertAircraft(Aircraft aircraft)
        {
            throw new NotImplementedException();
        }

        public Task InsertAircraftType(AcType acType)
        {
            throw new NotImplementedException();
        }
    }

    public static class WcfDataServicesExtensions
    {
        public static async Task<IEnumerable<TResult>> ExecuteAsync<TResult>(this DataServiceQuery<TResult> query)
        {
            var queryTask = Task.Factory.FromAsync<IEnumerable<TResult>>(query.BeginExecute(null, null), (asResult) =>
            {
                var result = query.EndExecute(asResult).ToList();
                return result;
            });

            return await queryTask;
        }
    }
}
