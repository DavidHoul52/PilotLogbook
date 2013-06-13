using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;

namespace LogbookApp.Data
{
    public interface IOnlineFlightData : IFlightDataService
    {
      

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
        Task UpdateUserFromLocal(User localUser);
    }
}
