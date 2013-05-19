using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class FlightData
    {
        private ObservableCollection<Flight> _flights;

        public FlightData()
        {
            Flights = new ObservableCollection<Flight>();
            Lookups = new Lookups();
            User = new User();
        }

        public ObservableCollection<Flight> Flights
        {
            get { return _flights; }
            set
            {
                if (value != null )
                {
                    _flights = new ObservableCollection<Flight>(value.Select(x =>
                    {
                        x.Capacity = Lookups.Capacity.Where(c => c.Id == x.CapacityId).FirstOrDefault();
                        x.From = Lookups.Airfields.Where(airfield => airfield.id == x.AirfieldFromId).FirstOrDefault();
                        x.To = Lookups.Airfields.Where(airfield => airfield.id == x.AirfieldToId).FirstOrDefault();
                        x.Aircraft =
                            Lookups.Aircraft.Where(aircraft => aircraft.id == x.AircraftId)
                                .FirstOrDefault();
                        x.Lookups = Lookups;

                        return x;
                    }).ToList());
                }
            }
        }

        public Lookups Lookups { get; set; }
        public User User { get; set; }

        public void AddAcType(AcType acType)
        {
            acType.UserId = User.id;
            Lookups.AcTypes.Add(acType);
        }

        public void AddAirfield(Airfield @from)
        {
            from.UserId = User.id;
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
            flight.UserId = User.id;
            Flights.Add(flight);
        }

        public async Task RemoveAcType(AcType acType)
        {
            Lookups.AcTypes.Remove(acType);
        }

        public void AddAircraft(Aircraft aircraft)
        {
            aircraft.UserId = User.id;
            Lookups.Aircraft.Add(aircraft);
        }
    }
}