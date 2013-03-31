using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class AircraftViewModel : LookupViewModelBase
    {

        public AircraftViewModel()
        {
            Classes = new ObservableCollection<AircraftClass>(AircraftClass.Items);


        }


        public override async Task Save()
        {
            if (Aircraft.Valid())
            {
                if (Aircraft.IsNew)
                {
                    await DataService.InsertAircraft(Aircraft);
                    if (Flight != null)
                    {
                        Flight.Lookups.Aircraft.Add(Aircraft);
                        Flight.Aircraft = Aircraft;
                    }
                }
                else
                {
                    await DataService.UpdateAircraft(Aircraft);
                }
            }


       

        }

        public ObservableCollection<AircraftClass> Classes { get; set; }

        private Aircraft aircraft;
        public Aircraft Aircraft
        {
            get

            { return aircraft; }

            set

            { aircraft = value; 
            RaisePropertyChanged(()=>Aircraft);}
        }

        protected override void OnFlightUpdated()
        {
            base.OnFlightUpdated();
            
            Aircraft = Flight.Aircraft;
        }

        
    }
}
