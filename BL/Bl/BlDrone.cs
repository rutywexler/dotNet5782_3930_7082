using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using BlApi;
using BO;
using static BO.Enums;
using BL;

namespace Bl
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
        public void AddDrone(int id, WeightCategories MaxWeight, string Model, int stationId)
        {
            try
            {
                Drone drone = new() { DroneId = id, Weight = (WeightCategories)MaxWeight, DroneModel = Model };
                dal.AddDrone(drone.DroneId, drone.DroneModel, (DO.WeightCategories)drone.Weight);
                DO.Station station = dal.GetStation(stationId);
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
            catch (Exception_ThereIsInTheListObjectWithTheSameValue ex)
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
        public IEnumerable<DroneToList> GetDrones() => drones.ToList();

        /// <summary>
        /// the function release the drone from charging
        /// </summary>
        /// <param name="id">the id of the drone the user wants to release</param>
        /// 
        public void ReleaseDroneFromCharging(int id)
        {
            DroneToList drone = drones.FirstOrDefault(item => item.DroneId == id);
            if (drone == default)
                throw new ArgumentNullException("In the charching not exist drone with this ID:(");
            if (drone.DroneStatus != DroneStatus.Meintenence)
                throw new InvalidEnumArgumentException("because that the drone status is not maintence, its not possible to release the srone from charging ");

            var timeOfCharge = DateTime.Now - dal.GetDroneCharging(d => d.DroneId == id).FirstOrDefault().StartTime;
            drone.BatteryDrone += DroneLoadingRate * timeOfCharge.TotalMinutes;
            drone.DroneStatus = DroneStatus.Available;


            dal.ReleaseDroneFromRecharge(drone.DroneId);
        }

        /// <summary>
        /// the function send the drone with the id the function get to charge
        /// </summary>
        /// <param name="id">the id of the drone the user want to charge</param>
        public void SendDroneForCharge(int id)
        {
            DroneToList droneToList = drones.Find(item => item.DroneId == id);
            if (droneToList.DroneStatus != DroneStatus.Available)
            {
                throw new InvalidEnumArgumentException("because the status drone isnt available, isnt possible to sent him for charge:(");
            }
            BO.BaseStation station = ClosetStationThatPossible(droneToList.Location, droneToList.BatteryDrone, out double minDistanc);
            drones.Remove(droneToList);
            droneToList.DroneStatus = DroneStatus.Meintenence;
            station.NumberOfChargingStations -= 1;
            droneToList.BatteryDrone -= minDistanc * Available;
            droneToList.Location = new Location() { Longitude = station.Location.Longitude, Lattitude = station.Location.Lattitude }; ;
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
        internal BO.BaseStation ClosetStationThatPossible(Location droneToListLocation, double BatteryStatus, out double minDistance)
        {
            BO.BaseStation station = CloseStation(droneToListLocation);
            minDistance = LocationExtensions.Distance(droneToListLocation, new Location() { Longitude = station.Location.Longitude, Lattitude = station.Location.Lattitude });
            return minDistance * Available <= BatteryStatus ? station : default(BO.BaseStation);
        }


        private BO.BaseStation CloseStation(Location location)
        {
            double minDistance = double.MaxValue;
            double curDistance;
            BO.BaseStation station = default;
            foreach (var item in dal.GetStations())
            {
                curDistance = LocationExtensions.Distance(location,
                    new Location() { Lattitude = item.Lattitude, Longitude = item.Longitude });
                if (curDistance < minDistance)
                {
                    minDistance = curDistance;
                    station = convert(item);
                }
            }


            return station;
        }

        private BaseStation convert(DO.Station station)
        {
            return new BaseStation()
            {
                Id = station.Id,
                Name = station.Name,
                Location = new Location() { Lattitude = station.Lattitude, Longitude = station.Longitude },
                NumberOfChargingStations = station.ChargeSlots,
                DronesInCharge = GetDroneInStation(station.Id)
            };
        }

        private List<DroneInCharging> GetDroneInStation(int id)
        {
            // איך עושים????
            IEnumerable<DO.DroneCharge> list;
            lock (dal)
                list = dal.GetDroneCharging((stationIdOfDrone) => stationIdOfDrone.StationId == id);
            if (list.Count() == 0)
                return new List<DroneInCharging>();
            List<DroneInCharging> droneInChargings = new();
            DroneToList droneToList;
            foreach (var droneCharge in list)
            {
                droneToList = drones.FirstOrDefault(item => (item.DroneId == droneCharge.DroneId));
                if (droneToList != default)
                {
                    droneInChargings.Add(new DroneInCharging() { ID = droneCharge.DroneId, BatteryStatus = droneToList.BatteryDrone });
                }
            }
            return droneInChargings;
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
            DO.Drone droneDl = dal.GetDrone(id);
            if (name.Equals(default))
                throw new ArgumentNullException("For updating, you must enter the name! ");
            dal.UpdateDrone(droneDl);

            //dal.RemoveDrone(droneDl);
            //dal.AddDrone(id, name, droneDl.MaxWeight);
            //DroneToList droneToList = drones.Find(item => item.DroneId == id);
            //drones.Remove(droneToList);
            //droneToList.ModelDrone = name;
            //drones.Add(droneToList);
        }

        /// <summary>
        /// the function check if drone can take parcel
        /// </summary>
        /// <param name="drone">the drone the user want to check if its can take the parcel</param>
        /// <param name="parcel">the parcel the user want to check if the drone can take him</param>
        /// <returns></returns>
        private bool IsDroneCanTakeTheParcel(Drone drone, ParcelInTransfer parcel)
        {
            var electricityUse = ElectricityUse(drone.DroneLocation, drone.BatteryStatus, parcel);
            return drone.BatteryStatus >= electricityUse;
        }

        private double ElectricityUse(Location droneLocation, double battery, ParcelInTransfer parcel)
        {
            double e = parcel.Weight switch
            {
                WeightCategories.Light => LightWeightCarrier,
                WeightCategories.Medium => MediumWeightBearing,
                WeightCategories.Heavy => CarryingHeavyWeight,
                _ => throw new NotImplementedException()
            };
            double electricity;
            var electricityUse =
           electricity = LocationExtensions.Distance(droneLocation, parcel.CollectParcelLocation) * Available +
                       LocationExtensions.Distance(parcel.CollectParcelLocation, parcel.DeliveryDestination) * e;
            var station = ClosetStationThatPossible(droneLocation, battery - electricity, out _);
            electricity += LocationExtensions.Distance(parcel.DeliveryDestination,
                         new Location() { Lattitude = station.Location.Lattitude, Longitude = station.Location.Longitude }) * Available;
            return electricityUse;
        }
        /// <summary>
        /// made the changes between drone to drone to list
        /// </summary>
        /// <param name="droneId"> the id of the drone that needs to change to drone to list</param>
        /// <returns></returns>

        private List<DroneInCharging> ConvertDroneToDroneToDroneInCharging(int droneId)
        {
            var listDronechargingInStation = dal.GetDronechargingInStation(droneId);
            if (listDronechargingInStation.Count() == 0)
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
                DO.Parcel parcel = dal.GetParcel(parcelId);
                dal.RemoveParcel(parcel.Id);
                parcel.PickedUp = DateTime.Now;
                parcel.Delivered = DateTime.Now;
                dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority, parcel.Id, parcel.DroneId, parcel.Requested, parcel.Scheduled, (DateTime)parcel.PickedUp, (DateTime)parcel.Delivered);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Dal.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {
                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }

        }

        public IEnumerable<DroneToList> GetSomeDronesByStatus(DroneStatus droneStatus)
        {
            return ((List<DroneToList>)GetDrones()).FindAll(item => item.DroneStatus == droneStatus);
        }

        public IEnumerable<DroneToList> GetSomeDronesByWeight(WeightCategories weightCategories)
        {
            return ((List<DroneToList>)GetDrones()).FindAll(item => item.DroneWeight == weightCategories);
        }
        public void StartSimulator(Action update , int id, Func<bool> stop) => new DroneSimulator(id,this,update,stop );

      
    }
}






