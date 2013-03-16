using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LogbookApp.Data
{
    public class Entity
    {
        [IgnoreDataMember]
        public bool IsNew { get; set; }
    }
}
