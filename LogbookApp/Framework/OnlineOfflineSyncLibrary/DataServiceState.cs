using System;

namespace OnlineOfflineSyncLibrary
{
    public class DataServiceState
    {
        public bool UserDataExists { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}