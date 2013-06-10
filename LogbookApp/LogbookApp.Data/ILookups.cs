using System.Collections.ObjectModel;

namespace LogbookApp.Data
{
    
    public interface ILookups
    {
       ObservableCollection<AcType> AcTypes { get; set; }
       
       ObservableCollection<Airfield> Airfields { get; set; }
       ObservableCollection<Aircraft> Aircraft { get; set; }

 
    }
}