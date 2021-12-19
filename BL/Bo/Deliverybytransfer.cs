using Bo;
using static BO.Enums;

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
        public override string ToString() => this.ToStringProps();



    }
}

