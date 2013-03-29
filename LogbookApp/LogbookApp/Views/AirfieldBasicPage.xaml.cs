using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LogbookApp.Commands;
using LogbookApp.Data;
using LogbookApp.ViewModel;
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
    public sealed partial class AirfieldBasicPage : LogbookApp.Common.LayoutAwarePage
    {
        private AirfieldViewModel viewModel;
        private FlightAirfieldActionCommand flightActionCommand;
        

        public AirfieldBasicPage(Page callingPage)
        {
            this.InitializeComponent();
            viewModel = new AirfieldViewModel();
            viewModel.GoBack = () => GoBack(callingPage);

            DataContext = viewModel;
        }

        public AirfieldBasicPage(MaintainActionCommand<Airfield> command, Page callingPage): this(callingPage)
        {
            if (command != null)
            {
                viewModel.Airfield = command.Item;
                viewModel.DataService = command.DataService;
            }
            
        }

        public AirfieldBasicPage(FlightAirfieldActionCommand command, Page callingPage)
            : this(callingPage)
        {
            flightActionCommand = command;
            if (flightActionCommand != null)
            {
                viewModel.Flight = flightActionCommand.Flight;
                viewModel.Airfield = flightActionCommand.Airfield;
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

        
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void GoBack(Page callingPage)
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

            Window.Current.Content = callingPage;
        }
    }
}
