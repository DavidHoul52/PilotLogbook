using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LogbookApp.Data
{

    public enum CapacityEnum
    {
        [EnumMember]
        P1,
        [EnumMember]
        P2,
        [EnumMember]
        Put,
        [EnumMember]
        Dual,
        [EnumMember]
        P1S
    }

    [DataContract]
    public class Capacity
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public CapacityEnum CapacityEnum
        {
            get
            {
                return (CapacityEnum)Id;
            }
        }

    
      public  bool InCommand { get
      
         { return CapacityEnum==CapacityEnum.P1 || CapacityEnum == CapacityEnum.P1S ;}
      }}
}
