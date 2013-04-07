using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LogbookApp.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LogbookApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {

        internal Rect splashImageRect; // Rect to store splash screen image coordinates. 
        internal bool dismissed = false; // Variable to track splash screen dismissal status. 
        internal Frame rootFrame;

        private SplashScreen splash; // Variable to hold the splash screen object. 
        private FlightsPage flightsPage;


        


        public ExtendedSplash(SplashScreen splashscreen)
        {
            this.InitializeComponent();

           
            // Listen for window resize events to reposition the extended splash screen image accordingly. 
            // This is important to ensure that the extended splash screen is formatted properly in response to snapping, unsnapping, rotation, etc... 
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            splash = splashscreen;

            if (splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed. 
                splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image. 
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }

         

        }

     


        // Position the extended splash screen image in the same location as the system splash screen image.
        private void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height;
            extendedSplashImage.Width = splashImageRect.Width;

            // Position the extended splash screen's progress ring.
            this.ProgressRing.SetValue(Canvas.TopProperty, splash.ImageLocation.Y + splash.ImageLocation.Height + 32);
            this.ProgressRing.SetValue(Canvas.LeftProperty,
         splash.ImageLocation.X +
                 (splash.ImageLocation.Width / 2) - 15);
        }

        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view). 
        private void DismissedEventHandler(SplashScreen sender, object args)
        {
            dismissed = true;
            
            

            // Navigate away from the app's extended splash screen after completing setup operations here...
        }

        private void ExtendedSplash_OnResize(object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc... 
            if (splash != null)
            {
                // Update the coordinates of the splash screen image. 
                splashImageRect = splash.ImageLocation;
                PositionImage();
            } 
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

     
    }
}
