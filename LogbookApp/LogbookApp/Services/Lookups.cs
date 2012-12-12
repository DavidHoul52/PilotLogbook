using LogbookApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogbookApp.Services
{
    public class Lookups
    {
        public List<AcType> AcTypes
        {
            get
            {
                return new List<AcType> { new AcType{ Code="C-152"}, new AcType{ Code="C-172"},new AcType{ Code="P28"} ,new AcType{ Code="P38"} };
            }

        }

        public List<Capacity> Capacity
        {

            get
            {
                return new List<Capacity> {new Capacity {Code= "P1"},new Capacity {Code= "P2"},new Capacity {Code= "Put"} };
            }

        }


        public List<Airfield> Airfields
        {

            get
            {
                return new List<Airfield> { new Airfield { ICAOCode = "EGLF", Name = "Farnborough" }, new Airfield { ICAOCode = "EGTF", Name = "Fairoaks" } };
            }

        }




    }
}
