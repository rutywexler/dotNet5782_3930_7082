using BL.Bl;
using BL.BO;
using IBL;
using IBL.BO;
using DalApi;
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
            try {
                dal.AddStation(baseStation.Id, baseStation.Name, baseStation.Location.Longitude, baseStation.Location.Longitude, baseStation.NumberOfChargingStations); 
              }
            catch (DalApi.DO.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {
                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }

        }

        public IEnumerable<BaseStationToList> GetStaionsWithEmptyChargeSlots()
        {
            IEnumerable<DalApi.DO.Station> AvailableChargingStationsFromDal = dal.GetAvailableChargingStations();
            List<BaseStationToList> stations = new();
            foreach (var Station in AvailableChargingStationsFromDal)
            {
                stations.Add(ConvertStationToStationForList(Station));
            }
            return stations;
        }

        public BaseStation GetStation(int id)
        {
            try
            {
                var baseStation = dal.GetStation(id);
                var chargeSlots = dal.GetDronechargingInStation(id).Where(StationId => StationId == id).ToList();

                return new BaseStation()
                {
                    Id = baseStation.Id,
                    Name = baseStation.Name,
                    Location = new Location() { Lattitude = baseStation.Lattitude, Longitude = baseStation.Longitude },
                    NumberOfChargingStations = baseStation.ChargeSlots - chargeSlots.Count,
                    DronesInCharge = ConvertDroneToDroneToDroneInCharging(id),
                };
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }


        /// <summary>
        /// Retrieves the list of stations from the data and converts it to station to list
        /// </summary>
        /// <returns>A list of statin to print</returns>
        public IEnumerable<BaseStationToList> GetStations()
        {
            IEnumerable<DalApi.DO.Station> list = dal.GetStations();
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
            if (name.Equals(string.Empty) && chargeSlots == 0)
                throw new ArgumentNullException("You must enter all the details!");
            try
            {
                var station = dal.GetStation(id);
                dal.RemoveStation(station);
                dal.AddStation(id, name.Equals(string.Empty) ? station.Name : name, station.Longitude, station.Lattitude, chargeSlots == 0 ? station.ChargeSlots : chargeSlots);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (DalApi.DO.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {
                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
        }

    

        /// <summary>
        /// Convert a DAL station to BLStationToList satation
        /// </summary>
        /// <param name="station">The sation to convert</param>
        /// <returns>The converted station</returns>
        private BaseStationToList ConvertStationToStationForList(DalApi.DO.Station station)
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
