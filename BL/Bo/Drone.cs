using Bo;
using static BO.Enums;


namespace BO
{
    public class Drone
    {
        public int DroneId { get; set; }
        public string DroneModel { get; set; }
        public WeightCategories Weight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatus DroneStatus { get; set; }
        public ParcelInTransfer DeliveryTransfer { get; set; }
        public Location DroneLocation { get; set; }
        public override string ToString() => this.ToStringProps();

    }

}

