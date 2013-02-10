using System.Collections.ObjectModel;
using System.Linq;
using LogbookApp.Commands;
using LogbookApp.Data;
using System;
using System.Collections.Generic;

namespace LogbookApp.ViewModel
{
    public class FlightDetailPageViewModel : ViewModelBase
    {
        public FlightDetailPageViewModel()
        {
            AddAircraftCommand = new DelegateCommand<Flight>((f) => AddAircraft(), (f) => { return true; });
            RaisePropertyChanged(() => AddAircraftCommand);
            AddAircraftTypeCommand = new DelegateCommand<Flight>((f) => AddAircraftType(), (f) => { return true; });
            RaisePropertyChanged(() => AddAircraftTypeCommand);
            AddAirfieldFromCommand = new DelegateCommand<Flight>((f) => AddAirfield(AirfieldDesignation.From), (f) => { return true; });
            RaisePropertyChanged(() => AddAirfieldFromCommand);
            AddAirfieldToCommand = new DelegateCommand<Flight>((f) => AddAirfield(AirfieldDesignation.To), (f) => { return true; });
            RaisePropertyChanged(() => AddAirfieldToCommand);
            Numbers = new ObservableCollection<int>();
            for (int i = 0; i <= 20; i++)
            {
                Numbers.Add(i);
            }
            RaisePropertyChanged(() => Numbers);
            
            TotalsCommand = new DelegateCommand<Flight>((f) => ShowTotals(), (f) => { return true; });
            RaisePropertyChanged(() => TotalsCommand);
            
        }

        private void ShowTotals()
        {
            ShowTotalsAction(new TotalsActionCommand { Flights= Flight.DataService.Flights, ToDate=Flight.Date});
        }

     


        private T FlightActionCommand<T>()
            where T:FlightActionCommand, new()
        {
            return new T
                {
                    Flight = this.Flight,
                    OnCompleted = (f) =>
                        {
                            Flight = f;


                        }
                };
        }



        private void AddAircraftType()
        {
            Flight.AcType = new AcType() { };

            ShowAircraftType(FlightActionCommand<FlightActionCommand>());
        }

        private void AddAircraft()
        {
            Flight.Aircraft = new Aircraft { };

            ShowAircraft(FlightActionCommand<FlightActionCommand>());

            
        }

        private void AddAirfield(AirfieldDesignation airfieldDesignation)
        {

            switch (airfieldDesignation)
            {
                case AirfieldDesignation.From:
                    Flight.From = new Airfield { };
                    break;
                case AirfieldDesignation.To:
                    Flight.To = new Airfield();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("airfieldDesignation");
            }


            FlightAirfieldActionCommand flightAirfieldActionCommand = FlightActionCommand<FlightAirfieldActionCommand>();
            flightAirfieldActionCommand.AirfieldDesignation = airfieldDesignation;
            ShowAirfield(flightAirfieldActionCommand);
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


                RaisePropertyChanged(() => Flight.Lookups.Aircraft);
                RaisePropertyChanged(() => Flight);
                RaisePropertyChanged(() => Depart);
                RaisePropertyChanged(() => Arrival);
              
           

            }
            }


     public AcType SelectedAcType { get; set; }

        public DelegateCommand<Flight> AddAircraftCommand { get; set; }
        public DelegateCommand<Flight> AddAircraftTypeCommand { get; set; }
        public DelegateCommand<Flight> AddAirfieldFromCommand { get; set; }
        public DelegateCommand<Flight> AddAirfieldToCommand { get; set; }

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

        public Action<FlightActionCommand> ShowAircraft { get; set; }
        public Action<FlightActionCommand> ShowAircraftType { get; set; }

        public Action<FlightAirfieldActionCommand> ShowAirfield { get; set; }


        public void SaveFlight()
        {
          
            Flight.DataService.SaveFlight(Flight);

        }


        public ObservableCollection<int> Numbers { get; set; }




        public DelegateCommand<Flight> TotalsCommand { get; set; }

        public Action<TotalsActionCommand> ShowTotalsAction;
    }
}
