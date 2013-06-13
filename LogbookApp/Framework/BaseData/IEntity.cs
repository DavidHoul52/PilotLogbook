using System;
using System.Runtime.Serialization;

namespace BaseData
{

    
    public interface IEntity
    {
        [IgnoreDataMember]
        bool IsNew { get; set; }


     
        

        int id { get; set; }

        DateTime? TimeStamp { get; set; }


    
    }
}
