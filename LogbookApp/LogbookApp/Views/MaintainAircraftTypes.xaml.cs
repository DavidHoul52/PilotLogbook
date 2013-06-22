using System;
using System.Collections.Generic;
using LogbookApp.Commands;
using LogbookApp.Data;
using LogbookApp.Services;
using LogbookApp.ViewModel;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace LogbookApp.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MaintainAircraftTypes : LogbookApp.Common.LayoutAwarePage
    {
        private MaintainAircraftTypesViewModel viewModel;
        public MaintainAircraftTypes()
        {
            this.InitializeComponent();
            IFlightDataManager data = App.DataManager;



            viewModel = new MaintainAircraftTypesViewModel(data);

            viewModel.ShowDetail = ActionShowDetail;
          
            viewModel.Messager = new Messager();


            DataContext = viewModel;
            viewModel.Load();
        }

        private void ActionShowDetail(MaintainActionCommand<AcType> command)
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
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }
    }
}
