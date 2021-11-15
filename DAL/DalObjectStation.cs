using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObjectStation
    {

        /// <summary>
        /// AddBaseStation is a method in the DalObject class.
        /// the method adds a new base station
        /// </summary>
        public void InsertStation(Stations station) => BaseStations.Add(station);

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
            foreach (Stations item in BaseStations)
            {
                Console.WriteLine(item);
            }
        }









    }
}
