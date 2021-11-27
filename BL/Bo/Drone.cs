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
            public double BatteryStatus { get; set; }
            public DroneStatuses DroneStatus { get; set; }
            public ParcelInTransfer DeliveryTransfer { get; set; }
            public Location DroneLocation { get; set; }
        }
        
    }
}
