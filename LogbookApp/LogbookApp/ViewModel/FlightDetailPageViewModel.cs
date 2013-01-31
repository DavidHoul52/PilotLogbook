using System.Collections.ObjectModel;
using System.Linq;
using LogbookApp.Data;
using System;

namespace LogbookApp.ViewModel
{
    public class FlightDetailPageViewModel : ViewModelBase
    {
        public FlightDetailPageViewModel()
        {
            AddAircraftCommand = new DelegateCommand<Flight>((f) => AddAircraft(), (f) => { return true; });
            RaisePropertyChanged(() => AddAircraftCommand);
            
        }

        private void AddAircraft()
        {
            ShowAircraftList(Flight);
        }


       

       

        private Flight _flight;
        public Flight Flight {
            get
            {
                return _flight; ;
            }
            set
            {
                _flight = value;
               
             
             
                RaisePropertyChanged(() => Flight);
                RaisePropertyChanged(() => Depart);
                RaisePropertyChanged(() => Arrival);
           

            }
            }


     public AcType SelectedAcType { get; set; }

        public DelegateCommand<Flight> AddAircraftCommand { get; set; }
        public DelegateCommand<Flight> AddAirfieldCommand { get; set; }

        public DateTime? Depart
        {
            get {

                if (Flight != null)
                    return Flight.Depart;
                else
                    return null;
            }
            set {
                   Flight.Depart = value.Value;
                   RaisePropertyChanged(() => Flight);
               }
        }


        public DateTime? Arrival
        {
            get {
                if (Flight != null)
                    return Flight.Arrival;
                else
                    return null;
            }
            set
            {
                Flight.Arrival = value.Value;
                RaisePropertyChanged(() => Flight);
            }
        }

        public Action<Flight> ShowAircraftList { get; set; }


        public void SaveFlights()
        {
          //  Flight.DataService.SaveTest();
            //var flight = new Flight
            //    {
            //        AcType = Lookups.AcTypes.FirstOrDefault(),
            //        Arrival = DateTime.Now,
            //        Date = DateTime.Today,
            //        Depart = DateTime.Now,
            //        From = Lookups.Airfields.FirstOrDefault(),
            //        To = Lookups.Airfields.FirstOrDefault(),
            //        Capacity = Lookups.Capacity.FirstOrDefault(),
            //        Captain = "haddock",
            //        Aircraft = Lookups.Aircraft.FirstOrDefault(),
            //        IsNew = true
            //    };
            Flight.DataService.SaveFlight(this.Flight);

            //if (Flight.IsNew)
            //    Flight.DataService.Flights.Add(this.Flight);
            //Flight.DataService.SaveFlights();
        }
    }
}
