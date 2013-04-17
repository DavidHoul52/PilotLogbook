
namespace LogbookApp.ViewModel
{
    public class PrivacyPolicyViewModel
    {
        public PrivacyPolicyViewModel()
        {
            Notes = "Your Flight Logs are stored in the Cloud using Windows Azure Mobile Services. They " +
                "are not shared with any third party. Apart from any billing information you may give us " +
                "we do not use any of your details apart from your Windows 8 user name. The app does not use cookies";
        }

        public string Notes { get; private set; }
    }
}
