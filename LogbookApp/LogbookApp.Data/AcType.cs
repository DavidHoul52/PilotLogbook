using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class AcType : IEntity
    {

      
        public string Code { get; set; }
        public bool IsNew { get; set; }
        public bool Valid()
        {
            return true;
        }

        public int id { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
