using System.Runtime.Serialization;

namespace LogbookApp.Data
{
    public class Entity
    {
        [IgnoreDataMember]
        public bool IsNew { get; set; }

        
        public virtual bool Valid()
        {
            return true;
        }

    
    }
}
