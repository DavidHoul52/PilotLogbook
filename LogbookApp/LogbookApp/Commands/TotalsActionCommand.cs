using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Commands
{
    public class TotalsActionCommand
    {
        public List<Flight> Flights { get; set; }
        public DateTime ToDate { get; set; }

        public DateTime FromDate { get; set; }
    }
}
