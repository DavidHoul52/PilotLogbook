using LogbookApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.ViewModel
{
    public class FlightDetailPageViewModel : ViewModelBase
    {
        public FlightDetailPageViewModel()
        {
         
        }


        public Lookups Lookups { get; set; }

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
                Lookups = _flight.Lookups;
                RaisePropertyChanged(() => Lookups);
            
            }
            }

        

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

        

       
        
        
        
    }
}
