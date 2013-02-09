using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightTimes
{
    public class FlightCalcResult
    {
        public FlightCalcResult()
        {
            GrandTotal = new TimeSpan(0,0,0);
            SEPpic = new TimeSpan(0, 0, 0);
            MEpic = new TimeSpan(0, 0, 0);
            MEp2Dual = new TimeSpan(0, 0, 0);
            
        }


        public TimeSpan GrandTotal { get; set; }

        public TimeSpan SEPpic { get; set; }

        public TimeSpan SEPp2Dual { get; set; }

        public TimeSpan MEpic { get; set; }

        public TimeSpan MEp2Dual { get; set; }
    }
}
