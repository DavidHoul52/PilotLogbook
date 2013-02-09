using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public enum AcClass { SEP, ME };

    public class Aircraft
    {
        public int id { get; set; }
        public string Reg { get; set; }
       
        [IgnoreDataMember]
        public AcClass AcClass { get; set; }


        
        public int? AcClassId
        {
            get
            {
                return (int)AcClass;
            }
            set
            {
                if (value!=null)
                  AcClass = (AcClass)value.Value;
            }
        }
        
       
    }
}
