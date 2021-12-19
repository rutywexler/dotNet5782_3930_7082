using BO;
using System.Collections.Generic;
using static BO.Enums;

namespace BlApi
{
    public interface IblDrone
    {
        public void AddDrone(int id, DO.WeightCategories MaxWeight, string Model, int stationId);
        public void UpdateDrone(int id, string name);
        public void SendDroneForCharge(int id);
        public void ReleaseDroneFromCharging(int id,double time);
        public Drone GetDrone(int id);
        public IEnumerable<DroneToList> GetDrones();
        public IEnumerable<DroneToList> GetSomeDronesByStatus(DroneStatus droneStatus);
        public IEnumerable<DroneToList> GetSomeDronesByWeight(WeightCategories WeightCategories);
    }
}
