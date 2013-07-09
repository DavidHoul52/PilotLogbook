using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary2.DataManagerTests;

namespace LogbookApp.FlightDataManagerTest
{
    public class FlightDataManagerTestsSaveFlight : DataManagerTestBase<FlightDataManager, FlightData, User>
    {
        [TestMethod]
        public void NewFlightShouldHavePopulatedLookups()
        {
            Target.SaveFlight(new Flight(), TestDates.Now20130101);
            
        }
    }

    public class TestDates
    {
        public static DateTime Now20130101
        {
            get
            {
                return new DateTime(2013, 01, 01);
            }
            
        }
    }
}
