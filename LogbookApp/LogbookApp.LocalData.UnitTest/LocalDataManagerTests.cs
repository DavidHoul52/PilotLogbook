using System.Collections.Generic;
using LogbookApp.Data;
using LogbookApp.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.LocalData.UnitTest
{
    [TestClass]
    public class LocalDataManagerTests
    {
        private ILocalStorage _localStorage;
        private LocalDataManager target;
        private List<Flight> _flights;
            
        [TestInitialize]
        public void Setup()
        {
            _localStorage = new LocalStorage();
            _flights = new List<Flight>();
            target = new LocalDataManager(_localStorage);
        }

        [TestMethod]
        public void TestMethod1()
        {
         
        }
    }
}
