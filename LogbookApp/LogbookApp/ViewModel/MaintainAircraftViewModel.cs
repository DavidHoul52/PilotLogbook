﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Commands;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class MaintainAircraftViewModel : MaintainViewModelBase<Aircraft>
    {
    

        public MaintainAircraftViewModel(IFlightDataService flightDataService)
        {
            this.flightDataService = flightDataService;
            EditCommand = new DelegateCommand<Aircraft>((f) => ShowDetail(new MaintainActionCommand<Aircraft>
            { Item= f, DataService = flightDataService}),
                (f) => { return f != null; });
            RaisePropertyChanged(() => EditCommand);
            DeleteCommand = new DelegateCommand<Aircraft>((f) => Delete(f), (f) => { return f != null; });
            RaisePropertyChanged(() => DeleteCommand);
            AddCommand = new DelegateCommand<Aircraft>((f) => Add(), (f) => { return true; });
            RaisePropertyChanged(() => AddCommand);
        }

        private Aircraft selected;
        public Aircraft SelectedAircraft
        {
            get
            { return selected; ;}

            set
            {
                selected = value;
                RaisePropertyChanged(() => SelectedAircraft);

            }
        }

        protected override void Add()
        {
            Aircraft.Add(new Aircraft
            {
                AircraftClass = AircraftClass.SEP,
                IsNew = true
                
            });
            SelectedAircraft = Aircraft.Last();
            ShowDetail(new MaintainActionCommand<Aircraft> { Item = SelectedAircraft, DataService = flightDataService });

        }




        protected async override void Delete(Aircraft f)
        {
            bool deleted = await flightDataService.DeleteAircraft(f);
            if (deleted)
                Aircraft.Remove(f);
        }


        


    

        public ObservableCollection<Aircraft> Aircraft { get; set; }

        public async override void Load()
        {

             await flightDataService.GetLookups();
            Aircraft = new ObservableCollection<Aircraft>(flightDataService.Lookups.Aircraft.OrderBy(x => x.Reg));
                
            RaisePropertyChanged(() => Aircraft);

        }
    }
}
