using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class Lookups : ILookups
    {
        public Lookups()
        {
            AcTypes = new ObservableCollection<AcType>();
            Capacity = new ObservableCollection<Capacity>();
            Airfields = new ObservableCollection<Airfield>();
            Aircraft = new ObservableCollection<Aircraft>();
        }

        public ObservableCollection<AcType> AcTypes { get; set; }
        public ObservableCollection<Capacity> Capacity { get; set; }
        public ObservableCollection<Airfield> Airfields { get; set; }
        public ObservableCollection<Aircraft> Aircraft { get; set; }
        
      
    }
}