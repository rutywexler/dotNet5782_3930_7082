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
        class Drone
        {
            public int DroneId { get; set; }
            public string DroneModel { get; set; }
            public WeightCategories Weight { get; set; }
            public int BatteryStatus { get; set; }
            public int DroneStatus { get; set; }
            public DeliveryByTransfer DeliveryTransfer { get; set; }
            public Location DroneLocation { get; set; }
        }
        
    }
}
