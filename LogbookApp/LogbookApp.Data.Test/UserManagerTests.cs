using System;
using System.Linq;
using System.Text;
using LogbookApp.Mocks;
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
           
            target= new UserManager();
        }

      

        [TestMethod]
        public void ShouldSetUserLastUpdated()
        {
            
           var user= target.CreateUser("",_now);
            Assert.AreEqual(_now, user.TimeStamp);


        }
    }
}
