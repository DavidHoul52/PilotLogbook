using System.Linq;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class AircraftTypeViewModel : LookupViewModelBase
    {

        private AcType acType;
        public AcType AcType
        {
            get

            { return acType; }

            set
            {
                acType = value;
                RaisePropertyChanged(() => AcType);
            }
        }



        public async override Task Save()
        {
            

            //Flight.AcTypeId =
            //    Flight.DataService.Lookups.AcTypes.Where(x => x.Code == Flight.AcType.Code).First().Id;

            if (AcType.IsNew)
            {
                await DataService.InsertAcType(AcType);
                if (Flight != null)
                    Flight.Lookups.AcTypes.Add(AcType);
            }
            else
            {
                await DataService.UpdateAcType(AcType);
            }
         
        }

      
    }
}