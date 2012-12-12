using LogbookApp.Data;
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Set the path in which to store the database.
            string dbRootPath = Path.Combine(new[]
                          {
                            ApplicationData.Current.LocalFolder.Path, "LogBook1"
                          });

           // Create a new connection
           var _db = new SQLiteConnection(dbRootPath);

          // Create the tables if they not exists
        
        _db.CreateTable<AcType>();
        _db.CreateTable<Capacity>();
        _db.CreateTable<Airfield>();
        _db.CreateTable<Flight>();
            
          
          

        }
    }
}
