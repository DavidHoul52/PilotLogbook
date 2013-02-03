using System.Linq;
using System.Threading.Tasks;
using LogbookApp.ViewModel;

namespace LogbookApp.Views
{
    public class AircraftTypeViewModel : LookupViewModelBase
    {
        public async override Task Save()
        {
            await Flight.DataService.InsertAircraftType(Flight.AcType);
            Flight.Lookups.AcTypes.Add(Flight.AcType);

            Flight.AcTypeId =
                Flight.DataService.Lookups.AcTypes.Where(x => x.Code == Flight.AcType.Code).First().Id;
            Flight.Save();
        }
    }
}