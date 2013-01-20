
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
            Airfields = new ObservableCollection<Airfield>(await MobileService.GetTable<Airfield>().ReadAsync());
            Aircraft =  new ObservableCollection<Aircraft>(await MobileService.GetTable<Aircraft>().ReadAsync());
        }


        public ObservableCollection<AcType> AcTypes {get; set;}
        public ObservableCollection<Capacity> Capacity { get; set; }
        public ObservableCollection<Airfield> Airfields { get; set; }
        public ObservableCollection<Aircraft> Aircraft { get; set; }

    }
}
