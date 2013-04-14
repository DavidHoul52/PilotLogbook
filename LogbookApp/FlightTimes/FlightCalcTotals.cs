using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightTimes
{
    public class FlightCalcTotals
    {
        public FlightCalcTotals()
        {
            
            
        }


        public FlightCalcResult Total { get; set;}
        public FlightCalcResult Night { get; set; }
        public FlightCalcResult Day { get; set; }

        public FlightCalcResult Currency90Days { get; set; }
        public FlightCalcResult Currency28Days { get; set; }
        
    }
}
