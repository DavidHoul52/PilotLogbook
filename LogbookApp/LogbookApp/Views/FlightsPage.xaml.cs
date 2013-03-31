
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.PlayTo;
using Windows.UI.Xaml;
using LogbookApp.Data;
using LogbookApp.ViewModel;
using LogbookApp.Views;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using LogbookApp.Commands;
using LogbookApp.Services;

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
            FlightDataService data = MobileService.Client;
                
                

            viewModel = new FlightsPageViewModel(data);
         
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
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
        }

        private void ActionShowDetail(Flight flight )
        {
            Window.Current.Content = new FlightDetailPage1(flight, this,()=>viewModel.Refresh());
            Window.Current.Activate();
            
        }

        private void ActionShowTotals(TotalsActionCommand command)
        {
            Window.Current.Content = new TotalsPage(command, this);
            
        }


        private void ActionShowAircraft()
        {
            Window.Current.Content = new MaintainAircraft(this);
            
        }

        private void ActionShowAirfields()
        {
            Window.Current.Content = new MaintainAirfields(this);
            
        }

        private void ActionShowAircraftTypes()
        {
            Window.Current.Content = new MaintainAircraftTypes(this);
            
        }

        internal Rect SplashImageRect;
        internal bool Dismissed; 

        public void SetExtendedSplashInfo(Rect splashImageRect, bool dismissed)
        {
            SplashImageRect = splashImageRect;
            Dismissed = dismissed;

          
        }

        public async Task Load()
        {
            await viewModel.Load();
        }
    }
}
