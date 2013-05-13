using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogbookApp.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataServiceTestsGetAvailableDataService :FlightDataServiceTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void LocalAvailableShouldGet()
        {
            // async unit tests - see http://blog.stephencleary.com/2012/02/async-unit-tests-part-2-right-way.html
          //  OnlineTestData.SetAvailable(true);
          //  TestLocalStorage.SetTimeStamp(NewerTime); 
          //  await Target.GetAvailableDataService();
          ////  Assert.IsTrue(SyncManager.UpdateOnlineDataCalled);
            
            



        }
    }
}
