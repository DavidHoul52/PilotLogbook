using System;
using System.Runtime.Serialization;

namespace LogbookApp.Data
{
 
    public class Airfield :IEntity
    {
        public string ICAOCode { get; set; }
   
        public string Name { get; set; }
       
        public int UserId { get; set; }

        public int id { get; set; }
        

        [IgnoreDataMember]
        public bool IsNew { get; set; }
        public DateTime? TimeStamp { get; set; }
        public bool Valid()
        {
            return !string.IsNullOrEmpty(Name);
        }

        
    }
}
