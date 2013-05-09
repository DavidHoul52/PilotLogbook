using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IOnlineFlightData : IFlightDataService
    {
        void Update<T>(T item)
          where T : Entity;
    }
}
