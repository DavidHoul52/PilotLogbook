using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace LogbookApp.Data
{

    
    public interface IEntity
    {
        [IgnoreDataMember]
        bool IsNew { get; set; }


     
        

        int id { get; set; }

        DateTime? TimeStamp { get; set; }


    
    }
}
