﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;

namespace IBL
{
    namespace BO
    {
        public class Deliverybytransfer
        {
            public int ID { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities priority { get; set; }
            public bool DeliveryStatus { get; set; }
            public double CollectionLocation { get; set; }
            public double DeliveryDestinationLocation { get; set; }
            public double TransportDistance { get; set; }


        }
    }
}
