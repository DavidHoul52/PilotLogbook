using LogbookApp.Controls.DateClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace LogbookApp.Controls.TemplatedControls
{
    public sealed class SelectedTimeControl : Control
    {
        private int _hour;
        private int _min;
        private DateTime _baseDate = new DateTime(2001,1,1);
        

        public ComboBox hourComboBox;
        public ComboBox minComboBox;

        

        public SelectedTimeControl()
        {
            this.DefaultStyleKey = typeof(SelectedTimeControl);
            SetInternals(RoundedTime(DateTime.Now));
        }

        private static DateTime RoundedTime(DateTime time)
        {


            int minute = Convert.ToInt32(Math.Ceiling((double)time.Minute / 5) * 5);
            if (Convert.ToInt32(minute) == 60)
                minute = 0;
            
         

            return new DateTime(time.Year, time.Month, time.Day, time.Hour, minute, 0);
        }


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            hourComboBox = GetTemplateChild("HourComboBox") as ComboBox;
            hourComboBox.ItemsSource = new Calendar().Hours();

            minComboBox = GetTemplateChild("MinComboBox") as ComboBox;
            var mins = new Calendar().FiveMinIntervals();
            minComboBox.ItemsSource = mins;

           

            SetControls();

            hourComboBox.SelectionChanged += (s, e) =>
            {
                SetTimeFromControls();
            };

            minComboBox.SelectionChanged += (s, e) =>
            {
                SetTimeFromControls();
            };

         
        }

        private void SetTimeFromControls()
        {
            Time = new DateTime(_baseDate.Year,_baseDate.Month,_baseDate.Day,(int)hourComboBox.SelectedItem,  (int)minComboBox.SelectedItem,0);
        }

        private void SetInternals(DateTime time)
        {
            _hour = time.Hour;
            _min = time.Minute;
        }


        private void SetControls()
        {
            hourComboBox.SelectedItem = _hour;
            minComboBox.SelectedItem = _min;
            
            
        }

        public void SetControls(DateTime time)
        {
            _baseDate = time;
            hourComboBox.SelectedItem = time.Hour;
            minComboBox.SelectedItem = time.Minute;


        }


        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set
            {
                SetValue(TimeProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Date.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime), typeof(SelectedTimeControl), new PropertyMetadata(default(DateTime), SelectedTimeChanged));

        private static void SelectedTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                SelectedTimeControl control = (SelectedTimeControl)d;
                DateTime time = RoundedTime((DateTime)e.NewValue);
                if (control.hourComboBox != null)
                    control.SetControls(time);
                else
                    control.SetInternals(time);
            }



        }
    }
}
