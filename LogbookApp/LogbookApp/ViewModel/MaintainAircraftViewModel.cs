using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Commands;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class MaintainAircraftViewModel : MaintainViewModelBase<Aircraft>
    {
    

        public MaintainAircraftViewModel(IFlightDataService flightDataService) : base(flightDataService)
        {
        
        }


        protected override void Add()
        {
            Items.Add(new Aircraft
            {
                AircraftClass = AircraftClass.SEP,
                IsNew = true
                
            });
            Selected = Items.Last();
            ShowDetail(new MaintainActionCommand<Aircraft> { Item = Selected, DataService = flightDataService });

        }

        public async override void Load()
        {

             await flightDataService.GetLookups();
            Refresh();
                
         

        }

        public void Refresh()
        {
            Items = new ObservableCollection<Aircraft>(flightDataService.Lookups.Aircraft.OrderBy(x => x.Reg));
            RaisePropertyChanged(() => Items);
        }
    }
}
