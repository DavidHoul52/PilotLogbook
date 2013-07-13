using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseData;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.Data
{
    public interface IOnlineFlightDataService :  IOnlineDataService<FlightData, User>,
        IFlightDataService
    {
      

       

      
    }
}
