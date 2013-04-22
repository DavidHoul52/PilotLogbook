using System;
using LogbookApp.Data;
using Microsoft.WindowsAzure.MobileServices;

namespace LogbookApp.Services
{
    public class MobileService


    {


        public MobileService(Action onDisconnected)
        {
            _client = new FlightDataService(new MobileServiceClient(
               "https://worldpilotslogbook.azure-mobile.net/", "LRlXCJsDuLcggcInPASNkoyofIwtuk47"));
            _client.OnDisconnectedAction = onDisconnected;
        }

        private FlightDataService _client;

        public FlightDataService Client
        {
            get
            {
             
                   
                return _client;
            }
        }
    }
}
