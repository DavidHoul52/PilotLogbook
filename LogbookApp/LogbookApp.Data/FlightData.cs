using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BaseData;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.Data
{
    public class FlightData : ISyncableData<User>
    {
        private ObservableCollection<Flight> _flights;
        private User _user;

        public FlightData()
        {
            Flights = new ObservableCollection<Flight>();
            Lookups = new Lookups();
            User = new User();
            InMemoryLookups = new InMemoryLookups();
        }

        public InMemoryLookups InMemoryLookups { get; set; }

        public ObservableCollection<Flight> Flights
        {
            get { return _flights; }
            set
            {
                if (value != null )
                {
                    _flights = new ObservableCollection<Flight>(value.Select(x =>
                    {
                        x.Capacity = InMemoryLookups.Capacities.Where(c => c.Id == x.CapacityId).FirstOrDefault();
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

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
            }
        }

        public bool CanDelete<T>(T item) where T : IEntity
        {
            if (typeof (T) == typeof (Aircraft))
                return Flights.All(x => x.Aircraft != item as Aircraft);
            if (typeof(T) == typeof(AcType))
                return Lookups.Aircraft.All(x => x.AcType != item as AcType);
            if (typeof(T) == typeof(Airfield))
                return !Flights.Any(x => x.From == item as Airfield || x.To==item as Airfield);
            return true;
        }

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


        public void DeleteAircraft(Aircraft aircraft)
        {
            
            Lookups.Aircraft.Remove(aircraft);
        }

        public void DeleteAirfield(Airfield airfield)
        {
            Lookups.Airfields.Remove(airfield);
        }

        public void DeleteFlight(Flight flight)
        {
            Flights.Remove(flight);
        }

        public void DeleteAcType(AcType acType)
        {
            Lookups.AcTypes.Remove(acType);
        }

        public void UpdateAircraft(Aircraft aircraft)
        {
            Update(aircraft,Lookups.Aircraft);
          
            

        }

        private void Update<T>(T item, ObservableCollection<T> list )
            where T:IEntity
        {
            var foundInList = list.Where(x => x.id == item.id).FirstOrDefault();
            list.Remove(foundInList);
            list.Add(item);
            
        }

        public void UpdateAirfield(Airfield airfield)
        {
            Update(airfield, Lookups.Airfields);
        }

        public void UpdateAcType(AcType acType)
        {
            Update(acType, Lookups.AcTypes);
        }

        public void UpdateFlight(Flight flight)
        {
            Update(flight, Flights);
        }
    }
}