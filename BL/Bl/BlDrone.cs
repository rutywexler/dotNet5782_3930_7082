using BL.BO;
using IBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using DalObject;
using IBL.BO;
using BL;
using System.Device.Location;
using static BL.BO.Enums;
using BL.Bl;
using System.ComponentModel;

namespace IBL
{
    public partial class BL : IblDrone
    {

        private const int NUM_OF_MINUTE_IN_HOUR = 60;
        private const int MIN_BATTERY = 20;
        private const int MAX_BATTERY = 40;

        /// <summary>
        /// the function add to the data the drone
        /// </summary>
        /// <param name="drone">the drone the user want to add</param>
        /// <param name="stationId">the statiion id in order to know the location to put the drone</param>
        public void AddDrone(Drone drone,int stationId)
        {
            try
            {

                dal.AddDrone(drone.DroneId, drone.DroneModel, (IDAL.DO.WeightCategories)drone.Weight);
                IDAL.DO.Station station = dal.GetStation(stationId);
                DroneToList droneToList = new()
                {
                    DroneId = drone.DroneId,
                    ModelDrone = drone.DroneModel,
                    DroneWeight = drone.Weight,
                    BatteryDrone = rand.NextDouble() + rand.Next(MIN_BATTERY, MAX_BATTERY),
                    DroneStatus = DroneStatus.Meintenence,
                    Location = new Location() { Lattitude = station.Lattitude, Longitude = station.Longitude }
                };
                drones.Add(droneToList);
            }
            catch (IDAL.DO.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {

                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException(ex.Message);
            }

        }

        /// <summary>
        /// the function returns the drone with the ID the function gets
        /// </summary>
        /// <param name="id">the id of the drone the user want to get</param>
        /// <returns></returns>
        public Drone GetDrone(int id)
        {
            try
            {
                DroneToList drone = drones.Find(d => d.DroneId == id);
                ParcelInTransfer parcelInDeliver = drone.DroneStatus == DroneStatus.Delivery ?
                                                  GetParcelInTransfer((int)drone.ParcelId) :
                                                  null;
                return new Drone()
                {
                    DroneId = drone.DroneId,
                    BatteryStatus = drone.BatteryDrone,
                    DroneLocation = drone.Location,
                    Weight = drone.DroneWeight,
                    DroneModel = drone.ModelDrone,
                    DroneStatus = drone.DroneStatus,
                    DeliveryTransfer = parcelInDeliver,
                };
            }
            catch (ArgumentNullException ex)
            {

                throw new ArgumentNullException(ex.Message);
            }
        }

        /// <summary>
        /// the function returns the drone list from the data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneToList> GetDrones() => drones;

        /// <summary>
        /// the function release the drone from charging
        /// </summary>
        /// <param name="id">the id of the drone the user wants to release</param>
        /// <param name="timeOfCharge">how many time takes the charging</param>
        public void ReleaseDroneFromCharging(int id, float timeOfCharge)
        {
            DroneToList drone = drones.FirstOrDefault(item => item.DroneId == id);
            if (drone == default)
                throw new ArgumentNullException("In the charching not exist drone with this ID:(");
            if (drone.DroneStatus != DroneStatus.Meintenence)
                throw new InvalidEnumArgumentException("becouse that the drone status is not maintence, its not possible to release the srone from charging ");

            drone.BatteryDrone += DroneLoadingRate * timeOfCharge;
            drone.DroneStatus = DroneStatus.Available;

            dal.ReleaseDroneFromRecharge(drone.DroneId);
        }

/// <summary>
/// the function send the drone with the id the function get to charge
/// </summary>
/// <param name="id">the id of the drone the user want to charge</param>
        public void SendDroneForCharge(int id)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.DroneId == id);
            if (droneToList.DroneStatus != DroneStatus.Available)
            {
                throw new InvalidEnumArgumentException("because the status drone isnt available, isnt possible to sent him for charge:(");
            }
            IDAL.DO.Station station = ClosetStationThatPossible(dal.GetStations(), droneToList.Location, droneToList.BatteryDrone, out double minDistanc);
            drones.Remove(droneToList);
            droneToList.DroneStatus = DroneStatus.Meintenence;
            station.ChargeSlots -= 1;
            droneToList.BatteryDrone -= minDistanc * Available;
            droneToList.Location = new Location() { Longitude = station.Longitude, Lattitude = station.Lattitude }; ;
            dal.AddDRoneCharge(id, station.Id);
            drones.Add(droneToList);
        }
/// <summary>
/// the function return the close station that possible
/// </summary>
/// <param name="stations">the list of the ststions</param>
/// <param name="droneToListLocation">the location of the drone</param>
/// <param name="BatteryStatus">the drone battery ststus</param>
/// <param name="minDistance">the min distabce</param>
/// <returns></returns>
        private IDAL.DO.Station ClosetStationThatPossible(IEnumerable<IDAL.DO.Station> stations, Location droneToListLocation, double BatteryStatus, out double minDistance)
        {
            IDAL.DO.Station station = CloseStation(stations, droneToListLocation);
            minDistance = Distance(droneToListLocation, new Location() { Longitude = station.Longitude, Lattitude = station.Lattitude });
            return minDistance * Available <= BatteryStatus ? station : default(IDAL.DO.Station);
        }


