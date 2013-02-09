using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace FlightTimes.Test
{
    [TestClass]
    public class FlightTimeCalculatorTests
    {
        private FlightTimeCalculator target;

        [TestInitialize]
        public void Setup()
        {
            target = new FlightTimeCalculator();
        }


        [TestMethod]
        public void ShouldCalcTotalFlightTimeZero()
        {
            var result = target.Calc(new List<Flight>(), new DateTime(2013, 1, 1), new DateTime(2013, 3, 1));
            TimeSpan expected = new TimeSpan(0, 0, 0);
            Assert.AreEqual(expected, result.Total.GrandTotal);
        }


        [TestMethod]
        public void ShouldCalcTotalFlightTimeNotZero()
        {
            var flights = new List<Flight> { new Flight { 
                   Date= new DateTime(2013,1,1),
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,0,10,0) },
            new Flight { 
                   Date= new DateTime(2013,1,3),
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,1,10,0) }};
            var result = target.Calc(flights, new DateTime(2013, 1, 1), new DateTime(2013, 3, 1));
            TimeSpan expected = new TimeSpan(1, 20, 0);
            Assert.AreEqual(expected, result.Total.GrandTotal);
        }

        [TestMethod]
        public void ShouldCalcTotalFlightTimeTakingDatesIntoAccount()
        {
            var flights = new List<Flight> { new Flight {
                Date= new DateTime(2012,1,1),
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,0,10,0) 
            },
            new Flight { 
                Date = new DateTime(2013,1,1),
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,1,10,0) }
            };
            var result = target.Calc(flights, new DateTime(2013, 1, 1), new DateTime(2013, 3, 1));
            TimeSpan expected = new TimeSpan(1, 10, 0);
            Assert.AreEqual(expected, result.Total.GrandTotal);
        }


        [TestMethod]
        public void ShouldCalcSEPPIC()
        {
            var flights = new List<Flight> { new Flight {
                Date= new DateTime(2013,1,1),
                Capacity = new Capacity{ Id = (int)CapacityEnum.P1 },
                   Aircraft= new Aircraft{ AcClass = AcClass.SEP },
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,0,10,0) 
            },
            new Flight { 
                Date = new DateTime(2013,1,1),
                Capacity = new Capacity{ Id = (int)CapacityEnum.Put },
                     Aircraft= new Aircraft{ AcClass = AcClass.SEP },
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,1,10,0) }
            };
            var result = target.Calc(flights, new DateTime(2013, 1, 1), new DateTime(2013, 3, 1));
            TimeSpan expected = new TimeSpan(0, 10, 0);
            Assert.AreEqual(expected, result.Total.SEPpic);
        }


        [TestMethod]
        public void ShouldCalcSEPP2Dual()
        {
            var flights = new List<Flight> { new Flight {
                Date= new DateTime(2013,1,1),
                Capacity = new Capacity{ Id = (int)CapacityEnum.P1 },
                     Aircraft= new Aircraft{ AcClass = AcClass.SEP },
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,0,10,0) 
            },
            new Flight { 
                Date = new DateTime(2013,1,1),
                Capacity = new Capacity{ Id = (int)CapacityEnum.Put },
                     Aircraft= new Aircraft{ AcClass = AcClass.SEP },
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,1,10,0) }
            };
            var result = target.Calc(flights, new DateTime(2013, 1, 1), new DateTime(2013, 3, 1));
            TimeSpan expected = new TimeSpan(1, 10, 0);
            Assert.AreEqual(expected, result.Total.SEPp2Dual);
        }


        [TestMethod]
        public void ShouldCalcMEpic()
        {
            var flights = new List<Flight> { new Flight {
                Date= new DateTime(2013,1,1),
                Capacity = new Capacity{ Id = (int)CapacityEnum.P1 },
                Aircraft= new Aircraft{ AcClass = AcClass.ME },
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,0,10,0) 
            },
            new Flight { 
                Date = new DateTime(2013,1,1),
                    Aircraft= new Aircraft{ AcClass = AcClass.SEP },
                Capacity = new Capacity{ Id = (int)CapacityEnum.Put },
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,1,10,0) }
            };
            var result = target.Calc(flights, new DateTime(2013, 1, 1), new DateTime(2013, 3, 1));
            TimeSpan expected = new TimeSpan(0, 10, 0);
            Assert.AreEqual(expected, result.Total.MEpic);
        }



        [TestMethod]
        public void ShouldCalcMEp2Dual()
        {
            var flights = new List<Flight> { new Flight {
                Date= new DateTime(2013,1,1),
                Capacity = new Capacity{ Id = (int)CapacityEnum.P1 },
                Aircraft= new Aircraft{ AcClass = AcClass.ME },
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,0,10,0) 
            },
            new Flight { 
                Date = new DateTime(2013,1,1),
                    Aircraft= new Aircraft{ AcClass = AcClass.ME },
                Capacity = new Capacity{ Id = (int)CapacityEnum.Put },
                Depart = new DateTime(1,1,1,0,0,0), Arrival = new DateTime(1,1,1,1,10,0) }
            };
            var result = target.Calc(flights, new DateTime(2013, 1, 1), new DateTime(2013, 3, 1));
            TimeSpan expected = new TimeSpan(1, 10, 0);
            Assert.AreEqual(expected, result.Total.MEp2Dual);
        }


    }
}
