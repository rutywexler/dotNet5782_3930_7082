using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class BaseStation
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int NumberOfChargingStations { get; set; }

            public List<Drone> DronesInCharge = new List<Drone>();

        }
    }
}
