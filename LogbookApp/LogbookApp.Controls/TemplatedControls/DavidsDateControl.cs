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
        }

        protected override void OnApplyTemplate()
        {

            ComboBox dayComboBox = GetTemplateChild("DayComboBox") as ComboBox;
            dayComboBox.ItemsSource = new Calendar().DaysOfMonth();
            dayComboBox.SelectedIndex = 0;
            ComboBox monthComboBox = GetTemplateChild("MonthComboBox") as ComboBox;
            monthComboBox.ItemsSource = new Calendar().Months();
            monthComboBox.SelectedIndex = 0;
            ComboBox yearComboBox = GetTemplateChild("YearComboBox") as ComboBox;
            yearComboBox.ItemsSource = new Calendar().Years();
            yearComboBox.SelectedIndex = 0;
            base.OnApplyTemplate();

        }
    }
}
