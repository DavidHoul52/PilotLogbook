using System;

namespace OnlineOfflineSyncLibrary.Test.SyncManagerTests
{
    public class TestUser : IUser
    {
        public bool IsNew { get; set; }
        public int id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string DisplayName { get; set; }
    }
}