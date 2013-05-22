using System;
using System.Linq;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class AircraftTypeViewModel : LookupViewModelBase
    {

        public AircraftTypeViewModel()
        {
          
        }

        private Aircraft _aircraft;
        public Aircraft Aircraft
        {
            get { return _aircraft; }
            set
            {
                _aircraft = value;
                if (value != null) AcType = value.AcType;
                RaisePropertyChanged(() => Aircraft);
            }
        }
      

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


            if (AcType.Valid())
            {
                if (AcType.IsNew)
                {
                    if (IsDuplicate())
                    {
                        await Messager.ShowMessage("This aircraft type has already been added.");
                        return;
                    }
                    await DataService.InsertAcType(AcType, DateTime.Now);
                    if (Aircraft != null)
                    {
                        Lookups.AcTypes.Add(AcType);
                        Aircraft.AcType = AcType;
                    }
                }
                else
                {
                    await DataService.UpdateAcType(AcType, DateTime.Now);
                }
            }


          
        }

        private bool IsDuplicate()
        {
            return Lookups.AcTypes.FirstOrDefault(x => x.Code == AcType.Code && AcType != x) != null;
        }
    }
}   