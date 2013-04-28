using System;
using LogbookApp.Data;

namespace LogbookApp.Commands
{
    public class MaintainActionCommand<T>
    {
        public T Item { get; set; }
        public IFlightDataManager DataService { get; set; }
        public Action OnCompleted { get; set; }
    }
}
