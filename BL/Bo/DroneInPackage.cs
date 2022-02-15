using BO;
using System.Collections.Generic;


namespace BO
{
    public class DroneInPackage
    {
        public int ID { get; set; }
        public double BatteryStatus { get; set; }
        public Location Location { get; set; }
        public List<ParcelInCustomer> DeliverieToCustomer = new();
        public List<ParcelInCustomer> Delivery_list_customer_to_customer = new List<ParcelInCustomer>();
        public override string ToString() => this.ToStringProps();


    }
}

