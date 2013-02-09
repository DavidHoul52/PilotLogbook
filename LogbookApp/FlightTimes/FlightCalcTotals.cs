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
            Total = new FlightCalcResult();
        }


        public FlightCalcResult Total { get; set;}
    }
}
