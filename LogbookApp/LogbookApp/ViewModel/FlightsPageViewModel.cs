using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LogbookApp.Data;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Xaml.Navigation;
using LogbookApp.Commands;


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
            TotalsCommand = new DelegateCommand<List<Flight>>((f) =>    ShowTotals(new TotalsActionCommand
            {
                Flights = Flights.ToList(),
                FromDate = new DateTime(1900,1,1),
                ToDate = DateTime.Now
            })
                , (f) => { return true; });
            RaisePropertyChanged(() => TotalsCommand);
            MaintainAircraftCommand = new DelegateCommand<List<Flight>>((f) => ShowAircraft() , (f) => { return true; });
            RaisePropertyChanged(() => MaintainAircraftCommand);

        }

      
     

      

        public async void Load()
        {
         
            bool loaded = await flightDataService.GetFlights();
            Flights = new ObservableCollection<Flight>(flightDataService.Flights.OrderByDescending(x=>x.Depart).
                OrderByDescending(x=>x.Date));
            RaisePropertyChanged(() => Flights);
            
        }


        private void AddFlight()
        {
            Flights.Add(new Flight { Lookups= flightDataService.Lookups, IsNew = true, DataService = flightDataService,
            Date = DateTime.Today, AircraftId = 1});
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


        public Action<Flight> ShowDetail { get; set; }

        public Action<TotalsActionCommand> ShowTotals { get; set; }

        public Action ShowAircraft { get; set; }


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

        public DelegateCommand<List<Flight>> TotalsCommand
        {
            get;
            set;
        }

        public DelegateCommand<List<Flight>> MaintainAircraftCommand { get; set; }
    }
}
