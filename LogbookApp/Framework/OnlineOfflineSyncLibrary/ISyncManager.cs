using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary
{
    public interface ISyncManager<TSyncableData>
        where TSyncableData: ISyncableData
    {
        Task UpdateOnlineData(TSyncableData sourceFlightData, DateTime now);
      
    }
}