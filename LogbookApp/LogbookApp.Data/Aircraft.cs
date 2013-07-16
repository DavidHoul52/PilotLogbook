using System;
using System.Linq;
using System.Runtime.Serialization;
using BaseData;


namespace LogbookApp.Data
{
  
   
    public class Aircraft : IEntity 
    {
     

        public Aircraft()
        {
            AircraftClass = AircraftClass.Items.First();
        }


        [IgnoreDataMember]
        public AircraftClass AircraftClass
        {
            get { return _aircraftClass; }
            set
            {
                _aircraftClass = value;
                acClassId = _aircraftClass.Id;
            }
        }


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
                    
                    AircraftClass = AircraftClass.Items.FirstOrDefault(x => x.Id == value.Value);
                    
                }
                acClassId = value;
            }
        }

        public int AcTypeId { get; set; }

        public string Reg { get; set; }
        public DateTime? TimeStamp { get; set; }

        public int UserId { get; set; }
        public int id { get; set; }




        

        private AcType _acType;
        private AircraftClass _aircraftClass;

        [IgnoreDataMember]
        public AcType AcType
        {
            get { return _acType; }
            set
            {
                _acType = value;
                if (_acType != null)
                    AcTypeId = AcType.id;
            }
        }

       

        [IgnoreDataMember]
        public bool IsNew { get; set; }

        public bool Valid()
        {
            return (AircraftClass != null && AcType != null);
        }
    }
}
