using System.Linq;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class AircraftViewModel : LookupViewModelBase
    {
        public override async Task Save()
        {
             await Flight.DataService.InsertAircraft(Flight.Aircraft);
            Flight.Lookups.Aircraft.Add(Flight.Aircraft);
        
            Flight.AircraftId =
                Flight.DataService.Lookups.Aircraft.Where(x => x.Reg == Flight.Aircraft.Reg).First().id;
            Flight.Save();
        }
    }
}
