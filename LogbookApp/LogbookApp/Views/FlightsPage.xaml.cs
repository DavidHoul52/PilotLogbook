using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using LogbookApp.Data;
using LogbookApp.ViewModel;
using LogbookApp.Views;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using LogbookApp.Commands;


// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace LogbookApp
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    /// 


    public sealed partial class FlightsPage : LogbookApp.Common.LayoutAwarePage
    {
        
        private FlightsPageViewModel viewModel;

        public FlightsPage()
        {
        
            this.InitializeComponent();
            //this.Loaded += (s, e) => VisualStateManager.GoToState(this, "Snapped", false);
                
                

            viewModel = new FlightsPageViewModel(App.Data);
         
            viewModel.ShowDetail = ActionShowDetail;
            viewModel.ShowTotals = ActionShowTotals;
            viewModel.ShowAircraft = ActionShowAircraft;
            viewModel.ShowAirfields = ActionShowAirfields;
            viewModel.ShowAircraftTypes = ActionShowAircraftTypes;
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
           viewModel.Load();
        }

        private void ActionShowDetail(Flight flight )
        {
            Frame.Navigate(typeof(FlightDetailPage1), flight);
        }

        private void ActionShowTotals(TotalsActionCommand command)
        {
          
            Frame.Navigate(typeof(TotalsPage), command);
        }


        private void ActionShowAircraft()
        {
            Frame.Navigate(typeof (MaintainAircraft));
            
            
        }

        private void ActionShowAirfields()
        {
            Frame.Navigate(typeof(MaintainAirfields));
        }

        private async void ActionShowAircraftTypes()
        {
            Frame.Navigate(typeof(MaintainAircraftTypes));
        }

        internal Rect SplashImageRect;
        internal bool Dismissed; 

        public void SetExtendedSplashInfo(Rect splashImageRect, bool dismissed)
        {
            SplashImageRect = splashImageRect;
            Dismissed = dismissed;

          
        }

       
    }
}
