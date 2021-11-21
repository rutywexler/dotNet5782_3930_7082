using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IDAL;
using IBL;
using static BL.BO.Enums;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        private IDal dalObject;
        private List<DroneToList> drones;
        public BL()
        {
            dalObject = new DalObject.DalObject();
            drones = new List<DroneToList>();
            initializeDrones();
            //drones = dal.GetDrones();



        }
        public void initializeDrones()
        {
            foreach (var drone in dal.GetDrones())
            {
                drones.Add(new DroneToList
                {
                    IdDrone = drone.Id,
                    ModelDrone = drone.Model,
                    DroneWeight = (WeightCategories)drone.MaxWeight
                });
            }
            int electricityConsumption;//צריכת חשמל
            int SkimmerLoadingRate;//קצב טעינת רחפן
                                   //TODO : DeliveryId
            foreach (var drone in drones)
            {
                drone.PackageNumberIsTransferred = 0;
            }
            //TODO : Battery & Status
            foreach (var drone in drones)
            {
                drone.BatteryDrone = 1;
                drone.DroneStatus = DroneStatuses.;
            }

            foreach (var drone in drones)
            {
                drone.Location = findDroneLocation(drone);
            }






        }
}
    