using System;
using LogbookApp.Data;
using Microsoft.WindowsAzure.MobileServices;

namespace LogbookApp.Services
{
    public class MobileService


    {


        public MobileService(string displayName)
        {
            _client = new MobileFlightDataService(new MobileServiceClient(
               "https://worldpilotslogbook.azure-mobile.net/", "LRlXCJsDuLcggcInPASNkoyofIwtuk47"),displayName);
            
        }

        private IFlightDataService _client;

        public IFlightDataService Client
        {
            get
            {
             
                   
                return _client;
            }
        }
    }
}
