using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Commands;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public abstract class MaintainViewModelBase<T> : ViewModelBase
    {
        protected IFlightDataService flightDataService;


        public DelegateCommand<T> EditCommand { get; set; }

        public DelegateCommand<T> DeleteCommand { get; set; }

        public DelegateCommand<T> AddCommand { get; set; }


        protected abstract void Add();

        protected abstract void Delete(T f);

        public abstract void Load();

        public Action<MaintainActionCommand<T>> ShowDetail { get; set; }


        private T selected;
        public T Selected
        {
            get
            { return selected; ;}

            set
            {
                selected = value;
                RaisePropertyChanged(() => Selected);

            }
        }
        
    }
}
