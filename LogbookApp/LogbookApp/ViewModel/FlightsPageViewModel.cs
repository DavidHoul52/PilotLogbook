using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Model;
using System.Collections.ObjectModel;
using LogbookApp.Services;


namespace LogbookApp.ViewModel
{
    public class FlightsPageViewModel : ViewModelBase
    {
        public FlightsPageViewModel()
        {
            Lookups lookups = new Lookups();
            Flights = new ObservableCollection<Flight>();
            for (int i = 0; i < 100; i++)
            {
                Flights.Add(new Flight
                {
                    Date = new DateTime(2012, 2, 2),
                    AcType = lookups.AcTypes.First(),
                    Reg = "G-ABCD",
                    Captain = "Self",
                    Capacity = lookups.Capacity.First(),
                    From = "Fairoaks",
                    To = "Goodwood",
                    Depart = new DateTime(2012, 2, 2, 10, 15, 0),
                    Arrival = new DateTime(2012, 2, 2, 11, 15, 0)
                });
            }

            EditCommand = new DelegateCommand<Flight>((f) => ShowDetail(f), (f) => { return f!=null; });
            RaisePropertyChanged(() => EditCommand);
            DeleteCommand = new DelegateCommand<Flight>((f) => DeleteFlight(f), (f) => { return f != null; });
            RaisePropertyChanged(() => DeleteCommand);
            AddCommand = new DelegateCommand<Flight>((f) => AddFlight(), (f) => { return true; });
            RaisePropertyChanged(() => AddCommand);
        }

        private void AddFlight()
        {
            Flights.Add(new Flight { Captain="Haddock"});
            SelectedFlight = Flights.Last();
            ShowDetail(SelectedFlight);

        }

        private void DeleteFlight(Flight f)
        {
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
