using System;

namespace LogbookApp.Data
{
    public class FlightFactory
    {
        public Flight CreateFlight(FlightData flightData, DateTime when)
        {
            return new Flight
            {
                UserId = flightData.User.id,
                Lookups = flightData.Lookups,
                Capacities = flightData.InMemoryLookups.Capacities,
                IsNew = true,
                Date = when
                
                
            };
        }
    }
}