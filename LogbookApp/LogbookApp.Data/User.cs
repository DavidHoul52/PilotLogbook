using System;
using System.Runtime.Serialization;


namespace LogbookApp.Data
{
    public class User : IEntity
    {
        
        public string DisplayName { get; set; }

          [IgnoreDataMember]
        public bool IsNew { get; set; }
        public bool Valid()
        {
            return true;
        }

        public int id { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
