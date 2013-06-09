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
    public class FlightDataServiceTestsLocalDataUser : FlightDataServiceTestsBase
    {
        private DateTime now;

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            now = new DateTime(2013, 1, 1);

        }

        //public void LocalUserNoIdShouldUpdateWhenConnected()
        //{
        //    base.SetupDataType(DataType.OffLine);
        //    Target.FlightData.User = new User();
        //    MockInternetTools.SetConnected(true);
        //    Target.SaveFlight(new Flight(), now);
        //}



    }
}
