using System.Linq;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class AirfieldViewModel : LookupViewModelBase
    {
        public AirfieldViewModel()
        {
          
        }

        private Airfield airfield;
        public Airfield Airfield
        {
            get
            { return airfield ;}
            set
            { 
                airfield=value;
                RaisePropertyChanged(() => Airfield);
            
            }
        }


        public override async Task Save()
        {
            if (Airfield.Valid())
            {
                if (Airfield.IsNew)
                {
                    await DataService.InsertAirfield(Airfield);
                    if (Flight != null)
                        Flight.Lookups.Airfields.Add(Airfield);
                }
                else
                    await DataService.UpdateAirfield(Airfield);
            }





        }

        public async Task SaveFrom()
        {
            await Save();
            Flight.From = Airfield;
         
            

        }

        public async Task SaveTo()
        {
            await Save();
            Flight.To = Airfield;
           
           
        }

        protected override void OnFlightUpdated()
        {
            base.OnFlightUpdated();
            
        }
    }
}