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
    public partial class BLDrone : IblDrone
    {
        public void AddDrone(int id, string model, Enums.WeightCategories MaxWeight, int stationId)
        {
            throw new NotImplementedException();
        }

        public Drone GetDrone(int id)
        {
            throw new NotImplementedException();
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
