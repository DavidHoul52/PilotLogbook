using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.LocalData
{
    public class LocalDataSyncer
    {
       

        public void Sync(IFlightDataService source, IFlightDataService target)
        {
            target.Flights = source.Flights;
            target.Lookups = source.Lookups;
            target.User = source.User;
        }
    }
}
