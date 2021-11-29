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
        private IDal dal;
        public void AddDrone(int id, string model, Enums.WeightCategories MaxWeight, int stationId)
        {
            Drone newDrone = new Drone();
            Random random = new Random();
            int RandomNumber(int min, int max)
            {
                return random.Next(min, max);
            }
            newDrone.BatteryStatus = ;
            newDrone.DroneStatus = 0;
            newDrone.DroneLocation = GetDrone(stationId).DroneLocation;
            

        }

        public Drone GetDrone(int id)
        {
            DroneToList drone = drones.Find(d => d.DroneId == id);
            Deliverybytransfer parcelInDeliver = drone.DroneStatus == DroneStatuses.Delivery ?
                                              GetParcelInTransfer((int)drone.DeliveredParcelId) :
                                              null;
            return new Drone()
            {
                DroneId = drone.DroneId,
                BatteryStatus = drone.BatteryDrone,
                DroneLocation = new Location() { Lattitude = drone.DroneLocation.Lattitude, Longitude = drone.DroneLocation.Longitude },
                Weight = drone.DroneWeight,
                DroneModel = drone.ModelDrone,
                DroneStatus = drone.DroneStatus,
                DeliveryTransfer = parcelInDeliver,

            };
        }

        public IEnumerable<DroneToList> GetDrones() => drones;


        public void ReleaseDroneFromCharging(int id, float timeOfCharge)
        {
            throw new NotImplementedException();
        }


        public void SendDroneForCharge(int id)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.DroneId == id);
            if (droneToList == default) ;
                if (droneToList.DroneStatus != DroneStatuses.Available) ;
            IDAL.DO.Station station = ClosetStationThatPossible(dal.GetStations(), droneToList.Location, droneToList.BatteryDrone, out double minDistanc);
            if (station.Equals(default(IDAL.DO.Station))) ;
            drones.Remove(droneToList);
            droneToList.DroneStatus = DroneStatuses.Meintenence;
            droneToList.BatteryDrone -= minDistanc * Available;
            droneToList.Location = new Location() { Longitude = station.Longitude, Lattitude = station.Lattitude }; ;
            dal.AddDRoneCharge(id, station.Id);
            drones.Add(droneToList);



        }

        private IDAL.DO.Station ClosetStationThatPossible(IEnumerable<IDAL.DO.Station> stations, Location droneToListLocation, double BatteryStatus, out double minDistance)
        {
            IDAL.DO.Station station = CloseStation(stations, droneToListLocation);
            minDistance = Distance(droneToListLocation, new Location() { Longitude = station.Longitude, Lattitude = station.Latitude });
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

            if (!ExistsIDTaxCheck(dal.GetDrones(), id))
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

        private bool IsAbleToPassParcel(Drone drone, ParcelInTransfer parcel)
        {
            var neededBattery =Distance(drone.DroneLocation, parcel.CollectParcelLocation) * Available +
                              Distance(parcel.CollectParcelLocation, parcel.DeliveryDestination) * GetElectricity(parcel.Weight) +
                              Distance(parcel.DeliveryDestination, CloseStation(dal.GetAvailableChargingStations(),drone.DroneLocation) * Available;
            return drone.BatteryStatus >= neededBattery;
        }

        
       
    }


   
}
