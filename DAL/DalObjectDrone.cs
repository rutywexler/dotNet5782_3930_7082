using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;

namespace DAlObject
{
    public partial class DalObject
    {
        /// <summary>
        /// AddDrone is a method in the DalObject class.
        /// the method adds a new drone
        /// </summary>
        public void InsertDrone(Drone drone) => Drones.Add(drone);

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


    }
}
