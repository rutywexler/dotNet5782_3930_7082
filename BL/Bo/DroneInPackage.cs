using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneInPackage
        {
            public int ID { get; set; }
            public int BatteryStatus { get; set; }
            public Location Location { get; set; }
            public List<ParcelInCustomer> DeliverieToCustomer = new ();
            public List<ParcelInCustomer> Delivery_list_customer_to_customer = new List<ParcelInCustomer>();

        }
    }
}
