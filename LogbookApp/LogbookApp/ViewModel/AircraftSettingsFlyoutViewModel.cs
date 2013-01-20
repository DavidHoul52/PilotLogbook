

using System.Collections.ObjectModel;
using LogbookApp.Data;

namespace LogbookApp.ViewModel
{
    public class AircraftSettingsFlyoutViewModel : ViewModelBase
    {

        public AircraftSettingsFlyoutViewModel()
        {
            Aircraft = new ObservableCollection<Aircraft>();
            for (int i = 0; i < 20; i++)
            {
                Aircraft.Add(new Aircraft { Reg = "G_WIZZ" });
            }
            
        }

        public ObservableCollection<Aircraft> Aircraft { get; set; }
    }
}
