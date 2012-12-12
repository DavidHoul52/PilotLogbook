using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class Capacity
    {
          [SQLite.PrimaryKey]
        public string Code { get; set; }
    }
}
