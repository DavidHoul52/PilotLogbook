using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;

namespace FlightTimes
{
    public class FlightTimeCalculator
    {
        public FlightCalcTotals Calc(List<Flight> flights, DateTime FromDate, DateTime ToDate)
        {
        
            var result = new FlightCalcTotals(); 
             result.Total = CalcTimePeriod(flights, FromDate, ToDate);
             result.Night = CalcTimePeriod(flights.Where(x => x.Night).ToList(), FromDate, ToDate);
             result.Day = CalcTimePeriod(flights.Where(x => !x.Night).ToList(), FromDate, ToDate);
             result.Currency90Days = CalcTimePeriod(flights, ToDate.AddDays(-90), ToDate);
             result.Currency28Days = CalcTimePeriod(flights, ToDate.AddDays(-28), ToDate);
             result.Currency7Days = CalcTimePeriod(flights, ToDate.AddDays(-7), ToDate);
             result.Currency45Days = CalcTimePeriod(flights, ToDate.AddDays(-45), ToDate);
             return result;
        }


        private FlightCalcResult CalcTimePeriod(List<Flight> flights, DateTime FromDate, DateTime ToDate)
        {
            var result = new FlightCalcResult();
         
            foreach (var flight in flights)
            {
                if (flight.Date >= FromDate && flight.Date <= ToDate)
                {
                    
                    result.GrandTotal = result.GrandTotal.Add(flight.Duration);
                 
                    result.Landings += flight.LDG.GetValueOrDefault(0);
                    if (flight.Aircraft != null && flight.Aircraft.AircraftClass == AircraftClass.SEP)
                    {
                        if (flight.Capacity != null && flight.Capacity.InCommand)
                            result.SEPpic = result.SEPpic.Add(flight.Duration);
                        else
                            result.SEPp2Dual = result.SEPp2Dual.Add(flight.Duration);
                    };
                    if (flight.Aircraft != null && flight.Aircraft.AircraftClass == AircraftClass.Multi)
                    {
                        if (flight.Capacity != null && flight.Capacity.InCommand)
                            result.MEpic = result.MEpic.Add(flight.Duration);
                        else
                            result.MEp2Dual = result.MEp2Dual.Add(flight.Duration);
                    };

                    if (flight.InstrumentFlying != null)
                    {
                        result.InstrumentFlying = result.InstrumentFlying.Add(
                            flight.InstrumentFlying.Value.Time());
                    }
                    if (flight.SimulatedInstrumentFlying != null)
                        result.SimInstrumentFlying = result.SimInstrumentFlying.Add
                            (flight.SimulatedInstrumentFlying.Value.Time());

                }
             
                
            }
            return result;
        }
    }
}
