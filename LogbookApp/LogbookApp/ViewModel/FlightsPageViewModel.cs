using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Model;
using System.Collections.ObjectModel;

namespace LogbookApp.ViewModel
{
    public class FlightsPageViewModel
    {
        public FlightsPageViewModel()
        {

            Flights = new ObservableCollection<Flight>();
            for (int i = 0; i < 100; i++)
            {
                Flights.Add(new Flight
                {
                    Date = new DateTime(2012, 2, 2),
                    AcType = "C-152",
                    Reg = "G-ABCD",
                    Captain = "Self",
                    Capacity = "P1",
                    From = "Fairoaks",
                    To = "Goodwood",
                    Depart = new DateTime(2012, 2, 2, 10, 15, 0),
                    Arrival = new DateTime(2012, 2, 2, 11, 15, 0)
                });
            }
            
            
        }

        public ObservableCollection<Flight> Flights { get; set; }

        public void SelectDetail()
        {

        }
    }
}
