using System;
using System.Collections.Generic;
using Windows.Networking.Proximity;
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

        public AircraftBasicPage()
        {
            this.InitializeComponent();
            viewModel = new AircraftViewModel();

            viewModel.Messager = new Messager();
            DataContext = viewModel;

            viewModel.ShowAircraftType = ActionAddAircraftType;
        }

        private void ActionAddAircraftType(AircraftActionCommand command)
        {
            Frame.Navigate(typeof(AircraftTypeBasicPage), command);
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
            if (navigationParameter is FlightActionCommand)
            {
                _flightActionCommand = navigationParameter as FlightActionCommand;
                if (_flightActionCommand != null) viewModel.Flight = _flightActionCommand.Flight;
            }
            if (navigationParameter is MaintainActionCommand<Aircraft>)
            {
                _aircraftActionCommand = navigationParameter as MaintainActionCommand<Aircraft>;
                if (_aircraftActionCommand != null)
                {
                    viewModel.DataService = _aircraftActionCommand.DataService;
                    viewModel.Aircraft = _aircraftActionCommand.Item;

                }
            }

           

        }


        protected async override void SaveState(Dictionary<String, Object> pageState)
        {

        }

        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {




            base.OnNavigatingFrom(e);
        }




        protected async override void GoBack(object sender, RoutedEventArgs e)
        {
            await viewModel.Save();

            //if (_flightActionCommand != null)
            //    _flightActionCommand.OnCompleted(_flightActionCommand.Flight);
            //if (_aircraftActionCommand != null)
            //    _aircraftActionCommand.OnCompleted();

            Frame.GoBack();



        }
    }
}
