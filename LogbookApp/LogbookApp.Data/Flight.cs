using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


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
            get { return  AcType.Id; }
            set {  AcType.Id = value; }
        }

        [IgnoreDataMember]
        public string Reg { get; set; }
        public string Captain { get; set; }

        [IgnoreDataMember]
        public Capacity Capacity { get; set; }

        public int CapacityId
        {
            get { return Capacity.Id; }
            set { Capacity.Id = value; }
        }

        [IgnoreDataMember]
        public Airfield From { get; set; }

        public int AirfieldFromId
        {
            get { return From.Id; }
            set { From.Id = value; }
        }

        [IgnoreDataMember]
        public Airfield To { get; set; }

        public int AirfieldToId
        {
            get { return To.Id; }
            set { To.Id = value; }
        }

        public DateTime Depart { get; set; }
        public DateTime Arrival { get; set; }
        
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
        
    }
}
