using System;
using System.Collections.Generic;
using LogbookApp.Commands;
using LogbookApp.Data;
using LogbookApp.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace LogbookApp.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AirfieldBasicPage : LogbookApp.Common.LayoutAwarePage
    {
        private AirfieldViewModel viewModel;
        private FlightAirfieldActionCommand flightActionCommand;
        private MaintainActionCommand<Airfield> maintainActionCommand;
        

        public AirfieldBasicPage()
        {
            this.InitializeComponent();
            viewModel = new AirfieldViewModel();
         
            viewModel.Messager = new Messager();
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
            if (navigationParameter is FlightAirfieldActionCommand)
            {
                flightActionCommand = navigationParameter as FlightAirfieldActionCommand;
                if (flightActionCommand != null)
                {
                    viewModel.Flight = flightActionCommand.Flight;
                    viewModel.Airfield = flightActionCommand.Airfield;
                }
            }

            if (navigationParameter is MaintainActionCommand<Airfield>)
            {
                var actionCommand = navigationParameter as MaintainActionCommand<Airfield>;
                if (actionCommand != null)
                {
                    viewModel.Airfield = actionCommand.Item;
                    viewModel.DataService = actionCommand.DataService;
                }
            }
        }

        
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private new async void GoBack(object sender, RoutedEventArgs e)
        {
            if (flightActionCommand != null)
                switch (flightActionCommand.AirfieldDesignation)
                {
                    case AirfieldDesignation.From:
                        await viewModel.SaveFrom();
                        break;
                    case AirfieldDesignation.To:
                        await viewModel.SaveTo();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            else
                await viewModel.Save();
            
            Frame.GoBack();
        }
    }
}
