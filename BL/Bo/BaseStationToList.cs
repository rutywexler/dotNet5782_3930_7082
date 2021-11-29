using BL.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    namespace BO
    {
        public class BaseStationToList
        {

            public int IdStation { get; set; }
            public string NameStation { get; set; }
            public int  NumOfAvailableChargingStations { get; set; }
            public int  NumOfBusyChargingStations { get; set; }
            public override string ToString() => this.ToStringProps();


        }
    }
}
