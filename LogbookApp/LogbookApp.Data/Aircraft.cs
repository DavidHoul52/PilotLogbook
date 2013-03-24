using System.Linq;
using System.Runtime.Serialization;


namespace LogbookApp.Data
{
  

    public class Aircraft : Entity 
    {
        public int id { get; set; }
        public string Reg { get; set; }

        public int UserId { get; set; }

        public Aircraft()
        {
            AircraftClass = AircraftClass.Items.First();
        }


        [IgnoreDataMember]
        public AircraftClass AircraftClass { get; set; }



        private int? acClassId;
        public int? AcClassId 
        {
            get
            {
                return acClassId;
                
                
            }
            set
            {
                if (value!=null)
                {
                    
                    AircraftClass = AircraftClass.Items.Where(x => x.Id == value.Value).FirstOrDefault();
                    
                }
                acClassId = value;
            }
        }


        private AcType _acType;
        [IgnoreDataMember]
        public AcType AcType
        {
            get { return _acType; }
            set
            {
                _acType = value;
                if (_acType != null)
                    AcTypeId = AcType.Id;
            }
        }

        public int AcTypeId { get; set; }

      
       
    }
}
