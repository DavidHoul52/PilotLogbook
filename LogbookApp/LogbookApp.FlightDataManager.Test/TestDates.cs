using System;

namespace LogbookApp.FlightDataManagerTest
{
    public class TestDates
    {
        public static DateTime Now20130101
        {
            get
            {
                return new DateTime(2013, 01, 01);
            }
            
        }

        public static DateTime? NowLess1 {
            get
            {
                return Now.AddDays(-1);
            }  }

        public static DateTime Now
        {
            get
            {
                return new DateTime(2013, 01, 03);
            }
        }

        public static DateTime? NowLess2{   get
            {
                return Now.AddDays(-2);
            }  }
    }
}