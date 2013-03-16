using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class MaintainAircraftViewModel : ViewModelBase
    {
        private IFlightDataService flightDataService;

        public MaintainAircraftViewModel(IFlightDataService flightDataService)
        {
            this.flightDataService = flightDataService;
            EditCommand = new DelegateCommand<Aircraft>((f) => ShowDetail(f), (f) => { return f != null; });
            RaisePropertyChanged(() => EditCommand);
            DeleteCommand = new DelegateCommand<Aircraft>((f) => Delete(f), (f) => { return f != null; });
            RaisePropertyChanged(() => DeleteCommand);
            AddCommand = new DelegateCommand<Aircraft>((f) => Add(), (f) => { return true; });
            RaisePropertyChanged(() => AddCommand);
        }

        private void Add()
        {
            //Flights.Add(new Flight
            //{
            //    Lookups = flightDataService.Lookups,
            //    IsNew = true,
            //    DataService = flightDataService,
            //    Date = DateTime.Today,
            //    AircraftId = 1
            //});
            //SelectedFlight = Flights.Last();
            //ShowDetail(SelectedFlight);

        }




        private async void Delete(Aircraft f)
        {
            //bool deleted = await flightDataService.DeleteFlight(f);
            //if (deleted)
            //    Flights.Remove(f);
        }


        public Action<Aircraft> ShowDetail { get; set; }        


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
