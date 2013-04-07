using System.Threading.Tasks;
using Windows.UI.Popups;
using System;


namespace LogbookApp.Views
{
    public class Messager : IMessager
    {
        public async Task ShowMessage(string message)
        {
            await new MessageDialog(message).ShowAsync();
           
        }
    }
}