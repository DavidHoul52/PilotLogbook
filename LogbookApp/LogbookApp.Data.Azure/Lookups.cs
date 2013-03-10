
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class Lookups : ILookups
    {
        private Microsoft.WindowsAzure.MobileServices.MobileServiceClient MobileService;

        public Lookups(Microsoft.WindowsAzure.MobileServices.MobileServiceClient mobileService)
        {
            
            this.MobileService = mobileService;
            
        }


      

        public async Task Load()
        {
            AcTypes = new ObservableCollection<AcType>(await MobileService.GetTable<AcType>().ReadAsync());
            Capacity = new ObservableCollection<Capacity>(await MobileService.GetTable<Capacity>().ReadAsync());
            Airfields = new ObservableCollection<Airfield>(await MobileService.GetTable<Airfield>().Take(500).OrderBy(x=>x.Name).
                ToListAsync());
            Aircraft =  new ObservableCollection<Aircraft>(await MobileService.GetTable<Aircraft>().Take(500).OrderBy(x=>x.Reg).ToListAsync());
        }


        public ObservableCollection<AcType> AcTypes {get; set;}
        public ObservableCollection<Capacity> Capacity { get; set; }
        public ObservableCollection<Airfield> Airfields { get; set; }
        public ObservableCollection<Aircraft> Aircraft { get; set; }

    }
}
