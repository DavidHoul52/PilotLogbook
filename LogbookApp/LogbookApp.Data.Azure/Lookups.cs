
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace LogbookApp.Data
{
    public class Lookups : ILookups
    {
        private Microsoft.WindowsAzure.MobileServices.MobileServiceClient MobileService;

        public Lookups(Microsoft.WindowsAzure.MobileServices.MobileServiceClient mobileService)
        {
            
            this.MobileService = mobileService;
            
        }


      

        public async Task Load(int userId)
        {
            AcTypes = new ObservableCollection<AcType>(await MobileService.GetTable<AcType>().Take(500).OrderBy(x=>x.Code).ToListAsync());
            Capacity = new ObservableCollection<Capacity>(await MobileService.GetTable<Capacity>().Take(500).OrderBy(x=>x.Description).ToListAsync());
            Airfields = new ObservableCollection<Airfield>(await MobileService.GetTable<Airfield>().Where(x=>x.UserId==userId).Take(500).OrderBy(x=>x.Name).
                ToListAsync());
            Aircraft =  new ObservableCollection<Aircraft>(await MobileService.GetTable<Aircraft>().Where(x=>x.UserId==userId).Take(500).OrderBy(x=>x.Reg).ToListAsync());
            foreach (var ac in Aircraft)
            {
                ac.AcType = AcTypes.Where(a => a.Id == ac.AcTypeId).FirstOrDefault();
                
            }
        }


        public ObservableCollection<AcType> AcTypes {get; set;}
        public ObservableCollection<Capacity> Capacity { get; set; }
        public ObservableCollection<Airfield> Airfields { get; set; }
        public ObservableCollection<Aircraft> Aircraft { get; set; }

    }
}
