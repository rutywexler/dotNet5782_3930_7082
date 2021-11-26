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
        public class ParcelInTransfer
        {
            public int ID { get; set; }
            public Priorities Priority { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Recipient { get; set; }
            public bool PackageStatus { get; set; }
            public WeightCategories Weight { get; set; }
            public Location CollectPackage { get; set; }
            public Location DeliveryDestination { get; set; }
            public double DeliveryDistance { get; set; }
        }
    }
   
}
