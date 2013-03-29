using LogbookApp.Data;

namespace LogbookApp.Commands
{
    public class MaintainActionCommand<T>
    {
        public T Item { get; set; }
        public IFlightDataService DataService { get; set; }
    }
}
