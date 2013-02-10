using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using Microsoft.WindowsAzure.MobileServices;

namespace LogbookApp.Services
{
    public class MobileService
    {
        private static Data.FlightDataService _client;

        public static Data.FlightDataService Client
        {
            get
            {
                if (_client == null)
                    _client = new FlightDataService(new MobileServiceClient(
                "https://worldpilotslogbook.azure-mobile.net/", "LRlXCJsDuLcggcInPASNkoyofIwtuk47"));
                return _client;
            }
        }
    }
}
