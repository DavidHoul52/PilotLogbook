using System.Collections.Generic;

namespace LogbookApp.Data
{
    public class FlightData
    {
        public FlightData()
        {
            Flights = new List<Flight>();
            Lookups = new Lookups();
            User = new User();
        }

        public List<Flight> Flights { get; set; }
        public Lookups Lookups { get; set; }
        public User User { get; set; }
    }
}