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
using LogbookApp.Data;
using Microsoft.WindowsAzure.MobileServices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DefaultDataLoader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            FlightDataService data = new FlightDataService(new MobileServiceClient(
               "https://worldpilotslogbook.azure-mobile.net/",  "LRlXCJsDuLcggcInPASNkoyofIwtuk47"));
            await data.ClearTable<Flight>();
            await data.ClearTable<AcType>();
            await data.Insert(new AcType {Code = "C-152"});
            await data.ClearTable<Airfield>();
            await data.Insert(new Airfield { ICAOCode = "EGHR", Name = "Goodwood"});
            await data.ClearTable<Capacity>();
            await data.Insert(new Capacity {Description = "P1"});

        }
    }
}
