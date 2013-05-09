using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using LogbookApp.FlightDataManagement;
using LogbookApp.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class SyncManagerTests
    {
        private SyncManager target;

        [TestInitialize]
        public void Setup()
        {
            target = new SyncManager(new MockFlightDataService(DataType.OnLine));
        }

        [TestMethod]
        public void Should()
        {
            target.UpdateOnlineData(new FlightData());
        }
    }
}
