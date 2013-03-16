using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.Commands
{
    public class MaintainActionCommand<T>
    {
        public T Item { get; set; }
        public IFlightDataService DataService { get; set; }
    }
}
