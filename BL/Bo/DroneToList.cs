using BL.Bo;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;

namespace BL
{
    namespace BO
    {
        public class DroneToList
        {
            public int  DroneId { get; set; }
            public string  ModelDrone { get; set; }
            public WeightCategories DroneWeight { get; set; }
            public double BatteryDrone { get; set; }
            public DroneStatus DroneStatus { get; set; }
            public Location Location { get; set; }
            public int ? ParcelId { get; set; }
            public override string ToString() => this.ToStringProps();

        }
    }
}
