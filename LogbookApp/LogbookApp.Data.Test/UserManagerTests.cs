using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.Data.Test
{
    [TestClass]
    public class UserManagerTests
    {
        private UserManager target;
        private MockFlightDataService flightDataService;
        private DateTime _now;

        [TestInitialize]
        public void Setup()
        {
            _now = new DateTime(2013,5,5);
            flightDataService = new MockFlightDataService(DataType.None);
            target= new UserManager();
        }

        [TestMethod]
        public void ShouldGetUserLastUpdated()
        {
            var lastupdated = new DateTime(2013, 5, 3);
            flightDataService.SetLastUpdated(lastupdated);
            target.GetUser(flightDataService,_now);
            Assert.AreEqual(lastupdated,target.User.LastUpdated);


        }

        [TestMethod]
        public void ShouldSetUserLastUpdated()
        {
            
            target.GetUser(flightDataService,_now);
            Assert.AreEqual(_now, target.User.LastUpdated);


        }
    }
}
