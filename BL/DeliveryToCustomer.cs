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
        class DeliveryToCustomer
        {
            public int ID { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities priority { get; set; }
            public DroneStatuses Status { get; set; }
            public CustomerInDelivery CustomerInDelivery { get; set; }

        }
    }
}
