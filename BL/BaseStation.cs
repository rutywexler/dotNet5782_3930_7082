using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class BaseStation
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public Location location { get; set; }
            public int NumberOfChargingStations { get; set; }
            public int MyProperty { get; set; }
            public List<Drone> SkimmersInCharge = new List<Drone>();

        }
    }
}
