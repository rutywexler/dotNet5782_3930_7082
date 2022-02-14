using PL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Model.Po;

namespace PL
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

        public static DroneInParcel ConvertDroneInParcel(BO.DroneInPackage drone)
        {
            return new()
            {
                Id = drone.ID,
                BatteryStatus = drone.BatteryStatus,
                CurrentLocation = LocationConverter.ConvertLocation(drone.Location)
            };
        }

        public static DroneForList ConvertDroneToList(BO.DroneToList drone)
        {
            return new DroneForList
            {
                Id = drone.DroneId,
                Model = drone.ModelDrone,
                Battery = (int)drone.BatteryDrone,
                Status = (Enums.DroneStatuses)drone.DroneStatus,
                Weight = (Enums.WeightCategories)drone.DroneWeight,
                Location = LocationConverter.ConvertLocation(drone.Location),
                ParcelId = drone.ParcelId??0
            };
        }

 

        //internal static DroneForList ConvertDroneToList(DroneInParcel droneInParcel)
        //{
        //    return new DroneForList
        //    {
        //        Id = droneInParcel.Id,
        //        Model = droneInParcel.mo,
        //        Battery = (int)droneInParcel.BatteryStatus,
        //        Status = (Enums.DroneStatuses)droneInParcel.DroneStatus,
        //        Weight = (Enums.WeightCategories)drone.DroneWeight,
        //        Location = LocationConverter.ConvertLocation(drone.Location),
        //        DeliveryId = (int)drone.ParcelId
        //    };
        //}
    }
}
