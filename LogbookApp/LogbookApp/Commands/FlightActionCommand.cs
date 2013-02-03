using System;
using LogbookApp.Data;

namespace LogbookApp.Commands
{
    public class FlightActionCommand
    {
        public Flight Flight { get; set; }
        public Action<Flight> OnCompleted { get; set; }
    }
}
