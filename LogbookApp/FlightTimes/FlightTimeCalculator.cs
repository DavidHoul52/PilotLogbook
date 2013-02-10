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
            // result.Night = CalcTimePeriod(flights.Where(x=>x.Night).ToList(), FromDate, ToDate);
            // result.Day = CalcTimePeriod(flights.Where(x => !x.Night).ToList(), FromDate, ToDate);
            // result.Currency90Days = CalcTimePeriod(flights, ToDate.AddDays(-90), ToDate);
            //result.Currency28Days = CalcTimePeriod(flights, ToDate.AddDays(-28), ToDate);
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
                    Debug.WriteLine("{0}   {1}  {2} {3}", flight.Duration.ToString(), result.GrandTotal.ToString(),
                        result.GrandTotal.TotalHours.ToString("00"), result.GrandTotal.Minutes.ToString("00"));
                    result.Landings += flight.LDG.GetValueOrDefault(0);
                    if (flight.Aircraft != null && flight.Aircraft.AcClass == AcClass.SEP)
                    {
                        if (flight.Capacity != null && flight.Capacity.CapacityEnum == CapacityEnum.P1)
                            result.SEPpic = result.SEPpic.Add(flight.Duration);
                        else
                            result.SEPp2Dual = result.SEPp2Dual.Add(flight.Duration);
                    };
                    if (flight.Aircraft != null && flight.Aircraft.AcClass == AcClass.ME)
                    {
                        if (flight.Capacity != null && flight.Capacity.CapacityEnum == CapacityEnum.P1)
                            result.MEpic = result.MEpic.Add(flight.Duration);
                        else
                            result.MEp2Dual = result.MEp2Dual.Add(flight.Duration);
                    };

                    if (flight.InstrumentFlying != null)
                        result.InstrumentFlying = result.InstrumentFlying.Add(new TimeSpan(flight.InstrumentFlying.Value.Ticks));
                    if (flight.SimulatedInstrumentFlying != null)
                        result.SimInstrumentFlying = result.SimInstrumentFlying.Add(new TimeSpan(flight.SimulatedInstrumentFlying.Value.Ticks));

                }
             
                
            }
            return result;
        }
    }
}
