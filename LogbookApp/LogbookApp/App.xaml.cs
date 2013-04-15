using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Foundation.Metadata;
using LogbookApp.Common;
using LogbookApp.Data;
using LogbookApp.Views;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using LogbookApp.Services;
using Windows.System.UserProfile;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;


// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace LogbookApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += OnResuming; 
            
        }

        public static IFlightDataService Data { get; set; }
        public static string DisplayName { get; set; }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        /// 
        /// 
        private Frame _rootFrame;
        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {
            
            bool loadState = (args.PreviousExecutionState == ApplicationExecutionState.Terminated || args.PreviousExecutionState==ApplicationExecutionState.NotRunning );
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                  // Begin executing setup operations.


                // Place the frame in the current Window
                ExtendedSplash extendedSplash = new ExtendedSplash(args.SplashScreen);
                
                Window.Current.Content = extendedSplash;
                Window.Current.Activate();
            
            }

            await PerformDataFetch(loadState);
        }

        private async Task PerformDataFetch(bool loadState)
        {
            if (loadState)
            {
                
               DisplayName = await UserInformation.GetDisplayNameAsync();
                //DisplayName = "test2";
                Data = MobileService.Client;
                await Data.GetData(DisplayName);
            }
            // Tear down the extended splash screen after all operations are complete.
            RemoveExtendedSplash(); 

        }

        private void RemoveExtendedSplash()
        {
            if (_rootFrame == null)
                _rootFrame = new Frame();
            if (_rootFrame != null) _rootFrame.Navigate(typeof(FlightsPage));
            Window.Current.Content = _rootFrame;
        }


        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete(); 
        }


        private async void OnResuming(Object sender, Object e)
        {
         
           // await PerformDataFetch(true);
        }


        public static async Task RefreshFlightData()
        {
            if (Data.FlightsChanged)
               await Data.GetFlights();
        }

        public static async Task GetAllFlightData()
        {
            
                await Data.GetData(DisplayName);
        }
    }
}
