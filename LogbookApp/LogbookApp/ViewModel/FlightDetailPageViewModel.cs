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
    }
}
