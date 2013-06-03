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


        public MaintainAircraftViewModel(IFlightDataManager flightDataService)
            : base(flightDataService)
        {
        
        }


        protected async override void Delete(Aircraft item)
        {
            await FlightDataManager.DeleteAircraft(item, DateTime.Now);
            Load();
        }

        protected override void Add()
        {
            Items.Add(new Aircraft
            {
                AircraftClass = AircraftClass.SEP,
                IsNew = true
                
            });
            Selected = Items.Last();
            ShowDetail(new MaintainActionCommand<Aircraft> { Item = Selected, DataService = FlightDataManager });

        }

        public async override void Load()
        {

            await FlightDataManager.GetLookups();
            Refresh();
                
         

        }

        public void Refresh()
        {
            Items = new ObservableCollection<Aircraft>(FlightData.Lookups.Aircraft.OrderBy(x => x.Reg));
            RaisePropertyChanged(() => Items);
        }
    }
}
