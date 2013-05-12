using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface IOnlineFlightData : IFlightDataService
    {
        Task Update<T>(T item)
          where T : IEntity;

        Task Insert<T>(T item)
            where T : IEntity;

        Task Delete<T>(T item)
              where T : IEntity;
    }
}
