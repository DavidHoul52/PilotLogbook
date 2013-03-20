﻿using System;
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
    /// 

       
    
    public sealed partial class AircraftBasicPage : LogbookApp.Common.LayoutAwarePage
    {

        private AircraftViewModel viewModel;
        private FlightActionCommand flightActionCommand;
        private MaintainActionCommand<Aircraft> aircraftActionCommand;

        public AircraftBasicPage()
        {
            this.InitializeComponent();
            viewModel = new AircraftViewModel();

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
            if (navigationParameter is FlightActionCommand)
            {
                flightActionCommand = navigationParameter as FlightActionCommand;
                if (flightActionCommand != null) viewModel.Flight = flightActionCommand.Flight;
            }
            if (navigationParameter is MaintainActionCommand<Aircraft>)
            {
                aircraftActionCommand = navigationParameter as MaintainActionCommand<Aircraft>;
                if (aircraftActionCommand != null)
                {
                    viewModel.DataService = aircraftActionCommand.DataService;
                    viewModel.Aircraft = aircraftActionCommand.Item;
                    
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

        private new async void GoBack(object sender, RoutedEventArgs e)
        {
           await viewModel.Save();
           Frame.GoBack();
        }
    }
}
