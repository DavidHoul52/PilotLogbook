
using LogbookApp.Data;
using LogbookApp.Data.LocalOData;
using LogbookApp.ViewModel;
using LogbookApp.Views;
using Microsoft.WindowsAzure.MobileServices;
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
            viewModel = new FlightsPageViewModel(new FlightDataService(new MobileServiceClient(
               "https://worldpilotslogbook.azure-mobile.net/",
    "LRlXCJsDuLcggcInPASNkoyofIwtuk47")));
         // viewModel = new FlightsPageViewModel(new LocalFlightDataService());
            viewModel.ShowDetail = ActionShowDetail; 
            DataContext = viewModel;
            viewModel.Load();
            
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
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
        }

        private void ActionShowDetail(Flight flight )
        {
            Frame.Navigate(typeof(FlightDetailPage1), flight);
        }
    }
}
