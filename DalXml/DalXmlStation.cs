using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dal
{
    public partial class DalXml
    {
        private readonly string StationPath = "Stations.xml";
        private readonly string DroneChargePath = "DroneCharge.xml";


        /// <summary>
        ///  Gets parameters and create new station 
        /// </summary>
        /// <param name="name"> Station`s name</param>
        /// <param name="longitude">The position of the station in relation to the longitude </param>
        /// <param name="latitude">The position of the station in relation to the latitude</param>
        /// <param name="chargeSlots">Number of charging slots at the station</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            List<Station> stationList = XMLTools.LoadListFromXmlSerializer<Station>(StationPath);
            //if (Dal.DalObject.ExistsIDCheck(stationList, id))
            //    throw new Dal.Exception_ThereIsInTheListObjectWithTheSameValue();
            Station newStation = new();
            newStation.Id = id;
            newStation.Name = name;
            newStation.Lattitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            stationList.Add(newStation);
            XMLTools.SaveListToXmlSerializer(stationList, StationPath);
        }



        /// <summary>
        /// Find  satation that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested station/param>
        /// <returns>A station for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            Station station = XMLTools.LoadListFromXmlSerializer<Station>(StationPath).FirstOrDefault(item => item.Id == id);
            if (station.Equals(default(Station)))
                throw new KeyNotFoundException("There isnt suitable station in the data!");
            return station;
        }

        /// <summary>
        ///  Prepares the list of Sations for display
        /// </summary>
        /// <returns>A list of stations</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStations()
        {
            return XMLTools.LoadListFromXmlSerializer<Station>(StationPath);
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
        private IEnumerable<Station> getAvailbleStations(Func<Station, bool> predicate = null)
        {
            return predicate == null ?
                    XMLTools.LoadListFromXmlSerializer<Station>(StationPath)
                    : XMLTools.LoadListFromXmlSerializer<Station>(StationPath).Where(predicate);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetAvailableChargingStations() => getAvailbleStations(item => item.ChargeSlots > NotAvailableChargingPorts(item.Id));

        /// <summary>
        /// check how many station is not available charging 
        /// </summary>
        /// <param name="baseStationId">the ststion ID</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int NotAvailableChargingPorts(int baseStationId)
        {
            List<DroneCharge> DroneCharges = XMLTools.LoadListFromXmlSerializer<DroneCharge>(DroneChargePath);
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveStation(int id)
        {
            List<Station> stations = XMLTools.LoadListFromXmlSerializer<Station>(StationPath);
            Station basestation = stations.FirstOrDefault(station => station.Id == id);
            //stations.Remove(basestation);
            basestation.IsDeleted = true;
            //stations.Add(basestation);
            XMLTools.SaveListToXmlSerializer(stations, StationPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateSation(Station updateStation)
        {
            var stations = XMLTools.LoadListFromXmlSerializer<Station>(StationPath);
            Station basestation = stations.FirstOrDefault(station => station.Id == updateStation.Id);
            stations.Remove(basestation);
            XMLTools.SaveListToXmlSerializer(stations, StationPath);
            AddStation(updateStation.Id, updateStation.Name, updateStation.Longitude, updateStation.Lattitude, updateStation.ChargeSlots);
        }
    }
}

