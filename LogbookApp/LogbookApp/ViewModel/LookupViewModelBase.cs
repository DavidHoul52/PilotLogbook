using System.Threading.Tasks;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public abstract class LookupViewModelBase : ViewModelBase
    {
        private Flight _flight;
        public Flight Flight
        {
            get { return _flight; }
            set
            {
                _flight = value;
                RaisePropertyChanged(()=>Flight);
            }
        }

        public abstract Task Save();
    }
}
