using System;
using System.Linq;
using System.Runtime.Serialization;


namespace LogbookApp.Data
{
   
    public class Flight 
    {
        public Flight()
        {
            Takeoffs = 1;
            LDG = 1;
            
        }


        private AcType _acType;
        private Capacity _capacity;
        private Airfield _from;
        private Airfield _to;
        private Aircraft _aircraft;


        public int id { get; set; }

        public DateTime Date { get; set; }
       
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
       

        [IgnoreDataMember]
        public string Reg { get; set; }
        public string Captain { get; set; }

        [IgnoreDataMember]
        public Capacity Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value;
                if (_capacity != null)
                    CapacityId = value.Id;
            }
        }

        public int CapacityId { get; set; }
     
        [IgnoreDataMember]
        public Airfield From
        {
            get { return _from; }
            set
            {
                _from = value;
                if (value != null)
                    AirfieldFromId = value.Id;

            }
        }

        public int AirfieldFromId { get; set; }
       

        [IgnoreDataMember]
        public Airfield To
        {
            get { return _to; }
            set
            {
                _to = value;
                if (value != null)
                    AirfieldToId = value.Id;
            }
        }

        public int AirfieldToId { get; set; }
      

        public DateTime Depart { get; set; }
        public DateTime Arrival { get; set; }
       
        [IgnoreDataMember]
        public Aircraft Aircraft
        {
            get { return _aircraft; }
            set
            {
                _aircraft = value;
                if (value != null)
                    AircraftId = value.id;
            }
        }

        public int AircraftId { get; set; }
      

        [IgnoreDataMember]
        public ILookups Lookups { get; set; }
          [IgnoreDataMember]
        public IFlightDataService DataService { get; set; }


        public void Save()
        {
            
            if (DataService != null) DataService.SaveFlight(this);
        }

          [IgnoreDataMember]
        public string FromTo
        {
            get { if (From != null && To != null) return String.Format("{0} - {1}", From.Name, To.Name);
            else
            {
                return "";
            }
            

            }
        }
          [IgnoreDataMember]
        public string DateStr
        {
            get
            {
                return Date.ToString("dd-MM-yyyy");
            }
        }
          [IgnoreDataMember]
        public string DurationStr
        {
            get
            {
                if (Duration != default(TimeSpan))
                    return Duration.ToString(@"hh\:mm");
                else
                    return "Invalid";
            }
        }

          [IgnoreDataMember]
        public TimeSpan Duration
        {
            get
            {
                if (Arrival >= Depart)
                    return Arrival - Depart;
                else
                    return default(TimeSpan);
            }
        }
          [IgnoreDataMember]
        public bool IsNew { get; set; }

          public string Remarks { get; set; }

          public int? Takeoffs { get; set; }

          public int? LDG { get; set; }

          public bool? Night { get; set; }

          public DateTime? InstrumentFlying { get; set; }

          public DateTime? SimulatedInstrumentFlying { get; set; }
        
    }
}
