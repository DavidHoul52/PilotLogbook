using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public enum AcClass { SEP, ME };

    public class Aircraft
    {
        public int id { get; set; }
        public string Reg { get; set; }
        public AcClass AcClass { get; set; }
        
       
    }
}
