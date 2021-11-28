using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IBL.BO;

namespace IBL
{
    public interface IblStation
    {
        public void AddStation(BaseStation baseStation);
        public void UpdateStation(int id, string name, int chargeSlots);
        public BaseStation GetStation(int id);
        public IEnumerable<BaseStationToList> GetStations();
        public IEnumerable<BaseStationToList> GetStaionsWithEmptyChargeSlots();
    }
}
