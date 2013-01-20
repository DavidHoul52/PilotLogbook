using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface ILookups
    {
       ObservableCollection<AcType> AcTypes { get; set; }
       ObservableCollection<Capacity> Capacity { get; set; }
       ObservableCollection<Airfield> Airfields { get; set; }
       ObservableCollection<Aircraft> Aircraft { get; set; }

        Task Load();
    }
}