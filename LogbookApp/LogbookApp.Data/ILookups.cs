using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public interface ILookups
    {
       IEnumerable<AcType> AcTypes { get; set; }
       IEnumerable<Capacity> Capacity { get; set; }
       IEnumerable<Airfield> Airfields { get; set; }
       IEnumerable<Aircraft> Aircaft { get; set; }

        Task Load();
    }
}