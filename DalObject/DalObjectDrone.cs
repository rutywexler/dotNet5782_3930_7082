using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
using System.Runtime.CompilerServices;

namespace Dal
{
    public partial class DalObject
    {
        /// <summary>
        ///  Gets parameters and create new drone 
        /// </summary>
        /// <param name="model"> Grone's model</param>
        /// <param name="MaxWeight"> The max weight that the drone can swipe (light- 0,medium - 1,heavy - 2)</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
            if (ExistsIDCheck(DataSource.Drones, id))
                throw new excepti();
            Drone newDrone = new()
            {
                Id = id,
                Model = model,
                MaxWeight = MaxWeight
            };
            Drones.Add(newDrone);
        }

        /// <summary>
        /// DisplayDrone is a method in the DalObject class.
        /// the method allows drone display
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DisplayDrone()
        {
            Console.WriteLine("enter drone id:");
            int input = int.Parse(Console.ReadLine());
            Console.WriteLine(Drones[input - 1]);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone drone,string name)
        {
            Drones.Remove(drone);
            drone.Model = name;
            AddDrone(drone.Id, drone.Model, drone.MaxWeight);
        }




        /// <summary>
        /// RemoveDrone is a method in the DalObject class.
        /// the method remove a drone frpm the drone list.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDrone(Drone drone)
        {

            Drones.Remove(drone);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] GetPowerConsumptionByDrone()
        {
            return new double[] { Config.Available, Config.LightWeightCarrier, Config.MediumWeightBearing, Config.CarriesHeavyWeight, Config.DroneLoadingRate };
        }

        /// <summary>
        /// Find a drone that has tha same id as the parameter
        /// </summary>
        /// <param name="id">The id of the requested drone</param>
        /// <returns>A drone for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            Drone drone = DataSource.Drones.FirstOrDefault(item => item.Id == id);
            if (drone.Equals(default(Drone)))
                throw new KeyNotFoundException("This drone doesnt exist in the data!");
            return drone;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones() => Drones;




    }
}
