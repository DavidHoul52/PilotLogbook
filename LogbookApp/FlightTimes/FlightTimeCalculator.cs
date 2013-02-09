using System;
using System.Collections.Generic;
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
             result.Night = CalcTimePeriod(flights.Where(x=>x.Night).ToList(), FromDate, ToDate);
             result.Day = CalcTimePeriod(flights.Where(x => !x.Night).ToList(), FromDate, ToDate);
             result.Currency90Days = CalcTimePeriod(flights, ToDate.AddDays(-90), ToDate);
             result.Currency28Days = CalcTimePeriod(flights, ToDate.AddDays(-28), ToDate);
             return result;
        }


        private FlightCalcResult CalcTimePeriod(List<Flight> flights, DateTime FromDate, DateTime ToDate)
        {
            var result = new FlightCalcResult();
         
            foreach (var flight in flights)
            {
                if (flight.Date >= FromDate && flight.Date <= ToDate)
                {
                    result.GrandTotal += flight.Duration;
                    result.Landings += flight.LDG.GetValueOrDefault(0);
                    if (flight.Aircraft != null && flight.Aircraft.AcClass == AcClass.SEP)
                    {
                        if (flight.Capacity != null && flight.Capacity.CapacityEnum == CapacityEnum.P1)
                            result.SEPpic+= flight.Duration;
                        else
                            result.SEPp2Dual += flight.Duration;
                    };
                    if (flight.Aircraft != null && flight.Aircraft.AcClass == AcClass.ME)
                    {
                        if (flight.Capacity != null && flight.Capacity.CapacityEnum == CapacityEnum.P1)
                            result.MEpic += flight.Duration;
                        else
                            result.MEp2Dual += flight.Duration;
                    };

                    if (flight.InstrumentFlying != null)
                        result.InstrumentFlying +=  new TimeSpan(flight.InstrumentFlying.Value.Ticks);
                    if (flight.SimulatedInstrumentFlying != null)
                        result.SimInstrumentFlying += new TimeSpan(flight.SimulatedInstrumentFlying.Value.Ticks);

                }
                
            }
            return result;
        }
    }
}
