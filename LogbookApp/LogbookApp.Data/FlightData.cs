﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class FlightData
    {
        private List<Flight> _flights;

        public FlightData()
        {
            Flights = new List<Flight>();
            Lookups = new Lookups();
            User = new User();
        }

        public List<Flight> Flights
        {
            get { return _flights; }
            set
            {
                if (value != null )
                {
                    _flights = value.Select(x =>
                    {
                        x.Capacity = Lookups.Capacity.Where(c => c.Id == x.CapacityId).FirstOrDefault();
                        x.From = Lookups.Airfields.Where(airfield => airfield.Id == x.AirfieldFromId).FirstOrDefault();
                        x.To = Lookups.Airfields.Where(airfield => airfield.Id == x.AirfieldToId).FirstOrDefault();
                        x.Aircraft =
                            Lookups.Aircraft.Where(aircraft => aircraft.id == x.AircraftId)
                                .FirstOrDefault();
                        x.Lookups = Lookups;

                        return x;
                    }).ToList();
                }
            }
        }

        public Lookups Lookups { get; set; }
        public User User { get; set; }

        public void AddAcType(AcType acType)
        {
            Lookups.AcTypes.Add(acType);
        }

        public void AddInsert(Airfield @from)
        {
            from.UserId = User.Id;
            Lookups.Airfields.Add(from);
        }

        public void RemoveAircraft(Aircraft aircraft)
        {
            Lookups.Aircraft.Remove(aircraft);
        }

        public void RemoveAirfield(Airfield airfield)
        {
            Lookups.Airfields.Remove(airfield);
        }

        public void AddFlight(Flight flight)
        {
            flight.UserId = User.Id;
            Flights.Add(flight);
        }

        public async Task RemoveAcType(AcType acType)
        {
            Lookups.AcTypes.Remove(acType);
        }
    }
}