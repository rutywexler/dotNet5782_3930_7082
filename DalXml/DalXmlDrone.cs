using Dal;
using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    public partial class DalXml
    {
        private readonly string dronesPath = "Drones.xml";



        /// <summary>
        ///  Gets parameters and create new drone 
        /// </summary>
        /// <param name="model"> Grone's model</param>
        /// <param name="MaxWeight"> The max weight that the drone can swipe (light- 0,medium - 1,heavy - 2)</param>
        public void AddDrone(int id, string model, DO.WeightCategories MaxWeight)
        { 
            List<Drone> drones = XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath);
            //if (Dal.DalObject.ExistsIDCheck(drones, id))
            //    throw new Exception_ThereIsInTheListObjectWithTheSameValue();
            Drone newDrone = new()
            {
                Id = id,
                Model = model,
                MaxWeight = MaxWeight
            };
            drones.Add(newDrone);
            XMLTools.SaveListToXmlSerializer(drones, dronesPath);
        }

        /// <summary>
        /// RemoveDrone is a method in the DalObject class.
        /// the method remove a drone frpm the drone list.
        /// </summary>
        public void RemoveDrone(Drone drone)
        {
            List<Drone> drones = XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath);
            drones.Remove(drone);
            XMLTools.SaveListToXmlSerializer(drones, dronesPath);
        }
        public void UpdateDrone(Drone updateDrone)
        {
            var drones = XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath);
            Drone drone = drones.FirstOrDefault(d => d.Id == updateDrone.Id);
            drones.Remove(drone);
            XMLTools.SaveListToXmlSerializer(drones, StationPath);
            AddDrone(updateDrone.Id, updateDrone.Model, updateDrone.MaxWeight);
        }


        public double[] GetPowerConsumptionByDrone()
        {
            return new double[] { 1, 2, 3, 4, 5 };
            try
            {
                XElement config = LoadConfigToXML(ConfigPath);
                var electricity = config.Elements().Select(elem => double.Parse(elem.Value));
                return new double[] { electricity.ElementAt(1), electricity.ElementAt(2), electricity.ElementAt(3), electricity.ElementAt(4), electricity.ElementAt(5) };
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw new XMLFileLoadCreateException(ex.Message);
            }
        }

        /// <summary>
        /// Find a drone that has tha same id as the parameter
        /// </summary>
        /// <param name="id">The id of the requested drone</param>
        /// <returns>A drone for display</returns>
        public Drone GetDrone(int id)
        {
            Drone drone = XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath).FirstOrDefault(item => item.Id == id);
            if (drone.Equals(default(Drone)))
                throw new KeyNotFoundException("This drone doesnt exist in the data!");
            return drone;
        }

        public IEnumerable<Drone> GetDrones() => XMLTools.LoadListFromXmlSerializer<Drone>(dronesPath);



    }
}
