using System.Linq;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class AirfieldViewModel : LookupViewModelBase
    {
        public AirfieldViewModel()
        {
            Airfield = new Airfield();
        }

        public Airfield Airfield { get; set; }


        public override async Task Save()
        {

            
            

           // Flight.Save();
        }

        public async Task SaveFrom()
        {
            await Flight.DataService.InsertAirfield(Airfield);
            Flight.Lookups.Airfields.Add(Airfield);
            Flight.From = Airfield;
         
            await Save();

        }

        public async Task SaveTo()
        {
            await Flight.DataService.InsertAirfield(Airfield);
            Flight.Lookups.Airfields.Add(Airfield);
            Flight.To = Airfield;
           
           await Save();
        }
    }
}