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
    public class FlightDataServiceTestsOnDataServiceChanged : FlightDataServiceTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            
        }

        [TestMethod]
        public void IfChangesFromLocalToOnLineThenServiceShouldBeOnline()
        {
            SetupDataType(DataType.OffLine);
            MockInternetTools.SetConnected(true);
            Target.CheckConnectionState();
            Assert.AreEqual(DataType.OnLine, Target.DataService.DataType);
        }

        [TestMethod]
        public void IfChangesFromLocalToOnLineThenShouldUpdateOnline()
        {
            SetupDataType(DataType.OffLine);
            MockInternetTools.SetConnected(true);
            Target.CheckConnectionState();
            Assert.AreEqual(DataType.OnLine,Target.DataService.DataType);
        }


       
     
    }
}
