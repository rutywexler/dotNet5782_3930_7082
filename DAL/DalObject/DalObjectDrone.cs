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
        ///  Gets parameters and create new drone 
        /// </summary>
        /// <param name="model"> Grone's model</param>
        /// <param name="MaxWeight"> The max weight that the drone can swipe (light- 0,medium - 1,heavy - 2)</param>
        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
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
        public void DisplayDrone()
        {
            Console.WriteLine("enter drone id:");
            int input;
            ValidRange(1, Drones.Count + 1, out input);
            Console.WriteLine(Drones[input - 1]);
        }


        /// <summary>
        /// ViewListDrones is a method in the DalObject class.
        /// the method displays the list of drones
        /// </summary>
        public void ViewListDrones()
        {
            foreach (Drone item in Drones)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// UpdatePickedUp is a method in the DalObject class.
        /// the method updates package assembly by drone
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdatePickedUp(int id)
        {
            checkValid(id, 0, Parcels.Count);
            Parcel tempParcel = Parcels[id];
            tempParcel.PickedUp = DateTime.Now;
            Parcels[id] = tempParcel;
            Drone droneTemp = Drones[Parcels[id].DroneId - 1];
            droneTemp.Status = (DroneStatuses)2;
            Drones[Parcels[id].DroneId - 1] = droneTemp;
        }

        /// <summary>
        /// UpdateSupply is a method in the DalObject class.
        /// the method updates delivery of a package to the destination
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateSupply(int id)
        {
            checkValid(id, 0, Parcels.Count);
            Parcel tempParcel = Parcels[id];
            tempParcel.Delivered = DateTime.Now;
            Parcels[id] = tempParcel;
            Drone tempDrone = Drones[Parcels[id].DroneId - 1];
            tempDrone.Status = 0;
        }

        /// <summary>
        /// UpdateRelease is a method in the DalObject class.
        /// the method removes a drone from charging at a base station
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateRelease(int id)
        {
            checkValid(id, 1, Drones.Count + 1);
            Drone tempDrone = Drones[id - 1];
            tempDrone.Status = 0;
            int sum = -1;
            foreach (DroneCharge item in DroneCharges)
            {
                sum++;
                if (item.DroneId == id)
                {
                    break;
                }
            }
            DroneCharges.RemoveRange(sum, 1);
        }
        /// <summary>
        /// RemoveDrone is a method in the DalObject class.
        /// the method remove a drone frpm the drone list.
        /// </summary>
        public void RemoveDrone(Drone drone)
        {
            
            Drones.Remove(drone);
        }

        public double[] GetPowerConsumptionByDrone()
        {
            return new double[] { Config.Available,Config.LightWeightCarrier, Config.MediumWeightBearing, Config.CarryingHeavyWeight,Config.DroneLoadingRate };
        }


    }
}
