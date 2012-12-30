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

        

        public IEnumerable<Flight> Flights
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<AcType> AcTypes
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Airfield> Airfields
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Capacity> Capacitys
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<Flight>> GetFlights()
        {
            var _serviceUri = @"http://localhost:49960/PilotsLogBookData.svc/";
            var _oDataClient = new ServiceReference.PilotsLogBookContainer(new Uri(_serviceUri));

            var query = (DataServiceQuery<ServiceReference.Flight>)_oDataClient.Flights;
            var data = await query.ExecuteAsync<ServiceReference.Flight>(); // extention class below

            
            return data.Select(x => new Flight { Id = (int)x.Id  });

            // TODO: complete the object graph

        }

        public Task<Lookups> GetLookups()
        {
            throw new NotImplementedException();
        }

        public Lookups Lookups
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

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
