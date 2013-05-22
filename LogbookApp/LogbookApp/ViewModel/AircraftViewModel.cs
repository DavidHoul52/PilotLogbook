using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using LogbookApp.Commands;
using LogbookApp.Data;
using LogbookApp.Views;

namespace LogbookApp.ViewModel
{
    public class AircraftViewModel : LookupViewModelBase
    {

        public AircraftViewModel()
        {
            Classes = new ObservableCollection<AircraftClass>(AircraftClass.Items);

            AddAircraftTypeCommand = new DelegateCommand<AircraftClass>((f) => AddAircraftType(), (f) => true);
            RaisePropertyChanged(() => AddAircraftTypeCommand);


        }

        

        public DelegateCommand<AircraftClass> AddAircraftTypeCommand { get; set; }


        private void AddAircraftType()
        {
            Aircraft.AcType = new AcType { IsNew = true };

            ShowAircraftType(new AircraftActionCommand { Aircraft = this.Aircraft, DataService = this.DataService});
        }

    
        public Action<AircraftActionCommand> ShowAircraftType { get; set; }

        public override async Task Save()
        {
           


            if (Aircraft.Valid())
            {
                if (Aircraft.IsNew)
                {
                    if (IsDuplicate())
                    {
                        await Messager.ShowMessage("This aircraft has already been added.");
                        return;
                    }
                    await DataService.InsertAircraft(Aircraft,DateTime.Now);
                    if (Flight != null)
                    {
                        Flight.Lookups.Aircraft.Add(Aircraft);
                        Flight.Aircraft = Aircraft;
                    }
                }
                else
                {
                    await DataService.UpdateAircraft(Aircraft, DateTime.Now);
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
