using Bo;

namespace BO
{
    public class BaseStationToList
    {

        public int IdStation { get; set; }
        public string NameStation { get; set; }
        public int NumOfAvailableChargingStations { get; set; }
        public int NumOfBusyChargingStations { get; set; }
        public override string ToString() => this.ToStringProps();


    }
}

