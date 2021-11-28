using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;

namespace DalObject
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
        public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots)
        {
            
            Station newStation = new Station();
            newStation.Id = id;
            newStation.Name = name;
            newStation.Lattitude = latitude;
            newStation.Longitude = longitude;
            newStation.ChargeSlots = chargeSlots;
            BaseStations.Add(newStation);
        }


        /// <summary>
        /// DisplayBaseStation is a method in the DalObject class.
        /// the method allows base station view
        /// </summary>
        public void DisplayBaseStation()
        {
            Console.WriteLine("enter base station id:");
            int input;
            ValidRange(0, BaseStations.Count, out input);
            Console.WriteLine(BaseStations[input - 1]);
        }

        /// <summary>
        /// ViewListBaseStations is a method in the DalObject class.
        /// the method displays a list of base stations
        /// </summary>
        public void ViewListBaseStations()
        {
            foreach (Station item in BaseStations)
            {
                Console.WriteLine(item);
            }
        }


        /// <summary>
        /// Find a satation that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested station/param>
        /// <returns>A station for display</returns>
        public Station GetStation(int id) => BaseStations.First(item => item.Id == id);

        /// <summary>
        ///  Prepares the list of Sations for display
        /// </summary>
        /// <returns>A list of stations</returns>
        public IEnumerable<Station> GetStations() => BaseStations;

        /// <summary>
        /// Find the satation that have empty charging slots
        /// </summary>
        /// <returns>A list of the requested station</returns>
        /// /// <summary>
        /// Checks which base Sations are available for charging
        /// </summary>
        /// <returns>A list of avaiable satations</returns>
        private List<Station> getAvailbleStations() => (BaseStations.FindAll(item => item.ChargeSlots > NotAvailableChargingPorts(item.Id)));
        public IEnumerable<Station> GetAvailableChargingStations() => getAvailbleStations().ToList();

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



    }
}
