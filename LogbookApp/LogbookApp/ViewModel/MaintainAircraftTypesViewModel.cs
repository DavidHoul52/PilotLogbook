using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class MaintainAircraftTypesViewModel : MaintainViewModelBase<AcType>
    {

        public MaintainAircraftTypesViewModel(IFlightDataManager flightDataService) :base (flightDataService)
        {
           
        }


        protected override void Delete(AcType item)
        {
            FlightDataManager.DeleteAcType(item, DateTime.UtcNow);
        }

        public async override void Load()
        {
            await FlightDataManager.GetLookups();
            Items = new ObservableCollection<AcType>(FlightData.Lookups.AcTypes.OrderBy(x => x.Code));
            RaisePropertyChanged(() => Items);
        }
    }
}
