using PL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Converters
{
    public static class DroneConverter
    {
        public static BO.DroneInCharging ConvertBackDroneCharging(DroneInCharging droneInCharging)
        {
            return new BO.DroneInCharging()
            {
                ID = droneInCharging.Id,
                BatteryStatus = droneInCharging.BatteryStatus
            };
        }

        public static DroneInCharging ConvertDroneCharging(BO.DroneInCharging droneInCharging)
        {
            return new DroneInCharging()
            {
                Id = droneInCharging.ID,
                BatteryStatus = (int)droneInCharging.BatteryStatus
            };
        }
    }
}
