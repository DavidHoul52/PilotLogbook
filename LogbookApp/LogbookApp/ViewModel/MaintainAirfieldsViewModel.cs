using System;
using System.Collections.ObjectModel;
using System.Linq;
using LogbookApp.Commands;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class MaintainAirfieldsViewModel  : MaintainViewModelBase<Airfield>
    {
        public MaintainAirfieldsViewModel(IFlightDataManager flightDataManager)
            : base(flightDataManager)
        {
        
        }


        protected override void Delete(Airfield item)
        {
            FlightDataManager.DeleteAirfield(item, DateTime.Now);
        }

        protected override void Add()
        {
            Items.Add(new Airfield
            {
                
                IsNew = true

            });
            Selected = Items.Last();
            ShowDetail(new MaintainActionCommand<Airfield> { Item = Selected, DataService = FlightDataManager });
        }

   

     
        public async override void Load()
        {
            await FlightDataManager.GetLookups();
            Items = new ObservableCollection<Airfield>(FlightData.Lookups.Airfields.OrderBy(x => x.Name));

            RaisePropertyChanged(() => Items);
        }

        
    }
}
