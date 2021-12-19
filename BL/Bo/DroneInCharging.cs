using Bo;

namespace BO
{
    public class DroneInCharging
    {
        public int ID { get; set; }
        public double BatteryStatus { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}


