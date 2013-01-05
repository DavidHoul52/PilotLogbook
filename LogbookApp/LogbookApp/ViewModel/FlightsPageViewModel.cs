using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LogbookApp.Data;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Xaml.Navigation;


namespace LogbookApp.ViewModel
{
    public class FlightsPageViewModel : ViewModelBase
    {
     
        private IFlightDataService flightDataService;

        public FlightsPageViewModel(IFlightDataService flightDataService)
        {

            this.flightDataService = flightDataService;
            EditCommand = new DelegateCommand<Flight>((f) => ShowDetail(f), (f) => { return f!=null; });
            RaisePropertyChanged(() => EditCommand);
            DeleteCommand = new DelegateCommand<Flight>((f) => DeleteFlight(f), (f) => { return f != null; });
            RaisePropertyChanged(() => DeleteCommand);
            AddCommand = new DelegateCommand<Flight>((f) => AddFlight(), (f) => { return true; });
            RaisePropertyChanged(() => AddCommand);
        }


        public async void Load()
        {
            bool loaded = await flightDataService.GetFlights();
            Flights = new ObservableCollection<Flight>(flightDataService.Flights);
            RaisePropertyChanged(() => Flights);
            
        }


        private void AddFlight()
        {
            Flights.Add(new Flight { Lookups= flightDataService.Lookups });
            SelectedFlight = Flights.Last();
            ShowDetail(SelectedFlight);

        }

        private async void DeleteFlight(Flight f)
        {
            bool deleted= await flightDataService.DeleteFlight(f);   
            if (deleted)
               Flights.Remove(f);
        }

        public ObservableCollection<Flight> Flights { get; set; }


        public Action<Flight> ShowDetail;




        private Flight selectedFlight;
        public Flight SelectedFlight
        {
            get
            { return selectedFlight; ;}

            set
            {
                selectedFlight = value;
                RaisePropertyChanged(() => SelectedFlight);
                
            }
        }

        public DelegateCommand<Flight> EditCommand
        {
            get;
            set;
        }

        public DelegateCommand<Flight> DeleteCommand
        {
            get;
            set;
        }

        public DelegateCommand<Flight> AddCommand
        {
            get;
            set;
        }
    }
}
