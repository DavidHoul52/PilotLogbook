using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace LogbookApp.Data
{
  

    public class Aircraft : Entity 
    {
        public int id { get; set; }
        public string Reg { get; set; }

        public Aircraft()
        {
            AircraftClass = AircraftClass.Items.First();
        }


        [IgnoreDataMember]
        public AircraftClass AircraftClass { get; set; }

      
        
        public int? AcClassId 
        {
            get
            {
                if (AircraftClass!=null)
                  return AircraftClass.Id;
                return null;
            }
            set
            {
                if (value!=null)
                {
                    
                    AircraftClass = AircraftClass.Items.Where(x => x.Id == value.Value).FirstOrDefault();
                }
            }
        }


      
       
    }
}
