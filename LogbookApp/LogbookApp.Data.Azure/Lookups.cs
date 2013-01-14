using System.Collections.Generic;
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


        public IEnumerable<Aircraft> Aircraft { get; set; }

        public async Task Load()
        {
            AcTypes = await MobileService.GetTable<AcType>().ReadAsync();
            Capacity = await MobileService.GetTable<Capacity>().ReadAsync();
            Airfields = await MobileService.GetTable<Airfield>().ReadAsync();
            Aircraft = await MobileService.GetTable<Aircraft>().ReadAsync();
        }


        public IEnumerable<AcType> AcTypes {get; set;}
        public IEnumerable<Capacity> Capacity { get; set; }
        public IEnumerable<Airfield> Airfields {get; set;}

    }
}
