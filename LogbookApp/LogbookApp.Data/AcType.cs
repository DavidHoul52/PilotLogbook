using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BaseData;

namespace LogbookApp.Data
{
    public class AcType : IEntity
    {

      
        public string Code { get; set; }
          [IgnoreDataMember]
        public bool IsNew { get; set; }
        public bool Valid()
        {
            return true;
        }

        public int id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
    }
}
