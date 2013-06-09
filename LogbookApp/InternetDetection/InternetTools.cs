using System;
using Windows.Networking.Connectivity;

namespace InternetDetection
{
   // based on  http://www.guruumeditation.net/blog/internet-connection-detection-in-winrt
    public class InternetTools : IInternetTools
    {
        //public delegate void InternetConnectionChangedHandler(object sender, InternetConnectionChangedEventArgs args);

        //public event InternetConnectionChangedHandler InternetConnectionChanged;

        public InternetTools()
        {
          //  NetworkInformation.NetworkStatusChanged += NetworkInformationNetworkStatusChanged;
        }

        //private void NetworkInformationNetworkStatusChanged(object sender)
        //{
        //    var arg = new InternetConnectionChangedEventArgs { IsConnected = (NetworkInformation.GetInternetConnectionProfile() != null) };

        //    if (InternetConnectionChanged != null)
        //        InternetConnectionChanged(null, arg);
        //}

        public bool IsConnected
        {
            get
            {
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
