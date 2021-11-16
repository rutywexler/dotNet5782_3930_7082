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
        public class PackageInCustomer
        {
            public int ID { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities priority { get; set; }
            public DroneStatuses Status { get; set; }
            public CustomerInPackage CustomerInDelivery { get; set; }

        }
    }
}
