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
    public sealed class DavidsDateControl : Control
    {
        public DavidsDateControl()
        {
            this.DefaultStyleKey = typeof(DavidsDateControl);
            
            SetInternals(DateTime.Now);
        }

        private int _day;
        private int _month;
        private int _year;
        public ComboBox dayComboBox;
        public ComboBox monthComboBox;
        public ComboBox yearComboBox;
        public List<string> months;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
          

            dayComboBox = GetTemplateChild("DayComboBox") as ComboBox;
            dayComboBox.ItemsSource = new Calendar().DaysOfMonth();
            
            monthComboBox = GetTemplateChild("MonthComboBox") as ComboBox;
            months = new Calendar().Months; ;
            monthComboBox.ItemsSource = months;
          
            yearComboBox = GetTemplateChild("YearComboBox") as ComboBox;
            yearComboBox.ItemsSource = new Calendar().Years();
      
            SetControls();

            dayComboBox.SelectionChanged += (s, e) =>
            {
                SetDateFromControls();
            };

            monthComboBox.SelectionChanged += (s, e) =>
            {
                SetDateFromControls();
            };

            yearComboBox.SelectionChanged += (s, e) =>
            {
                SetDateFromControls();
            };

          

        }

        private void SetDateFromControls()
        {
            int year = (int) yearComboBox.SelectedItem;
            int month = (int) months.FindIndex((s) => { return s == monthComboBox.SelectedItem; }) + 1;
            int day = (int) dayComboBox.SelectedItem;
            if (day<= DateTime.DaysInMonth(year,month))
               Date = new DateTime(year,month, day);
            else
                Date = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            
        }

        public void SetControls()
        {
            dayComboBox.SelectedItem = _day;
            monthComboBox.SelectedItem = months.ElementAt(_month - 1);
            yearComboBox.SelectedItem = _year;
        }

        public void SetControls(DateTime date)
        {
            dayComboBox.SelectedItem = date.Day;
            monthComboBox.SelectedItem = months.ElementAt(date.Month - 1);
            yearComboBox.SelectedItem = date.Year;
        }

        public void SetInternals(DateTime date)
        {
            _day = date.Day;
            _month = date.Month;
            _year = date.Year;

        }


    


        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set {
                SetValue(DateProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for Date.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTime), typeof(DavidsDateControl),  new PropertyMetadata(default(DateTime), SelectedDateChanged));

        private static void SelectedDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DavidsDateControl control = (DavidsDateControl)d;
            DateTime date = (DateTime)e.NewValue;
            if (control.monthComboBox != null)
                control.SetControls(date);
            else
                control.SetInternals(date);
            


        }

        
        
    }
}
