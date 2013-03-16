using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Commands;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class MaintainAircraftViewModel : ViewModelBase
    {
        private IFlightDataService flightDataService;

        public MaintainAircraftViewModel(IFlightDataService flightDataService)
        {
            this.flightDataService = flightDataService;
            EditCommand = new DelegateCommand<Aircraft>((f) => ShowDetail(new AircraftActionCommand { Aircraft= f, DataService = flightDataService}),
                (f) => { return f != null; });
            RaisePropertyChanged(() => EditCommand);
            DeleteCommand = new DelegateCommand<Aircraft>((f) => Delete(f), (f) => { return f != null; });
            RaisePropertyChanged(() => DeleteCommand);
            AddCommand = new DelegateCommand<Aircraft>((f) => Add(), (f) => { return true; });
            RaisePropertyChanged(() => AddCommand);
        }

        private Aircraft selected;
        public Aircraft SelectedAircraft
        {
            get
            { return selected; ;}

            set
            {
                selected = value;
                RaisePropertyChanged(() => SelectedAircraft);

            }
        }

        private void Add()
        {
            Aircraft.Add(new Aircraft
            {
                AircraftClass = AircraftClass.SEP,
                IsNew = true
                
            });
            SelectedAircraft = Aircraft.Last();
            ShowDetail(new AircraftActionCommand { Aircraft = SelectedAircraft, DataService = flightDataService });

        }




        private async void Delete(Aircraft f)
        {
            bool deleted = await flightDataService.DeleteAircraft(f);
            if (deleted)
                Aircraft.Remove(f);
        }


        public Action<AircraftActionCommand> ShowDetail { get; set; }        


        public DelegateCommand<Aircraft> EditCommand { get; set; }

        public DelegateCommand<Aircraft> DeleteCommand { get; set; }

        public DelegateCommand<Aircraft> AddCommand { get; set; }

        public ObservableCollection<Aircraft> Aircraft { get; set; }

        public async void Load()
        {

             await flightDataService.GetLookups();
            Aircraft = new ObservableCollection<Aircraft>(flightDataService.Lookups.Aircraft.OrderBy(x => x.Reg));
                
            RaisePropertyChanged(() => Aircraft);

        }
    }
}
