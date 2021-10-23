using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;
using static DalObject.DataSource.Config;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            Initalize();  
        }
        public  void InsertStation(Stations station)
        {
            DataSource.BaseStations[DataSource.Config.NumOfBaseStations++] = station;
        }

        public  void InsertDrone(Drone drone)
        {
            DataSource.Drones[DataSource.Config.NumOfDrons++] = drone;
        }

        public  void InsertParcel(Parcel parcel)
        {
            DataSource.Parcels[DataSource.Config.NumOfParcels] = parcel;
        }

        public  void InsertCustomer(Customer customer)
        {
            DataSource.Customers[DataSource.Config.NumOfCustomers] = customer;
        }
        /// <summary>
        /// UpdateScheduled is a method in the DalObject class.
        /// the method assigns a package to the drone
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateScheduled(int id)
        {
            checkValid(id, 0, NumOfParcels);
            for (int i = 0; i < Drones.Length; i++)
            {
                if ((Drones[i].MaxWeight >= Parcels[NumOfParcels].Weight) && (Drones[i].Battery >= 30) && Drones[i].Status == 0)
                {
                    Parcels[id].DroneId = Drones[i].Id;
                    Parcels[id].Scheduled = DateTime.Now;
                    return;
                }
            }
        }
        /// <summary>
        /// UpdatePickedUp is a method in the DalObject class.
        /// the method updates package assembly by drone
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdatePickedUp(int id)
        {
            checkValid(id, 0, NumOfParcels);
            Parcels[id].PickedUp = DateTime.Now;
            Drones[Parcels[id].DroneId - 1].Status = (DroneStatuses)2;
        }
        /// <summary>
        /// UpdateSupply is a method in the DalObject class.
        /// the method updates delivery of a package to the destination
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateSupply(int id)
        {
            checkValid(id, 0, NumOfParcels);
            Parcels[id].Delivered = DateTime.Now;
            Drones[Parcels[id].DroneId - 1].Status = (DroneStatuses)0;
        }
        /// <summary>
        /// UpdateCharge is a method in the DalObject class.
        /// the method updates sending a skimmer for charging at a base station
        /// </summary>
        /// <param name="id">int value</param>
        /// <returns>if succeed in finding available charge slot</returns>
        public bool UpdateCharge(int id)
        {
            checkValid(id, 1, NumOfDrons + 1);
            Drones[id].Status = (DroneStatuses)1;
            return FindChargeSlot(id);
        }
        /// <summary>
        /// UpdateRelease is a method in the DalObject class.
        /// the method removes a drone from charging at a base station
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateRelease(int id)
        {
            checkValid(id, 1, NumOfDrons + 1);
            Drones[id - 1].Status = 0;
            int sum = -1;
            foreach (DroneCharge item in )
            {
                sum++;
                if (item.DroneId == id)
                {
                    break;
                }
            }
            BaseStations.RemoveRange(sum, 1);
        }

        /// <summary>
        /// checkValid is a static method in the DalObject class.
        /// the helper method check if input is valid if not the func throws exception 
        /// </summary>
        /// <param name="id">the first int value</param>
        /// <param name="min">the second int value</param>
        /// <param name="max">3th out int value</param>
        public void checkValid(int id, int min, int max)
        {
            if (id < min || id >= max)
            {
                throw new FormatException();
            }
        }

        /// <summary>
        /// FindChargeSlot is a static method in the DalObject class.
        /// the method finds charge slot to a drone
        /// </summary>
        /// <param name="id">int value</param>
        /// <returns>if succeed in finding charge slot to a drone</returns>
        public static bool FindChargeSlot(int id)
        {
            foreach (BaseStation item in BaseStations)
            {
                int sum_chargeSlots = 0;
                foreach (DroneCharge item2 in DroneCharges)
                {
                    if (item2.DroneId == item.Id)
                        sum_chargeSlots++;
                    if (item2.DroneId > item.Id)
                        break;
                }
                if (sum_chargeSlots < item.ChargeSlots)
                {
                    DroneCharges.Add(new DroneCharge(id, item.Id));
                    DroneCharges.Sort();
                    return true;
                }
            }
            return false;
        }
    }

}
}
