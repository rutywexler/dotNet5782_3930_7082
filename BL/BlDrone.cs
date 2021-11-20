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
            Drone g = new Drone();
            return g;
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
        public Location FindCloseLocation()
        {
            var sCoord = new GeoCoordinate(sLocation.Latitude, sLocation.Longitud
            var tCoord = new GeoCoordinate(tLocation.Latitude, tLocation.Longitude);
            double distance = sCoord.GetDistanceTo(tCoord);
        }
        public void UpdateDrone(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
