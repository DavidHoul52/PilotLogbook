using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using LogbookApp.Data;
using LogbookApp.Views;

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
            if (IsDuplicate())
            {
                await Messager.ShowMessage("This aircraft has already been added.");
                return;
            }


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

        private bool IsDuplicate()
        {
            return Lookups.Aircraft.FirstOrDefault(x => x.Reg == Aircraft.Reg && Aircraft!=x) != null;
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
