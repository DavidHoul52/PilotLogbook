﻿using System;
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
       
        public int Id { get; set; }

        public DateTime Date { get; set; }
       
        [IgnoreDataMember]
        public AcType AcType { get; set; }
        
        public int AcTypeId { get; set; }
        [IgnoreDataMember]
        public string Reg { get; set; }
        public string Captain { get; set; }

        [IgnoreDataMember]
        public Capacity Capacity { get; set; }

        public int CapacityId { get; set; }

        [IgnoreDataMember]
        public Airfield From { get; set; }
        public int AirfieldFromId { get; set; }
        [IgnoreDataMember]
        public Airfield To { get; set; }
        public int AirfieldToId { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Arrival { get; set; }

        public int AircraftId { get; set; }
        [IgnoreDataMember]
        public Lookups Lookups { get; set; }


        
        

   
        //public string FromTo
        //{
        //    get
        //    {
        //        return String.Format("{0} - {1}", From.Name, To.Name);
        //    }
        //}

        //public string DateStr
        //{
        //    get
        //    {
        //        return Date.ToString("dd-MM-yyyy");
        //    }
        //}

        //public string DurationStr
        //{
        //    get
        //    {
        //        if (Duration != default(TimeSpan))
        //            return Duration.ToString(@"hh\:mm");
        //        else
        //            return "Invalid";
        //    }
        //}


        //public TimeSpan Duration
        //{
        //    get
        //    {
        //        if (Arrival >= Depart)
        //            return Arrival - Depart;
        //        else
        //            return default(TimeSpan);
        //    }
        //}



    }
}