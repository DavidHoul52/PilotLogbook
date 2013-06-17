using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;

namespace OnlineOfflineSyncLibrary2.Stubs
{
    public class TestEntity : IEntity
    {
        public bool IsNew { get; set; }
        public int id { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
