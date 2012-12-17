using LogbookApp.Data;
using Microsoft.WindowsAzure.MobileServices;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LogbookApp.InitSqlite
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
        public 
            MobileServiceClient MobileService = new MobileServiceClient( "https://win8pilotslogbook.azure-mobile.net/",  "muBOJHLaoxgRzKMhnmjhbqfSeVfInI19");
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
         //   AcType acType = new AcType { Code = "C-152" };
          //  await MobileService.GetTable<AcType>().InsertAsync(acType);
            //Flight flight = new Flight {Reg="G-WIZZ", Depart= DateTime.Today, Arrival=DateTime.Today, Captain="Haddock",Date=DateTime.Today,
            //AcTypeId=1};
            // await MobileService.GetTable<Flight>().InsertAsync(flight);
            var poo = new FlightDataService(MobileService);
            var goo= await poo.GetAllFlights();
            
        }
            
    }
            
          
          

    
}
