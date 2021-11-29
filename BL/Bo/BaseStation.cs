using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL.Bo;

namespace IBL
{
    namespace BO
    {
        public class BaseStation
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            public int NumberOfChargingStations { get; set; }

            public List<DroneInCharging> DronesInCharge = new List<DroneInCharging>();
            public override string ToString() => this.ToStringProps();

        }
    }
}
