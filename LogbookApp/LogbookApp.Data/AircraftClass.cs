using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Data
{
    public class AircraftClass
    {
        [IgnoreDataMember]
        public string Text { get; set; }
        [IgnoreDataMember]
        public int Id { get; set; }

        public static List<AircraftClass> Items
        {
            get
            {
                return new List<AircraftClass> { new AircraftClass { Id = 1, Text = "SEP" }, new AircraftClass { Id = 2, Text = "Multi" } };
            }


        }

        public static AircraftClass SEP { get {return Items.First(); }}
        public static AircraftClass Multi { get { return Items.Last(); } }


        public override bool Equals(object obj)
        {
            return ((AircraftClass)obj).Id == this.Id;

        }

        public static bool operator ==(AircraftClass A, AircraftClass B)
        {
            return (A.Id == B.Id);

        }
        public static bool operator !=(AircraftClass A, AircraftClass B)
        {
            return !(A.Id == B.Id);
        }
    }
}
