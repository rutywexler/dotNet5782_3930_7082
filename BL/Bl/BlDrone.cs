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


namespace IBL
{
    public partial class BL : IblDrone
    {

        private const int NUM_OF_MINUTE_IN_HOUR = 60;
        private const int MIN_BATTERY = 20;
        private const int MAX_BATTERY = 40;

        public void AddDrone(Drone drone,int stationId)
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

        public Drone GetDrone(int id)
        {
            DroneToList drone = drones.Find(d => d.DroneId == id);
            ParcelInTransfer parcelInDeliver = drone.DroneStatus == DroneStatus.Delivery ?
                                              GetParcelInTransfer(drone.ParcelId) :
                                              null;
            return new Drone()
            {
                DroneId = drone.DroneId,
                BatteryStatus = drone.BatteryDrone,
                DroneLocation = new Location() { Lattitude = drone.Location.Lattitude, Longitude = drone.Location.Longitude },
                Weight = drone.DroneWeight,
                DroneModel = drone.ModelDrone,
                DroneStatus = drone.DroneStatus,
                DeliveryTransfer = parcelInDeliver,
            };
        }

        public IEnumerable<DroneToList> GetDrones() => drones;


        public void ReleaseDroneFromCharging(int id, float timeOfCharge)
        {
            DroneToList drone = drones.FirstOrDefault(item => item.DroneId == id);

            drone.BatteryDrone += DroneLoadingRate * timeOfCharge;
            drone.DroneStatus = DroneStatus.Available;

            dal.ReleaseDroneFromRecharge(drone.DroneId);
        }


        public void SendDroneForCharge(int id)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.DroneId == id);
            if (droneToList.DroneStatus != DroneStatus.Available)
            {
                throw new InValidActionException();
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
        public void UpdateDrone(int id, string name)
        {
            if (!ExistsIDCheck(dal.GetDrones(), id))
                throw new KeyNotFoundException();
            IDAL.DO.Drone droneDl = dal.GetDrone(id);
            if (name.Equals(default))
                throw new ArgumentNullException("For updating the name must be initialized ");
            dal.RemoveDrone(droneDl);
            dal.AddDrone(id, name, droneDl.MaxWeight);
            DroneToList droneToList = drones.Find(item => item.DroneId == id);
            drones.Remove(droneToList);
            droneToList.ModelDrone = name;
            drones.Add(droneToList);
        }

        private bool IsDroneCanTakeTheParcel(Drone drone, ParcelInTransfer parcel)
        {
            double electricity;
            double e = parcel.Weight switch
            {
                WeightCategories.Light => LightWeightCarrier,
                WeightCategories.Medium => MediumWeightBearing,
                WeightCategories.Heavy => CarryingHeavyWeight,
                _ => throw new NotImplementedException()
            };
            IDAL.DO.Station station;
            var neededBattery = 
            electricity = Distance(drone.DroneLocation, parcel.CollectParcelLocation) * Available +
                        Distance(parcel.CollectParcelLocation, parcel.DeliveryDestination) * e;
            station = ClosetStationThatPossible(dal.GetStations(), drone.DroneLocation, drone.BatteryStatus - electricity, out _);
            electricity += Distance(parcel.DeliveryDestination,
                         new Location() { Lattitude = station.Lattitude, Longitude = station.Longitude }) * Available;
            return drone.BatteryStatus >= neededBattery;
        }

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



    }


   
}
