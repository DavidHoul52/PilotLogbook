using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Commands;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public abstract class MaintainViewModelBase<T> : ViewModelBase
        where T: Entity, new()
    {
        protected IFlightDataService flightDataService;


        public DelegateCommand<T> EditCommand { get; set; }

        public DelegateCommand<T> DeleteCommand { get; set; }

        public DelegateCommand<T> AddCommand { get; set; }

        public ObservableCollection<T> Items { get; set; }


        protected virtual void Add()
        {
            Items.Add(new T
            {

                IsNew = true

            });
            Selected = Items.Last();
            ShowDetail(new MaintainActionCommand<T> { Item = Selected, DataService = flightDataService });
        }

       


        protected async void Delete(T item)
        {
            bool deleted;
            try
            {
                deleted = await flightDataService.Delete<T>(item);
            }
            catch (Exception)
            {

                deleted = false;
            }


            if (deleted)
                Items.Remove(item);
            else
            {
                await Messager.ShowMessage("Unable to remove this item");
            }
        }

        public abstract void Load();

        public Action<MaintainActionCommand<T>> ShowDetail { get; set; }


  
        

        public MaintainViewModelBase(IFlightDataService flightDataService)
        {
            this.flightDataService = flightDataService;
            EditCommand = new DelegateCommand<T>((f) => ShowDetail(new MaintainActionCommand<T> { Item = f, 
                DataService = flightDataService }),
                (f) => { return f != null; });
            RaisePropertyChanged(() => EditCommand);
            DeleteCommand = new DelegateCommand<T>((f) => Delete(f), (f) => { return f != null; });
            RaisePropertyChanged(() => DeleteCommand);
            AddCommand = new DelegateCommand<T>((f) => Add(), (f) => { return true; });
            RaisePropertyChanged(() => AddCommand);
        }

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
