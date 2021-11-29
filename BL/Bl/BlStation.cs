using BL.BO;
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
        public void AddStation(BaseStation baseStation)
        {
            dal.AddStation(baseStation.Id, baseStation.Name, baseStation.Location.Longitude, baseStation.Location.Longitude, baseStation.NumberOfChargingStations);
        }

        public IEnumerable<BaseStationToList> GetStaionsWithEmptyChargeSlots()
        {
            IEnumerable<IDAL.DO.Station> AvailableChargingStationsFromDal = dal.GetAvailableChargingStations();
            List<BaseStationToList> stations = new();
            foreach (var Station in AvailableChargingStationsFromDal)
            {
                stations.Add(ConvertStationToStationForList(Station));
            }
            return stations;
        }

        public BaseStation GetStation(int id)
        {
            var baseStation = dal.GetStation(id);
            var chargeSlots = dal.GetDronechargingInStation(id).Where(charge => charge.StationId == id).ToList();
            var dronesInChargeList = chargeSlots.Select(charge => GetDrone(charge.DroneId)).ToList();

            return new BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                Location = new Location() { Lattitude = baseStation.Lattitude, Longitude = baseStation.Longitude },
                NumberOfChargingStations = baseStation.ChargeSlots - chargeSlots.Count,
                DronesInCharge = dronesInChargeList,
            };
        }


        /// <summary>
        /// Retrieves the list of stations from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<BaseStationToList> GetStations()
        {
            IEnumerable<IDAL.DO.Station> list = dal.GetStations();
            List<BaseStationToList> stations = new();
            foreach (var item in list)
            {
                stations.Add(ConvertStationToStationForList(item));
            }
            return stations;
        }


        /// <summary>
        /// Update a station in the Stations list
        /// </summary>
        /// <param name="id">The id of the station</param>
        /// <param name="name">The new name</param>
        /// <param name="chargeSlots">A nwe number for charging slots</param>
        public void UpdateStation(int id, string name, int chargeSlots)
        {
            var station = dal.GetStation(id);
            dal.RemoveStation(station);
            dal.AddStation(id, name.Equals(string.Empty) ? station.Name : name, station.Longitude, station.Lattitude, chargeSlots == 0 ? station.ChargeSlots : chargeSlots);
        }

        private IDAL.DO.Station FindClosetStation(IEnumerable<IDAL.DO.Station> stations, Location location, double batteryDrone)
        {
            double minDistance = 0;
            double curDistance;
            IDAL.DO.Station Station = new();
            foreach (var item in stations)
            {
                curDistance = Distance(location,
                    new Location() { Lattitude = item.Lattitude, Longitude = item.Longitude });
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    Station = item;
                }
            }
            return Station;
        }

        /// <summary>
        /// Convert a DAL station to BLStationToList satation
        /// </summary>
        /// <param name="station">The sation to convert</param>
        /// <returns>The converted station</returns>
        private BaseStationToList ConvertStationToStationForList(IDAL.DO.Station station)
        {
            return new BaseStationToList()
            {
                IdStation = station.Id,
                NameStation = station.Name,
                NumOfAvailableChargingStations = station.ChargeSlots - dal.NotAvailableChargingPorts(station.Id),
                NumOfBusyChargingStations = dal.NotAvailableChargingPorts(station.Id)
            };
        }
    }
}
