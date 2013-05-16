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

        Task InsertFlight(Flight flight);
        Task DeleteFlight(Flight flight);
        Task SaveFlight(Flight flight);
        Task InsertAircraft(Aircraft aircraft);
        Task InsertAircraftType(AcType acType);
        Task InsertAirfield(Airfield from);
        Task UpdateAircraft(Aircraft aircraft);
        Task DeleteAircraft(Aircraft f);
        Task UpdateAirfield(Airfield airfield);
        Task DeleteAirfield(Airfield f);
        Task UpdateAcType(AcType acType);
        Task InsertAcType(AcType acType);

        Task DeleteAcType(AcType acType);
    }
}
