using System;


namespace LogbookApp.Data
{
    public class User : Entity
    {
        
        public string DisplayName { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
