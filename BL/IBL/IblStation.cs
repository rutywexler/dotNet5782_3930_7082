using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IblStation
    {
        public void AddStation(int id, string name, Location location, int chargeSlots);
        public void UpdateStation(int id, string name, int chargeSlots);
        public BaseStation GetStation(int id);
        public IEnumerable<BaseStation> GetStations();
        public IEnumerable<BaseStation> GetStaionsWithEmptyChargeSlots();
    }
}
