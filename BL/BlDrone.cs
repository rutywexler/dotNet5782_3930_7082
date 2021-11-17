using BL.BO;
using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IblDrone
    {
        public void AddDrone(int id, string model, Enums.WeightCategories MaxWeight, int stationId)
        {
            Drone newDrone = new Drone();
            newDrone.BatteryStatus = ;
            newDrone.DroneStatus = 0;
            newDrone.DroneLocation = GetDrone(stationId).DroneLocation;
            AssignPackageToDrone(56);

        }

        public Drone GetDrone(int id)
        {
            Drone g = new Drone();
            return g;
        }

        public IEnumerable<Drone> GetDrones()
        {
            throw new NotImplementedException();
        }

        public void ReleaseDroneFromCharging(int id, float timeOfCharge)
        {
            throw new NotImplementedException();
        }

        public void SendDroneForCharge(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDrone(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
