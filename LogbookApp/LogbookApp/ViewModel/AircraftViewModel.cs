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
            if (Aircraft.IsNew)
            {
                await DataService.InsertAircraft(Aircraft);
                if (Flight!=null)
                   Flight.Lookups.Aircraft.Add(Aircraft);
            }
            else
            {
                await DataService.UpdateAircraft(Aircraft);
            }


            //Flight.AircraftId =
            //    Flight.DataService.Lookups.Aircraft.Where(x => x.Reg == Flight.Aircraft.Reg).First().id;

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
            Aircraft = Flight.Aircraft;
        }

        
    }
}
