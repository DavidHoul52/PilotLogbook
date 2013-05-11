using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Commands
{
    public class TotalsActionCommand
    {
        public ObservableCollection<Flight> Flights { get; set; }
        public DateTime ToDate { get; set; }

        public DateTime FromDate { get; set; }
    }
}
