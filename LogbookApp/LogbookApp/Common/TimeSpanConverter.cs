using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace LogbookApp.Common
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is TimeSpan)
            {
                TimeSpan timeSpan = (TimeSpan)value;
                if (timeSpan.Days==0)
                  return ((TimeSpan)value).ToString(@"hh\:mm");
                return string.Format("{0}:{1}", timeSpan.TotalHours.ToString("00"), timeSpan.Minutes.ToString("00"));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
