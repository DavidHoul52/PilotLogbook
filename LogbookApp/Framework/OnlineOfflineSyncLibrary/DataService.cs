using System;
using System.Threading.Tasks;

namespace OnlineOfflineSyncLibrary
{
    public class DataService<TSyncableData>
    {
        public virtual async Task<TSyncableData> LoadUserData(string userName)
        {
            Loaded = true;
            return default(TSyncableData);
        }


        public virtual async Task<TSyncableData> CreateUserData(string userName, DateTime? timeStamp)
         {
             Loaded = true;
             return default(TSyncableData);
         }

        public bool Loaded { get; private set; }
    }
}