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

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
     
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
            
           Window.Current.Content = callingPage;
        }
    }
}
