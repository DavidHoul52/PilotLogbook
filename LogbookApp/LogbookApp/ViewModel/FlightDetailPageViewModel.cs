using LogbookApp.Model;
using LogbookApp.Services;
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
            Lookups = new Lookups();
            RaisePropertyChanged(()=>Lookups);
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
            
            }
            }

        

        public DateTime Depart
        {
            get { return Flight.Depart; }
            set {
                   Flight.Depart = value;
                   RaisePropertyChanged(() => Flight);
               }
        }


        public DateTime Arrival
        {
            get { return Flight.Arrival; }
            set
            {
                Flight.Arrival = value;
                RaisePropertyChanged(() => Flight);
            }
        }

        

       
        
        
        
    }
}
