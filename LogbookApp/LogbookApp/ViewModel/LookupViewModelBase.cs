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
                if (value!=null)
                  OnFlightUpdated();
                RaisePropertyChanged(()=>Flight);
            }
        }

        protected virtual void OnFlightUpdated()
        {
            DataService = Flight.DataService;
        }

        public abstract Task Save();

        public IFlightDataService DataService { get; set; }
    }
}
