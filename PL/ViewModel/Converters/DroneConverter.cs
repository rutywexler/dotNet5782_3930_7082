using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using PL;

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

        public static BO.DroneToList ConvertBackDrone(DroneToAdd drone)
        {
            return new()
            {
                DroneId = (int)drone.Id,
                ModelDrone = drone.Model,
                DroneWeight = (BO.Enums.WeightCategories)drone.Weight,
                DroneStatus = BO.Enums.DroneStatus.Meintenence,
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



        public static Drone ConvertDrone(BO.Drone drone)
        {
            return new Drone
            {
                Id = drone.DroneId,
                Model = drone.DroneModel,
                Battery = (int)drone.BatteryStatus,
                Status = (Enums.DroneStatuses)((int)drone.DroneStatus),
                Weight = (Enums.WeightCategories)drone.Weight,
                Location =LocationConverter.ConvertLocation(drone.DroneLocation),
                DeliveryByTransfer = drone.DeliveryTransfer == null ? null : ParcelConverter.ConvertParcelInTransfer(drone.DeliveryTransfer)
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
