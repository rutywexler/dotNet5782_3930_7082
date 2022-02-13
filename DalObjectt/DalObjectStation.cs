﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;

namespace Dal
{
    public partial class DalObject
    {

        /// <summary>
        ///  Gets parameters and create new station 
        /// </summary>
        /// <param name="name"> Station`s name</param>
        /// <param name="longitude">The position of the station in relation to the longitude </param>
        /// <param name="latitude">The position of the station in relation to the latitude</param>
        /// <param name="chargeSlots">Number of charging slots at the station</param>
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            if (ExistsIDCheck(DataSource.Stations, id))
                throw new Exception_ThereIsInTheListObjectWithTheSameValue();
            Station newStation = new();
            newStation.Id = id;
            newStation.Name = name;
            newStation.Lattitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            Stations.Add(newStation);
        }



        /// <summary>
        /// Find  satation that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested station/param>
        /// <returns>A station for display</returns>
        public Station GetStation(int id)
        {
            Station station = Stations.FirstOrDefault(item => item.Id == id);
            if (station.Equals(default(Station)))
                throw new KeyNotFoundException("There isnt suitable Station in the data!");
            return station;
        }

        /// <summary>
        ///  Prepares the list of Sations for display
        /// </summary>
        /// <returns>A list of stations</returns>
        public IEnumerable<Station> GetStations()
        {
            return Stations.Where(station => station.IsDeleted == false);
        }

        /// <summary>
        /// Find the satation that have empty charging slots
        /// </summary>
        /// <returns>A list of the requested station</returns>
        /// /// <summary>
        /// Checks which base Sations are available for charging
        /// </summary>
        /// <returns>A list of avaiable satations</returns>
        //private List<Station> getAvailbleStations(Predicate<Station> predicate) => (BaseStations.FindAll(item => item.ChargeSlots > NotAvailableChargingPorts(item.Id)));
        private List<Station> getAvailbleStations(Predicate<Station> predicate) => (Stations.FindAll(predicate));
        public IEnumerable<Station> GetAvailableChargingStations() => getAvailbleStations(item => item.ChargeSlots > NotAvailableChargingPorts(item.Id)).ToList();

        /// <summary>
        /// check how many station is not available charging 
        /// </summary>
        /// <param name="baseStationId">the ststion ID</param>
        /// <returns></returns>
        public int NotAvailableChargingPorts(int baseStationId)
        {
            int count = 0;
            foreach (DroneCharge item in DroneCharges)
            {
                if (item.StationId == baseStationId)
                    ++count;
            }
            return count;
        }
        /// <summary>
        /// remove station from ststion list
        /// </summary>
        /// <param name="customer">the station i want to delete</param>
        public void RemoveStation(int id)
        {
            Station station = Stations.FirstOrDefault(station => station.Id == id);
            Stations.Remove(station);
            station.IsDeleted = true;
            Stations.Add(station);
        }

        public void UpdateSation(Station station)
        {
            var s = Stations.FirstOrDefault(item => item.Id == station.Id);
            if (station.Equals(default(Station)))
                throw new KeyNotFoundException("There isnt suitable Station in the data!");
            Stations.Remove(s);
            AddStation(station.Id, station.Name, station.Longitude, station.Lattitude, station.ChargeSlots);
        }
    }
}
