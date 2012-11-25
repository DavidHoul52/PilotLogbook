using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Services
{
    public class Lookups
    {
        public List<string> AcTypes
        {
            get
            {
                return new List<string> { "C-152", "C-172", "P28", "P38" };
            }

        }

        public List<string> Capacity
        {

            get
            {
                return new List<string> { "P1", "P2", "Put" };
            }

        }




    }
}
