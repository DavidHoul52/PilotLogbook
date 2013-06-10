using System.Collections.Generic;

namespace LogbookApp.Data
{
    public class InMemoryLookups 
    {
        public  List<Capacity> Capacities { get
        {
            return new List<Capacity>
            {
                new Capacity {Id=0, Description = "P1"},
                new Capacity {Id = 1, Description = "P2"},
                new Capacity {Id=2, Description = "Put"},
                new Capacity {Id=3, Description = "Dual"},
                new Capacity {Id=4, Description = "P1/S"}
            };

        }}
    }
}
