using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;
using IBL.BO;

namespace IBL
{
    public interface IblDrone
    {
        public void AddDrone(int id, string model, WeightCategories MaxWeight, int stationId);
        public void UpdateDrone(int id, string name);
        public void SendDroneForCharge(int id);
        public void ReleaseDroneFromCharging(int id, float timeOfCharge);
        public Drone GetDrone(int id);
        public IEnumerable<Drone> GetDrones();
    }
}
