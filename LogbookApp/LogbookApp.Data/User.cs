using System;


namespace LogbookApp.Data
{
    public class User
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
