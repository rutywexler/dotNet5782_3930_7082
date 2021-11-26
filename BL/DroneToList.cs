﻿using IBL.BO;
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
            public int DroneId { get; set; }
            public string  ModelDrone { get; set; }
            public WeightCategories DroneWeight { get; set; }
            public int BatteryDrone { get; set; }
            public DroneStatuses DroneStatus { get; set; }
            public Location DroneLocation { get; set; }
            public int PackageNumberIsTransferred { get; set; }
        }
    }
}
