using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace LogbookApp.Data
{
   
    public class Flight :IEntity
    {
        public Flight()
        {
            Takeoffs = 1;
            LDG = 1;
            Depart=new DateTime(2001, 1, 1);
            Arrival = new DateTime(2001, 1, 1);
            
        }


      
        private Capacity _capacity;
        private Airfield _from;
        private Airfield _to;
        private Aircraft _aircraft;


       
        public int? UserId { get; set; }

        public DateTime Date { get; set; }

    
       

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
                    AirfieldFromId = value.id;

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
                    AirfieldToId = value.id;
            }
        }

        private int _airfieldToId;
        private DateTime _arrival;
        private DateTime? _instrumentFlying;
        private DateTime? _timeStamp;

        public int AirfieldToId
        {
            get
            {   
                return _airfieldToId;}
            set
            {
                _airfieldToId = value; ;
            }
        }
      

        public DateTime Depart { get; set; }
        public DateTime Arrival
        {
            get { return _arrival; }
            set
            {
                _arrival = value;
            }
        }

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
        public Lookups Lookups { get; set; }
     


     

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
                    return "";
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
      

          public string Remarks { get; set; }

          public int? Takeoffs { get; set; }

          public int? LDG { get; set; }

          public bool Night { get; set; }

          public DateTime? InstrumentFlying

          {
              get { return _instrumentFlying; }
              set
              {
                  _instrumentFlying = value;
              }
          }

        public DateTime? SimulatedInstrumentFlying { get; set; }


        public bool IsNew { get; set; }

        public bool Valid()
        {
            return Arrival > Depart ;
        }

        public int id { get; set; }
        public DateTime? TimeStamp
        {
            get { return _timeStamp; }
            set
            {
                _timeStamp = value;
            }
        }


        public void PopulateLookups(Lookups lookupData)
        {
            Aircraft = lookupData.Aircraft.FirstOrDefault(x => x.id == AircraftId);
            Capacity = lookupData.Capacity.FirstOrDefault(x => x.Id == CapacityId);
            From = lookupData.Airfields.FirstOrDefault(x => x.id == AirfieldFromId);
            To = lookupData.Airfields.FirstOrDefault(x => x.id == AirfieldToId);
            
            this.Lookups = lookupData;
           
        }
    }
}
