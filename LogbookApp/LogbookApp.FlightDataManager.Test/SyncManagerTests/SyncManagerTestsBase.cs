using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary;

namespace LogbookApp.FlightDataManagerTest
{
  
    public abstract class SyncManagerTestsBase
    {
        protected FlightsSyncManager target;
        protected MockFlightDataService _onlineFlightDataService;
        protected FlightData _sourceFlightData;
        protected FlightData _targetFlightData;
        protected DateTime _newerTimeStamp = new DateTime(2013, 10, 1);
        protected DateTime _olderTimeStamp = new DateTime(2013, 1, 1);



       
        public virtual void Setup()
        {
            _targetFlightData = new FlightData();
            _onlineFlightDataService = new MockFlightDataService(DataType.OnLine, _targetFlightData,"");
            _sourceFlightData = new FlightData();
            target = new FlightsSyncManager(_onlineFlightDataService);
        }
    
    

    
    }
}
