using System;

namespace FlightTimes
{
    public static class TimesExtensions
    {
        public static TimeSpan Time(this DateTime time)
        {
            return new TimeSpan(time.Hour,time.Minute,time.Second);
        }
    }
}
