using System;
using Windows.Networking.Connectivity;

namespace InternetDetection
{
   // based on  http://www.guruumeditation.net/blog/internet-connection-detection-in-winrt
    public class InternetTools : IInternetTools
    {
        

   
        public bool IsConnected
        {
            get
            {
                return false;  // for testing
                var profile = NetworkInformation.GetInternetConnectionProfile() ;
                return (profile != null
                        && profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);

            }
        }
    }

    public class InternetConnectionChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
    }
 
}
