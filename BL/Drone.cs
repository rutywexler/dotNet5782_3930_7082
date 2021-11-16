using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;

namespace IBL
{
    namespace BO
    {
        public class Drone
        {
            public int DroneId { get; set; }
            public string DroneModel { get; set; }
            public WeightCategories Weight { get; set; }
            public int BatteryStatus { get; set; }
            public int DroneStatus { get; set; }
            public PackageInTransfer DeliveryTransfer { get; set; }
            public Location DroneLocation { get; set; }
        }
        
    }
}
