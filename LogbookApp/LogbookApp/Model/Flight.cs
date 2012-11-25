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
        public string AcType { get; set; }
        public string Reg { get; set; }
        public string Captain { get; set; }
        public string Capacity { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Arrival { get; set; }

    }
}
