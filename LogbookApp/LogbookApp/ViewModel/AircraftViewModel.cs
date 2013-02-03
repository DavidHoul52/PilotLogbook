using System.Linq;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class AircraftViewModel : ViewModelBase
    {
        private Flight _flight;
        

        public Flight Flight
        {
            get { return _flight; }
            set
            {
                _flight = value;
                RaisePropertyChanged(()=>Reg);
            }
        }

        public string Reg

        {
            get { if (Flight != null) return Flight.Aircraft.Reg;
                return "";
            }
            set
            {
                Flight.Aircraft.Reg = value;
                
            }
        }

        public async Task Save()
        {
            await Flight.DataService.InsertAircraft(Flight.Aircraft);
            Flight.Lookups.Aircraft.Add(Flight.Aircraft);
          //  await Flight.DataService.GetLookups();
          //  Flight.Lookups = Flight.DataService.Lookups;
            Flight.AircraftId =
                Flight.DataService.Lookups.Aircraft.Where(x => x.Reg == Reg).First().id;
            Flight.Save();
        }
    }
}
