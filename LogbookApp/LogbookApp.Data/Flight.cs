using System;
using System.Runtime.Serialization;


namespace LogbookApp.Data
{
   
    public class Flight 
    {
        

        public int id { get; set; }

        public DateTime Date { get; set; }
       
        [IgnoreDataMember]
        public AcType AcType { get; set; }
        
        public int AcTypeId
        {
            get { if (AcType != null) return  AcType.Id;
                return 0;

            }
            set { if (AcType != null) AcType.Id = value; }
        }

        [IgnoreDataMember]
        public string Reg { get; set; }
        public string Captain { get; set; }

        [IgnoreDataMember]
        public Capacity Capacity { get; set; }

        public int CapacityId
        {
            get { if (Capacity != null) return Capacity.Id;
            return 0;
            }
            set { if (Capacity != null) Capacity.Id = value; }
        }

        [IgnoreDataMember]
        public Airfield From { get; set; }

        public int AirfieldFromId
        {
            get { if (From != null) return From.Id;
            return 0;
            }
            set { if (From != null) From.Id = value; }
        }

        [IgnoreDataMember]
        public Airfield To { get; set; }

        public int AirfieldToId
        {
            get { if (To != null) return To.Id;
            return 0;
            }
            set { if (To != null) To.Id = value; }
        }

        public DateTime Depart { get; set; }
        public DateTime Arrival { get; set; }
       
        [IgnoreDataMember]
        public Aircraft Aircraft { get; set; }

        public int AircraftId 
        {
            get { if (Aircraft != null) return Aircraft.id;
                return 0; 
            }
            set { if (Aircraft != null) Aircraft.id = value; }
        }

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
        
    }
}
