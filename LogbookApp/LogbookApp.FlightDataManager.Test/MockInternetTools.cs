using InternetDetection;

namespace LogbookApp.FlightDataManagerTest
{
    public class MockInternetTools : IInternetTools
    {
        public event InternetTools.InternetConnectionChangedHandler InternetConnectionChanged;
        public bool IsConnected { get; private set; }
        public void SetConnected(bool connected)
        {
            IsConnected = connected;
        }
    }
}