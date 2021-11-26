using IBL;
using IBL.BO;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IblStation
    {
        public void AddStation(int id, string name, Location location, int chargeSlots)
        {
            BaseStation BaseStation = new BaseStation();
            BaseStation.NumberOfChargingStations‏ = 1;

        }

        public IEnumerable<BaseStation> GetStaionsWithEmptyChargeSlots()
        {
            throw new NotImplementedException();
        }

        public BaseStation GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseStation> GetStations()
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(int id, string name, int chargeSlots)
        {
            throw new NotImplementedException();
        }
    }
}
