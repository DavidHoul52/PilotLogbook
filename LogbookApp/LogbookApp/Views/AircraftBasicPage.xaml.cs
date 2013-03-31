using System;
using System.Collections.Generic;
using LogbookApp.Commands;
using LogbookApp.Data;
using LogbookApp.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace LogbookApp.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    /// 

       
    
    public sealed partial class AircraftBasicPage : LogbookApp.Common.LayoutAwarePage
    {

        private AircraftViewModel viewModel;
        private FlightActionCommand _flightActionCommand;
        private MaintainActionCommand<Aircraft> _aircraftActionCommand;

        public AircraftBasicPage(Page callingPage)
        {
            this.InitializeComponent();
            viewModel = new AircraftViewModel();
            viewModel.GoBack = () => GoBack(callingPage);
            DataContext = viewModel;
        }

        

        public AircraftBasicPage(FlightActionCommand flightActionCommand, Page callingPage) : this(callingPage)
        {
            _flightActionCommand = flightActionCommand;
            if (_flightActionCommand != null) viewModel.Flight = _flightActionCommand.Flight;
        }

        public AircraftBasicPage(MaintainActionCommand<Aircraft> aircraftActionCommand, Page callingPage)
            : this(callingPage)
        {
            _aircraftActionCommand = aircraftActionCommand;
            if (_aircraftActionCommand != null)
            {
                viewModel.DataService = _aircraftActionCommand.DataService;
                viewModel.Aircraft = _aircraftActionCommand.Item;

            }
        }

   
   
     
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
          
        }


        protected async override void SaveState(Dictionary<String, Object> pageState)
        {
        
        }

        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
          
            
          
            
            base.OnNavigatingFrom(e);
        }

        private async void GoBack(Page callingPage)
        {
           await viewModel.Save();

            if (_flightActionCommand != null)
                _flightActionCommand.OnCompleted(_flightActionCommand.Flight);
            if (_aircraftActionCommand != null)
                _aircraftActionCommand.OnCompleted();
           Window.Current.Content = callingPage;
           Window.Current.Activate();
        }
    }
}
