using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;

namespace Dal
{
    public partial class DalObject
    {
        /// <summary>
        /// FindChargeSlot is a static method in the DalObject class.
        /// the method finds charge slot to a drone
        /// </summary>
        /// <param name = "id" > int value</param>
        /// <returns>if succeed in finding charge slot to a drone</returns>
        //public static bool FindChargeSlot(int id)
        //{
        //    foreach (Station item in BaseStations)
        //    {
        //        int sum_chargeSlots = 0;
        //        foreach (DroneCharge item2 in DroneCharges)
        //        {
        //            if (item2.DroneId == item.Id)
        //                sum_chargeSlots++;
        //            if (item2.DroneId > item.Id)
        //                break;
        //        }
        //        if (sum_chargeSlots < item.ChargeSlots)
        //        {
        //            DroneCharges.Add(new DroneCharge(id, item.Id));
        //            DroneCharges.Sort();
        //            return true;
        //        }
        //    }
        //    return false;
        //}


        /// <summary>
        /// ViewListAvailableChargeSlots is a method in the DalObject class.
        /// the method  display base stations with available charging stations
        /// </summary>
        //public void ViewListAvailableChargeSlots()
        //{
        //    foreach (Station item in BaseStations)
        //    {
        //        int sum = 0;
        //        foreach (DroneCharge droneCharge in DroneCharges)
        //        {
        //            if (droneCharge.DroneId == item.Id)
        //                sum++;
        //            if (droneCharge.DroneId > item.Id)
        //                break;
        //        }
        //        if (sum < item.ChargeSlots)
        //            Console.WriteLine(item);
        //    }
        //}
        /// <summary>
        /// Finds all the drones that are charged at a particular station
        /// </summary>
        /// <param name="id">The id of particular station</param>
        /// <returns>A list of DroneCarge</returns>
        public List<int> GetDronechargingInStation(int id)
        {
            List<int> DronechargingInStation = new();
            foreach (var DroneCharge in DroneCharges)
            {
                if (DroneCharge.StationId == id)
                    DronechargingInStation.Add(DroneCharge.DroneId);
            }
            return DronechargingInStation;
        }

        /// <summary>
        /// Gets parameters and create new DroneCharge 
        /// </summary>
        /// <param name="droneId">The drone to add</param>
        /// <param name="stationId">The station to add the drone</param>
        public void AddDRoneCharge(int droneId, int stationId)
        {
            DroneCharges.Add(new DroneCharge() { DroneId = droneId, StationId = stationId,StartTime=DateTime.Now});
        }

        /// <summary>
        /// Find a drone that has tha same id and release him from charging
        /// </summary>
        /// <param name="id">The id of the requested drone</param>
        /// <returns>A drone for display</returns>
        public void ReleaseDroneFromRecharge(int droneId)
        {
            var droneCharge = DataSource.DroneCharges.First(charge => charge.DroneId == droneId);

            DataSource.DroneCharges.Remove(droneCharge);
        }

    }
}
