using BO;
using System;
using System.Collections.Generic;
using static BO.Enums;

namespace BlApi
{
    public interface IblDrone
    {
        public void AddDrone(int id, WeightCategories MaxWeight, string Model, int stationId);
        public void UpdateDrone(int id, string name);
        public void SendDroneForCharge(int id);
        public void ReleaseDroneFromCharging(int id);
        public Drone GetDrone(int id);
        public IEnumerable<DroneToList> GetDrones();
        public IEnumerable<DroneToList> GetSomeDronesByStatus(DroneStatus droneStatus);
        public IEnumerable<DroneToList> GetSomeDronesByWeight(WeightCategories WeightCategories);
        public void StartSimulator(Action update, int id, Func<bool> stop);
    }
}
