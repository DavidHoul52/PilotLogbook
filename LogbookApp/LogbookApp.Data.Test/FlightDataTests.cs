using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LogbookApp.Data.Test
{
   

    [TestClass]
    public class FlightDataTests
    {

        private FlightData _target;

        [TestInitializeAttribute]
        public void Setup()
        {
            _target= new FlightData();
        }

        [TestMethod]
        public void ShouldUpdate()
        {
            Flight flight = new Flight {id =1, Reg = "X"};
            var flights = new ObservableCollection<Flight>();
            flights.Add(flight);
            flights.Add(new Flight {id=2});
            flights.Add(new Flight { id = 3 });
            FlightData.Update(new Flight {id=1, Reg="Y"},flights);
            var updated = flights.Where(x => x.id == 1).First();
            Assert.AreEqual("Y",updated.Reg);
        }
    }
}
