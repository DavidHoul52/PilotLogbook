using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Controls.DateClasses
{
    public class Calendar
    {
        public List<int> DaysOfMonth()
        {
            List<int> days = new List<int>();
            for (int i = 0; i < 31; i++)
            {
                days.Add(i + 1);
            }
            return days;
        }



        public List<string> Months
        {
            get
            {
                return new List<string>{
            "January","February","March","April","May","June","July","August","September","October",
            "November","December"};
            }
        }

        public List<int> Years()
        {
            return new List<int> { 2008, 2009, 2010, 2011, 2012, 2013 };
        }

        public List<int> Hours()
        {
            List<int> result = new List<int>();
            for (int i = 1; i <= 24; i++)
            {
                result.Add(i);
            }

            return result;
        }

        public List<int> FiveMinIntervals()
        {
            List<int> result = new List<int>();
            for (int i = 5; i <= 60; i=i+5)
            {
                result.Add(i);
            }
            return result;
        }
    }
}
