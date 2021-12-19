using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    public struct DroneCharge
    {
        public int DroneId { get; set; }
        public int StationId { get; set; }
        public DateTime StartTime { get; set; }


        ///// <summary>
        ///// DroneCharge constructor
        ///// </summary>
        ///// <param name="droneId">the first int value</param>
        ///// <param name="stationId">the second int value</param>
        //public DroneCharge(int droneId, int stationId)
        //{
        //    DroneId = droneId;
        //    StationId = stationId;

        //}
    }
}
