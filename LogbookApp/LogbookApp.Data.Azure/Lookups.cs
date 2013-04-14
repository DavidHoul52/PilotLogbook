
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Linq;

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
            var aircraft = await
                MobileService.GetTable<Aircraft>()
                    .Where(x => x.UserId == userId)
                    .Take(500)
                    .OrderBy(x => x.Reg)
                    .ToListAsync();

             Aircraft = new ObservableCollection<Aircraft>(aircraft.
                Select(ac =>
                {
                    ac.AcType = AcTypes.FirstOrDefault(x => x.Id == ac.AcTypeId);
                        return ac;
                }));
             Airfields = new ObservableCollection<Airfield>(await MobileService.GetTable<Airfield>().Where(x=>x.UserId==userId).Take(500).OrderBy(x=>x.Name).
                ToListAsync());
   


        }

   
       


        public ObservableCollection<AcType> AcTypes {get; set;}
        public ObservableCollection<Capacity> Capacity { get; set; }
        public ObservableCollection<Airfield> Airfields { get; set; }
        public ObservableCollection<Aircraft> Aircraft { get; set; }

    }
}
