using BO;
using System.ComponentModel;
using static BO.Enums;

namespace BO
{
    public class DroneToList
    {
        public int DroneId { get; set; }
        public string ModelDrone { get; set; }
        public WeightCategories DroneWeight { get; set; }
        public double BatteryDrone { get; set; }
        public DroneStatus DroneStatus { get; set; }
        public Location Location { get; set; }
        public int? ParcelId { get; set; }

        public override string ToString() => this.ToStringProps();

    }
}

