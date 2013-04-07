using System.Collections.ObjectModel;
using System.Linq;
using LogbookApp.Commands;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class MaintainAirfieldsViewModel  : MaintainViewModelBase<Airfield>
    {
        public MaintainAirfieldsViewModel(IFlightDataService flightDataService) : base(flightDataService)
        {
        
        }


        protected override void Add()
        {
            Items.Add(new Airfield
            {
                
                IsNew = true

            });
            Selected = Items.Last();
            ShowDetail(new MaintainActionCommand<Airfield> { Item = Selected, DataService = flightDataService });
        }

   

     
        public async override void Load()
        {
            await flightDataService.GetLookups();
            Items = new ObservableCollection<Airfield>(flightDataService.Lookups.Airfields.OrderBy(x => x.Name));

            RaisePropertyChanged(() => Items);
        }

        
    }
}
