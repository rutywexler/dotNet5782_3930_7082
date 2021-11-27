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









    }
}
