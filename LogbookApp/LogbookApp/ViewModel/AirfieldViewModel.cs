using System;
using System.Linq;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.Views;

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
                    if (IsDuplicate())
                    {
                        await Messager.ShowMessage("This airfield has already been added.");
                        return;
                    }
                    await DataService.InsertAirfield(Airfield, DateTime.Now);
                    if (Flight != null)
                        Flight.Lookups.Airfields.Add(Airfield);
                }
                else
                    await DataService.UpdateAirfield(Airfield, DateTime.Now);
            }





        }

        private bool IsDuplicate()
        {
            return Lookups.Airfields.FirstOrDefault(x => x.Name == Airfield.Name && x!=Airfield) != null;
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