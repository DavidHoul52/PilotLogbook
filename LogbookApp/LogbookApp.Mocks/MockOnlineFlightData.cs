using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using BaseData;
using LogbookApp.Data;
using OnlineOfflineSyncLibrary.TestMocks;

namespace LogbookApp.Mocks
{
    public class MockOnlineFlightData : MockOnlineDataService<FlightData, User>, IOnlineFlightDataService
    {
      


       public async Task InsertFlight(Flight flight)
       {
           await Insert(flight);
       }

       public async Task DeleteFlight(Flight flight)
       {
           await Delete(flight);
       }

        public async Task UpdateFlight(Flight flight)
        {
            await UpdateFlight(flight);
        }

     

       public async Task InsertAircraft(Aircraft aircraft)
       {
           await Insert(aircraft);
       }

       public async Task InsertAircraftType(AcType acType)
        {
            
        }

       public async Task InsertAirfield(Airfield @from)
       {
           await Insert(@from);
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
            
        }

       public async Task DeleteAcType(AcType acType)
        {
            
        }

       public async Task<ObservableCollection<Flight>> GetFlights(int userId)
       {
           return InternalData.Flights;
       }

       public async Task<Lookups> LoadLookups(int userId)
       {
           return InternalData.Lookups;
       }

        protected async override Task InternalCreateUserData(string userName)
        {
            base.InternalCreateUserData(userName);
            
        }

        protected override FlightData CopyOfInternalData()
        {
            var flightData= new FlightData();
            flightData.Lookups.AcTypes = Copy(InternalData.Lookups.AcTypes);
            flightData.Lookups.Aircraft = Copy(InternalData.Lookups.Aircraft);
            flightData.Lookups.Airfields = Copy(InternalData.Lookups.Airfields);
            flightData.Flights = Copy(InternalData.Flights);
            flightData.User = CreateCopy(InternalData.User);
            return flightData;
        }

     

        public async override Task Insert<T>(T item)
        {
            if (typeof (T) == typeof (Aircraft))
            {
                var aircraft = CreateCopy(item as Aircraft);
                InternalData.AddAircraft(aircraft);
            }
            if (typeof(T) == typeof(Airfield))
                InternalData.AddAirfield(CreateCopy(item as Airfield));
            if (typeof(T) == typeof(AcType))
                InternalData.AddAcType(CreateCopy(item as AcType));
            if (typeof (T) == typeof (Flight))
            {
                
                InternalData.AddFlight(CreateCopy(item as Flight));
            }
        }

      

        public async override Task Delete<T>(T item) 
        {
            
            if (item.id==0)
                throw new Exception();
            if (typeof(T) == typeof(Aircraft))
                InternalData.DeleteAircraft(InternalData.Lookups.Aircraft.FirstOrDefault(x=>x.id==item.id));
            if (typeof(T) == typeof(Airfield))
                InternalData.DeleteAirfield(InternalData.Lookups.Airfields.FirstOrDefault(x => x.id == item.id));
            if (typeof(T) == typeof(AcType))
                InternalData.DeleteAcType(InternalData.Lookups.AcTypes.FirstOrDefault(x => x.id == item.id));
            if (typeof(T) == typeof(Flight))
                InternalData.DeleteFlight(InternalData.Flights.FirstOrDefault(x=>x.id==item.id));
        }

        public async override Task Update<T>(T item)
        {
            if (typeof(T) == typeof(Aircraft))
                InternalData.UpdateAircraft(item as Aircraft);
            if (typeof(T) == typeof(Airfield))
                InternalData.UpdateAirfield(item as Airfield);
            if (typeof(T) == typeof(AcType))
                InternalData.UpdateAcType(item as AcType);
            if (typeof(T) == typeof(Flight))
                InternalData.UpdateFlight(item as Flight);
        }

     
    }
}