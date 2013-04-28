using System;
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
            get { return acType; }

            set
            {
                acType = value;
                RaisePropertyChanged(() => AcType);
            }
        }



        public override async Task Save()
        {


            if (AcType.IsNew && IsDuplicate())
            {
                await Messager.ShowMessage("This Aircraft Type has already been added.");
                return;
            }


            if (AcType.IsNew)
            {
                await DataService.InsertAcType(AcType, DateTime.UtcNow);
                if (Flight != null)
                    Flight.Lookups.AcTypes.Add(AcType);
            }
            else
            {
                await DataService.UpdateAcType(AcType, DateTime.UtcNow);
            }

        }

        private bool IsDuplicate()
        {
            return Lookups.AcTypes.FirstOrDefault(x => x.Code == AcType.Code && AcType != x) != null;
        }
    }
}   