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
           /* if (!CheckIdIfExistInTheList(dal.GetDrones(), id))
                throw new KeyNotFoundException();*/
            return MapDrone(dal.GetDrone(id));
        }

        public IEnumerable<Drone> GetDrones()
        {
            throw new NotImplementedException();
        }

        public void ReleaseDroneFromCharging(int id, float timeOfCharge)
        {
            throw new NotImplementedException();
        }

        private BO.Drone MapDrone(IDAL.DO.Drone drone)
        {
            DroneToList droneToList = drones.Find(item => item.Id == drone.Id);
            return new Drone()
            {
                DroneId = drone.Id,
                DroneModel = drone.Model,
                Weight = (WeightCategories)drone.MaxWeight,
                DroneStatus = droneToList.DroneStatus,
                BattaryMode = droneToList.BatteryStatus,
                CurrentLocation = droneToList.CurrentLocation,
                Parcel = droneToList.ParcelId != null ? CreateParcelInTransfer((int)droneToList.ParcelId) : null
            };
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

                
                IDAL.DO.Station baseStations = dal.GetStation(id2);
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
            throw new NotImplementedException();
        }


        
    }
}
