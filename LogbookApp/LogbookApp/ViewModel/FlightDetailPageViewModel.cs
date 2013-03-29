using System.Collections.ObjectModel;
using System.Linq;
using LogbookApp.Commands;
using LogbookApp.Data;
using System;
using System.Threading.Tasks;

namespace LogbookApp.ViewModel
{
    public class FlightDetailPageViewModel : ViewModelBase
    {
        public FlightDetailPageViewModel()
        {
            Init();
        }

        private void Init()
        {
            AddAircraftCommand = new DelegateCommand<Flight>((f) => AddAircraft(), (f) => { return true; });
            RaisePropertyChanged(() => AddAircraftCommand);

            AddAirfieldFromCommand = new DelegateCommand<Flight>((f) => AddAirfield(AirfieldDesignation.From),
                                                                 (f) => { return true; });
            RaisePropertyChanged(() => AddAirfieldFromCommand);
            AddAirfieldToCommand = new DelegateCommand<Flight>((f) => AddAirfield(AirfieldDesignation.To),
                                                               (f) => { return true; });
            RaisePropertyChanged(() => AddAirfieldToCommand);
            LandingTimes = new ObservableCollection<int>();
            for (int i = 0; i <= 20; i++)
            {
                LandingTimes.Add(i);
            }
            RaisePropertyChanged(() => LandingTimes);

            TotalsCommand = new DelegateCommand<Flight>((f) => ShowTotals(), (f) => { return true; });
            RaisePropertyChanged(() => TotalsCommand);
        }

        private void ShowTotals()
        {
            ShowTotalsAction(new TotalsActionCommand { Flights= Flight.DataService.Flights,
                FromDate = new DateTime(1900,1,1),
                ToDate=Flight.Date});
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



    

        private void AddAircraft()
        {
            Flight.Aircraft = new Aircraft { IsNew = true };
            
            ShowAircraft(FlightActionCommand<FlightActionCommand>());

            
        }

        private void AddAirfield(AirfieldDesignation airfieldDesignation)
        {

            FlightAirfieldActionCommand flightAirfieldActionCommand = FlightActionCommand<FlightAirfieldActionCommand>();
            flightAirfieldActionCommand.AirfieldDesignation = airfieldDesignation;
            flightAirfieldActionCommand.Airfield = new Airfield { IsNew = true };
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
                RaisePropertyChanged(() => Landings);
              
           

            }
            }


        public int Landings
        {
            get
            {
                
                if (Flight.LDG != null) return Flight.LDG.Value;
                return 0;
            }
            set
            {
                Flight.LDG = value;
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
        

        public Action<FlightAirfieldActionCommand> ShowAirfield { get; set; }


        public async Task<bool> SaveFlight()
        {
          
            return await Flight.DataService.SaveFlight(Flight);

        }


        public ObservableCollection<int> LandingTimes { get; set; }




        public DelegateCommand<Flight> TotalsCommand { get; set; }

        public Action<TotalsActionCommand> ShowTotalsAction;
        
    }
}
