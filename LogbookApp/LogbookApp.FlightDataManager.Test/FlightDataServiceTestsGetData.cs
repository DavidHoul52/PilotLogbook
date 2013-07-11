﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using OnlineOfflineSyncLibrary.TestMocks;

namespace LogbookApp.FlightDataManagerTest
{
    [TestClass]
    public class FlightDataServiceTestsGetData : FlightDataManagerTestBase
    {

        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            StartupAsConnected();
        }

      

        [TestMethod]
        public void ShouldGetFlights()
        {
            StartupAsConnected();
            Assert.IsNotNull(Target.Data.Flights);
        }

    


        [TestMethod]
        public void ShouldGetLookups()
        {
        
            Assert.IsNotNull(Target.Data.Lookups);



        }


        [TestMethod]
        public void ShouldGetUser()
        {
            
            Assert.IsNotNull(Target.Data.User);



        }
    }
}
