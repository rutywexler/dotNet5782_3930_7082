using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;

namespace DALObject
{
    public partial class DalObjectDroneCharge
    {
        /// <summary>
        /// FindChargeSlot is a static method in the DalObject class.
        /// the method finds charge slot to a drone
        /// </summary>
        /// <param name="id">int value</param>
        /// <returns>if succeed in finding charge slot to a drone</returns>
        public static bool FindChargeSlot(int id)
        {
            foreach (Stations item in BaseStations)
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

        /// <summary>
        /// UpdateCharge is a method in the DalObject class.
        /// the method updates sending a skimmer for charging at a base station
        /// </summary>
        /// <param name="id">int value</param>
        /// <returns>if succeed in finding available charge slot</returns>
        public bool UpdateCharge(int id)
        {
            checkValid(id, 1, Drones.Count + 1);
            Drone tempDrone = Drones[id];
            tempDrone.Status = (DroneStatuses)1;
            return FindChargeSlot(id);
        }

        /// <summary>
        /// ViewListAvailableChargeSlots is a method in the DalObject class.
        /// the method  display base stations with available charging stations
        /// </summary>
        public void ViewListAvailableChargeSlots()
        {
            foreach (Stations item in BaseStations)
            {
                int sum = 0;
                foreach (DroneCharge droneCharge in DroneCharges)
                {
                    if (droneCharge.DroneId == item.Id)
                        sum++;
                    if (droneCharge.DroneId > item.Id)
                        break;
                }
                if (sum < item.ChargeSlots)
                    Console.WriteLine(item);
            }
        }
    }
}
