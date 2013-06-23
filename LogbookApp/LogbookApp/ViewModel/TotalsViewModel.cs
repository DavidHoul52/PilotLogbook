using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTimes;
using LogbookApp.Commands;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class TotalsViewModel : ViewModelBase
    {
        private FlightTimeCalculator calculator;

        public TotalsViewModel()
        {
            calculator = new FlightTimeCalculator();
       
            
        }

    

        public List<Flight> Flights { get; set; }

    

        public FlightCalcTotals Totals { get; set; }

        private TotalsActionCommand _command;
        public TotalsActionCommand Command
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
                
          
            }
        }

        public async Task Load()
        {
            await App.GetAllFlightData();
            Totals = calculator.Calc(App.FlightDataManager.Data.Flights, _command.FromDate, _command.ToDate);

            RaisePropertyChanged(() => Totals);
            
        }
    }
}
