using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
            Airfields.Add(new Airfield
            {
                
                IsNew = true

            });
            Selected = Airfields.Last();
            ShowDetail(new MaintainActionCommand<Airfield> { Item = Selected, DataService = flightDataService });
        }

        protected async override void Delete(Airfield f)
        {
            bool deleted = await flightDataService.DeleteAirfield(f);
            if (deleted)
                Airfields.Remove(f);
        }

        public async override void Load()
        {
            await flightDataService.GetLookups();
            Airfields = new ObservableCollection<Airfield>(flightDataService.Lookups.Airfields.OrderBy(x => x.Name));

            RaisePropertyChanged(() => Airfields);
        }

        public ObservableCollection<Airfield> Airfields { get; set; }
    }
}
