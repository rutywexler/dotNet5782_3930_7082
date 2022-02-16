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
            // DroneCharges.Add(new DroneCharge() { DroneId = droneId, StationId = stationId,StartTime=DateTime.Now});
            if (DataSource.DroneCharges.Exists(dc => dc.DroneId == droneId))
                throw new Exception_ThereIsInTheListObjectWithTheSameValue("This drone is already being charged");
            DroneCharges.Add(new DroneCharge()
            {
                DroneId = droneId,
                StationId = stationId,
                StartTime = DateTime.Now
            });
           // BaseStationDroneIn(stationId);
        }

        private void BaseStationDroneIn(int baseStationId)
        {
            int index = Stations.FindIndex(bs => bs.Id == baseStationId);
            if (index == -1)
                throw new Exception_ThereIsInTheListObjectWithTheSameValue("This drone is already being charged");
            Station station = Stations[index];
            --station.ChargeSlots;
            Stations[index] = station;
        }


        /// <summary>
        /// Find a drone that has tha same id and release him from charging
        /// </summary>
        /// <param name="id">The id of the requested drone</param>
        /// <returns>A drone for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDroneFromRecharge(int droneId)
        {
            var droneCharge = DroneCharges.First(charge => charge.DroneId == droneId);

            DroneCharges.Remove(droneCharge);
           // BaseStationDroneOut(droneCharge.StationId);
        }

        public void BaseStationDroneOut(int baseStationId)
        {
            int index = DataSource.Stations.FindIndex(bs => bs.Id == baseStationId);
            if (index == -1)
                throw new Exception_ThereIsInTheListObjectWithTheSameValue("Base station does not exist");
            Station station = Stations[index];
            ++station.ChargeSlots;
            Stations[index] = station;
        }

    }
}
