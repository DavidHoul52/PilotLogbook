using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Model
{
    public class Flight
    {
        public DateTime Date { get; set; }
        public AcType AcType { get; set; }
        public string Reg { get; set; }
        public string Captain { get; set; }
        public Capacity Capacity { get; set; }
        public Airfield From { get; set; }
        public Airfield To { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Arrival { get; set; }

    }
}