        private IDAL.DO.Station CloseStation(IEnumerable<IDAL.DO.Station> stations, Location location)
        {
            double minDistance = double.MaxValue;
            double curDistance;
            IDAL.DO.Station station = default;
            foreach (var item in stations)
            {
                curDistance = Distance(location,
                    new Location() { Lattitude = item.Lattitude, Longitude = item.Longitude });
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    station = item;
                }
            }
            return station;
        }

        /// <summary>
        /// the function update the drone with theid that was send
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void UpdateDrone(int id, string name)
        {
            if (ExistsIDCheck(dal.GetDrones(), id))
                throw new KeyNotFoundException();
            IDAL.DO.Drone droneDl = dal.GetDrone(id);
            if (name.Equals(default))
                throw new ArgumentNullException("For updating, you must enter the name! ");
            dal.RemoveDrone(droneDl);
            dal.AddDrone(id, name, droneDl.MaxWeight);
            DroneToList droneToList = drones.Find(item => item.DroneId == id);
            drones.Remove(droneToList);
            droneToList.ModelDrone = name;
            drones.Add(droneToList);
        }

        /// <summary>
        /// the function check if drone can take parcel
        /// </summary>
        /// <param name="drone">the drone the user want to check if its can take the parcel</param>
        /// <param name="parcel">the parcel the user want to check if the drone can take him</param>
        /// <returns></returns>
        private bool IsDroneCanTakeTheParcel(Drone drone, ParcelInTransfer parcel)
        {
            var electricityUse = ElectricityUse(drone.DroneLocation,drone.BatteryStatus, parcel);
            return drone.BatteryStatus >= electricityUse;
        }

        private double ElectricityUse(Location droneLocation,double battery, ParcelInTransfer parcel)
        {
            double e = parcel.Weight switch
            {
                WeightCategories.Light => LightWeightCarrier,
                WeightCategories.Medium => MediumWeightBearing,
                WeightCategories.Heavy => CarryingHeavyWeight,
                _ => throw new NotImplementedException()
            };
            double electricity;
            IDAL.DO.Station station;
            var electricityUse =
           electricity = Distance(droneLocation, parcel.CollectParcelLocation) * Available +
                       Distance(parcel.CollectParcelLocation, parcel.DeliveryDestination) * e;
            station = ClosetStationThatPossible(dal.GetStations(), droneLocation, battery - electricity, out _);
            electricity += Distance(parcel.DeliveryDestination,
                         new Location() { Lattitude = station.Lattitude, Longitude = station.Longitude }) * Available;
            return electricityUse;
        }
        /// <summary>
        /// made the changes between drone to drone to list
        /// </summary>
        /// <param name="droneId"> the id of the drone that needs to change to drone to list</param>
        /// <returns></returns>

        private List<DroneInCharging> ConvertDroneToDroneToList(int droneId)
        {
            List<int> listDronechargingInStation = dal.GetDronechargingInStation(droneId);
            if (listDronechargingInStation.Count == 0)
                return new();
            List<DroneInCharging> droneInChargings = new();
            DroneToList droneToList;
            foreach (var idDrone in listDronechargingInStation)
            {
                droneToList = drones.FirstOrDefault(item => (item.DroneId == idDrone));
                if (droneToList != default)
                {
                    droneInChargings.Add(new DroneInCharging() { ID = idDrone, BatteryStatus = droneToList.BatteryDrone });
                }
            }
            return droneInChargings;
        }

        /// <summary>
        /// Collect the parcel by drone
        /// </summary>
        /// <param name="parcelId">The parcel to update</param>
        private void colloctDalParcel(int parcelId)
        {
            try
            {
                IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
                dal.RemoveParcel(parcel);
                parcel.PickedUp = DateTime.Now;
                dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority, parcel.Id, parcel.DroneId, parcel.Requested, parcel.Scheduled, (DateTime)parcel.PickedUp, (DateTime)parcel.Delivered);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (IDAL.DO.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {
                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }

        }



    }


   
}
