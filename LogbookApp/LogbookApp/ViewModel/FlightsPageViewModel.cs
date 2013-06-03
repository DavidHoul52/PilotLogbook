using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.Commands;
using Windows.System.UserProfile;


namespace LogbookApp.ViewModel
{
    public class FlightsPageViewModel : ViewModelBase
    {
     
        private IFlightDataManager flightDataManager;


        public FlightsPageViewModel(IFlightDataManager flightData)
        {
            this.flightDataManager = flightData;
            
            
            EditCommand = new DelegateCommand<Flight>((f) => ShowDetail(f), (f) => f!=null);
            RaisePropertyChanged(() => EditCommand);
            DeleteCommand = new DelegateCommand<Flight>(DeleteFlight, (f) => f != null);
            RaisePropertyChanged(() => DeleteCommand);
            AddCommand = new DelegateCommand<Flight>((f) => AddFlight(), (f) => true);
            RaisePropertyChanged(() => AddCommand);
            TotalsCommand = new DelegateCommand<List<Flight>>((f) =>    ShowTotals(new TotalsActionCommand
            {
                Flights = this.Flights,
                FromDate = new DateTime(1900,1,1),
                ToDate = DateTime.Now
            })
                , (f) => { return true; });
            RaisePropertyChanged(() => TotalsCommand);
            MaintainAircraftCommand = new DelegateCommand<List<Flight>>((f) => ShowAircraft() , (f) => { return true; });
            RaisePropertyChanged(() => MaintainAircraftCommand);
            MaintainAirfieldsCommand = new DelegateCommand<List<Flight>>((f) => ShowAirfields(), (f) => { return true; });
            RaisePropertyChanged(() => MaintainAirfieldsCommand);
            MaintainAircraftTypesCommand = new DelegateCommand<List<Flight>>((f) => ShowAircraftTypes(), (f) => { return true; });
            RaisePropertyChanged(() => MaintainAircraftTypesCommand);

        }

        public async void  Load()
        {

            await App.RefreshFlightData();
            Refresh();
        }

        public void Refresh()
        {
            if (flightDataManager.FlightData.Flights != null)
                Flights = new ObservableCollection<Flight>(flightDataManager.FlightData.Flights.OrderByDescending(x => x.Depart).
                    OrderByDescending(x => x.Date).ToList());
            RaisePropertyChanged(() => Flights);
        }


        private void AddFlight()
        {
            Flights.Add(new Flight
            {
                UserId = flightDataManager.FlightData.User.id,
                Lookups = flightDataManager.FlightData.Lookups,
                IsNew = true, 
            Date = DateTime.Today, AircraftId = 1});
            SelectedFlight = Flights.Last();
            ShowDetail(SelectedFlight);

        }

       


        private async void DeleteFlight(Flight f)
        {
            await flightDataManager.DeleteFlight(f, DateTime.Now);   
            Load();
            
        }

        public ObservableCollection<Flight> Flights { get; set; }


        public Action<Flight> ShowDetail { get; set; }

        public Action<TotalsActionCommand> ShowTotals { get; set; }

        public Action ShowAircraft { get; set; }

        public Action ShowAirfields { get; set; }

        public Action ShowAircraftTypes { get; set; }
        

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

        public DelegateCommand<List<Flight>> MaintainAirfieldsCommand { get; set; }

        public DelegateCommand<List<Flight>> MaintainAircraftTypesCommand { get; set; }

        
    }
}
