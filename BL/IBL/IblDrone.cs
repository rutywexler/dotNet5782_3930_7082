using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;
using IBL.BO;
using BL.BO;

namespace IBL
{
    public interface IblDrone
    {
        public void AddDrone(Drone drone ,int stationId);
        public void UpdateDrone(int id, string name);
        public void SendDroneForCharge(int id);
        public void ReleaseDroneFromCharging(int id, float timeOfCharge);
        public Drone GetDrone(int id);
        public IEnumerable<DroneToList> GetDrones();
        public IEnumerable<DroneToList> GetSomeDronesByStatus(DroneStatus droneStatus);
        public IEnumerable<DroneToList> GetSomeDronesByWeight(WeightCategories WeightCategories);
    }
}
