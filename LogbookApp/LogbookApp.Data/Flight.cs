using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace LogbookApp.Data
{
   
    public class Flight 
    {
        
        public DateTime Date { get; set; }
       
        [IgnoreDataMember]
        public AcType AcType { get; set; }
        
        public int AcTypeId { get; set; }
        public string Reg { get; set; }
        public string Captain { get; set; }
       
        //public Capacity Capacity { get; set; }
       
        //public Airfield From { get; set; }
        //       public Airfield To { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Arrival { get; set; }


        
        public int Id { get; set; }

   
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
