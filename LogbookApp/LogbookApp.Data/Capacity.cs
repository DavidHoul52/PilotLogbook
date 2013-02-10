using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{

    public enum CapacityEnum  {P1,P2,Put, Dual, P1S}

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
