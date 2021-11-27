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
                                              GetParcelInDeliver((int)drone.DeliveredParcelId) :
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

        public IEnumerable<Drone> GetDrones()
        {
            throw new NotImplementedException();
        }

        public void ReleaseDroneFromCharging(int id, float timeOfCharge)
        {
            throw new NotImplementedException();
        }


        public void SendDroneForCharge(int id)
        {
            Drone newDrone = new Drone();
            //צריך למצוא מיקום קרוב ביותר!!
            BaseStation baseStation = new BaseStation();
            newDrone = GetDrone(id);
            if (newDrone.DroneStatus == 1/*||newDrone.DroneLocation.Lattitude*/)
            {
                int id2;//idשל התחנה הקרובה ביותר
                newDrone.DroneLocation = baseStation.location;
                /*■	הורדת מספר עמדות טעינה פנויות ב-1IDal.DO.*/
                Drone drone = new Drone();

                
                IDAL.DO.Stations baseStations = dal.GetStation(id2);
                baseStations.ChargeSlots -= 1;
                IDAL.DO.Drone DalDrone = dal.GetDrone(id);
                DalDrone.Battery = ;//לתקן!!
                DalDrone.









            }

        }
       /* public double FindCloseLocation(Location sLocation , Location tLocation)
        {
            var sCoord = new GeoCoordinate(sLocation.Lattitude, sLocation.Longitude);
            var tCoord = new GeoCoordinate(tLocation.Lattitude, tLocation.Longitude);
            double distance = sCoord.GetDistanceTo(tCoord);
            return distance;
        }*/
        public void UpdateDrone(int id, string name)
        {

            if (!ExistsIDTaxCheck(dal.GetDrones(), id))
                throw new KeyNotFoundException();
            IDAL.DO.Drone droneDl = dal.GetDrone(id);
            if (name.Equals(default))
                throw new ArgumentNullException("For updating the name must be initialized ");
            dal.RemoveDrone(droneDl);
            dal.AddDrone(id, name, droneDl.MaxWeight);
            DroneToList droneToList = drones.Find(item => item.Id == id);
            drones.Remove(droneToList);
            droneToList.ModelDrone = name;
            drones.Add(droneToList);
        }


        
    }
}
