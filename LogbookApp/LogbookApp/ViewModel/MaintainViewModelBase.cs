using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseData;
using LogbookApp.Commands;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public abstract class MaintainViewModelBase<T> : ViewModelBase
        where T: IEntity, new()
    {
        protected IFlightDataManager FlightDataManager;

        protected FlightData FlightData;

        public DelegateCommand<T> EditCommand { get; set; }

        public DelegateCommand<T> DeleteCommand { get; set; }

        public DelegateCommand<T> AddCommand { get; set; }

        public ObservableCollection<T> Items { get; set; }

        public MaintainViewModelBase(IFlightDataManager flightDataService)
        {
            this.FlightDataManager = flightDataService;
            this.FlightData = flightDataService.Data;
            EditCommand = new DelegateCommand<T>((f) => ShowDetail(new MaintainActionCommand<T>
            {
                Item = f,
                DataService = flightDataService
            }),
                (f) => { return f != null; });
            RaisePropertyChanged(() => EditCommand);
            DeleteCommand = new DelegateCommand<T>((f) => Delete(f), (f) => { return f != null; });
            RaisePropertyChanged(() => DeleteCommand);
            AddCommand = new DelegateCommand<T>((f) => Add(), (f) => { return true; });
            RaisePropertyChanged(() => AddCommand);
        }

        protected abstract void Delete(T item);
       

        protected virtual void Add()
        {
            Items.Add(new T
            {

                IsNew = true

            });
            Selected = Items.Last();
            ShowDetail(new MaintainActionCommand<T> { Item = Selected, DataService = FlightDataManager });
        }

       


      

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
