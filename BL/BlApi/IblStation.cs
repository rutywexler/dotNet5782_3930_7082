using System.Collections.Generic;
using BO;

namespace BlApi
{
    public interface IblStation
    {
        public void AddStation(BaseStation baseStation);
        public void UpdateStation(int id, string name, int chargeSlots);
        public BaseStation GetStation(int id);
        public void RemoveStation(int id);
        public IEnumerable<BaseStationToList> GetStations();
        public IEnumerable<BaseStationToList> GetStaionsWithEmptyChargeSlots();
    }
}
