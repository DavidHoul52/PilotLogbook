using LogbookApp.Data;

namespace LogbookApp.Views
{
    public class AircraftActionCommand
    {
        public Aircraft Aircraft { get; set; }
        
        public IFlightDataManager DataService { get; set; }
    }
}