
using LogbookApp.Commands;
using LogbookApp.Data;
using LogbookApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace LogbookApp.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class FlightDetailPage1 : LogbookApp.Common.LayoutAwarePage
    {
        private FlightDetailPageViewModel viewModel;
    


        public FlightDetailPage1()
        {
            InitializeComponent();
            viewModel = new FlightDetailPageViewModel();
            viewModel.ShowAircraft = ActionAddAircraft;
            viewModel.ShowAircraftType = ActionAddAircraftType;
            viewModel.ShowAirfield = ActionAddAirfield;
            viewModel.ShowTotalsAction = ActionShowTotals;
            DataContext = viewModel;
            
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
           viewModel.Flight = navigationParameter as Flight;

            
        }

       
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
           
            base.OnNavigatingFrom(e);
        }

        private void ActionAddAircraft(FlightActionCommand flightActionCommand)
        {
            Frame.Navigate(typeof(AircraftBasicPage), flightActionCommand);
        }

        private void ActionAddAircraftType(FlightActionCommand flightActionCommand)
        {
            Frame.Navigate(typeof(AircraftTypeBasicPage), flightActionCommand);
        }


        private void ActionAddAirfield(FlightAirfieldActionCommand flightActionCommand)
        {
            
            Frame.Navigate(typeof(AirfieldBasicPage), flightActionCommand);
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            viewModel.SaveFlight();
            Frame.GoBack();
        }

        private void ActionShowTotals(TotalsActionCommand command)
        {
            Frame.Navigate(typeof(TotalsPage), command);
        }
        
    }
}
