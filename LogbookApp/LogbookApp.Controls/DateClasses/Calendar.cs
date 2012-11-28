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
    }
}
