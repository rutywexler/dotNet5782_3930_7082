using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace Dal
{
    public partial class DalXml
    {
        private readonly string droneChargesPath = "DroneCharges.xml";

        /// <summary>
        /// Finds all the drones that are charged at a particular station
        /// </summary>
        /// <param name="id">The id of particular station</param>
        /// <returns>A list of DroneCarge</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> GetDronechargingInStation(int id)
        {
            List<DroneCharge> droneCharges = XMLTools.LoadListFromXmlSerializer<DroneCharge>(droneChargesPath);
            List<int> DronechargingInStation = new();
            foreach (var DroneCharge in droneCharges)
            {
                if (DroneCharge.StationId == id)
                    DronechargingInStation.Add(DroneCharge.DroneId);
            }
            return DronechargingInStation;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneCharging(Predicate<DroneCharge> predicate)
        {
            return XMLTools.LoadListFromXmlSerializer<DroneCharge>(droneChargesPath)
                .Where(s =>predicate(s));
        }

        /// <summary>
        /// Gets parameters and create new DroneCharge 
        /// </summary>
        /// <param name="droneId">The drone to add</param>
        /// <param name="stationId">The station to add the drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDRoneCharge(int droneId, int stationId)
        {
            List<DroneCharge> droneCharges = XMLTools.LoadListFromXmlSerializer<DroneCharge>(droneChargesPath);
            droneCharges.Add(new DroneCharge() { DroneId = droneId, StationId = stationId, StartTime = DateTime.Now });
            XMLTools.SaveListToXmlSerializer(droneCharges, droneChargesPath);
        }

        /// <summary>
        /// Find a drone that has tha same id and release him from charging
        /// </summary>
        /// <param name="id">The id of the requested drone</param>
        /// <returns>A drone for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDroneFromRecharge(int droneId)
        {
            List<DroneCharge> droneCharges = XMLTools.LoadListFromXmlSerializer<DroneCharge>(droneChargesPath);
            var droneCharge = droneCharges.First(charge => charge.DroneId == droneId);
            droneCharges.Remove(droneCharge);
            XMLTools.SaveListToXmlSerializer(droneCharges, droneChargesPath);
        }

    }
}


