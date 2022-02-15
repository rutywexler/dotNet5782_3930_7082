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
        /// Finds all the drones that are charged at a particular station
        /// </summary>
        /// <param name="id">The id of particular station</param>
        /// <returns>A list of DroneCarge</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<int> GetDronechargingInStation(int id)
        {
            List<int> DronechargingInStation = new();
            foreach (var DroneCharge in DroneCharges)
            {
                if (DroneCharge.StationId == id)
                    DronechargingInStation.Add(DroneCharge.DroneId);
            }
            return DronechargingInStation;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneCharging(Predicate<DroneCharge> predicate)
        {
            return DroneCharges.Where(d => predicate(d));
        }
        /// <summary>
        /// Gets parameters and create new DroneCharge 
        /// </summary>
        /// <param name="droneId">The drone to add</param>
        /// <param name="stationId">The station to add the drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDRoneCharge(int droneId, int stationId)
        {
            DroneCharges.Add(new DroneCharge() { DroneId = droneId, StationId = stationId,StartTime=DateTime.Now});
        }

        /// <summary>
        /// Find a drone that has tha same id and release him from charging
        /// </summary>
        /// <param name="id">The id of the requested drone</param>
        /// <returns>A drone for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDroneFromRecharge(int droneId)
        {
            var droneCharge = DataSource.DroneCharges.First(charge => charge.DroneId == droneId);

            DataSource.DroneCharges.Remove(droneCharge);
        }

    }
}
