using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class Lookups : ILookups
    {
        public ObservableCollection<AcType> AcTypes { get; set; }
        public ObservableCollection<Capacity> Capacity { get; set; }
        public ObservableCollection<Airfield> Airfields { get; set; }
        public ObservableCollection<Aircraft> Aircraft { get; set; }
        
      
    }
}