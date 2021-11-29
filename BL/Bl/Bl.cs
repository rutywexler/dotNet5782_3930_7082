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
using System.Device.Location;


namespace IBL
{
    public partial class BL : IBL
    {
        private static readonly Random rand = new();
        private IDal dalObject;
        private List<DroneToList> drones;
        public double Available { get; set; }
        public double LightWeightCarrier { get; set; }
        public double MediumWeightBearing { get; set; }
        public double CarryingHeavyWeight { get; set; }
        public static double DroneLoadingRate { get; set; }
        public BL()
        {
            dalObject = new DalObject.DalObject();
            double[] arr;
            arr = dal.GetPowerConsumptionByDrone();
            Available = arr[0];
            LightWeightCarrier = arr[1];
            MediumWeightBearing = arr[2];
            CarryingHeavyWeight = arr[3];
            DroneLoadingRate = arr[4];
        }
        public void initializeDrones()
        {
            foreach (var drone in dal.GetDrones())
            {
                drones.Add(new DroneToList
                {
                    DroneId = drone.Id,
                    ModelDrone = drone.Model,
                    DroneWeight = (WeightCategories)drone.MaxWeight
                });
            }

            var parcels = dal.GetParcels().ToList();

            foreach (var drone in drones)
            {
                drone.ParcelId = 0;
            }
            foreach (var drone in drones)
            {
                drone.BatteryDrone = 1;
                if (drone.DroneStatus = ) ;
            }

            foreach (var drone in drones)
            {
                if (drone.DroneStatus != Enums.DroneStatus.Delivery)
                {
                    drone.DroneStatus = (DroneStatus)rand.Next(0, 2);
                    if (drone.DroneStatus == Enums.DroneStatus.Meintenence)
                    {
                        IDAL.DO.Station station = dal.GetStations().ToList()[rand.Next(0, dal.GetStations().ToList().Count)];
                        drone.Location = new Location()
                        {
                            Lattitude = station.Lattitude,
                            Longitude = station.Longitude

                        };
                        drone.BatteryDrone = rand.Next(0, 20) + rand.NextDouble();
                    }

                    if (drone.DroneStatus == Enums.DroneStatus.Available)
                    {

                    }


                }
            }
        }
        private static double Distance(Location sLocation, Location tLocation)
        {
            var sCoord = new GeoCoordinate(sLocation.Lattitude, sLocation.Longitude);
            var tCoord = new GeoCoordinate(tLocation.Lattitude, tLocation.Longitude);
            return sCoord.GetDistanceTo(tCoord);
        }
        public void AddOneDrone(int id, string model, WeightCategories maxWeight, int stationId)
        {
            var station = GetStation(stationId);

            var drone = new Drone()
            {
                DroneId = id,
                DroneModel = model,
                Weight = maxWeight,
                BatteryStatus = 0,
                DroneLocation = station.Location,
                DeliveryTransfer = null,
                DroneStatus = DroneStatus.Meintenence,
            };

            drones.Add(new DroneToList()
            {
                DroneId = drone.DroneId,
                ModelDrone = drone.DroneModel,
                DroneWeight = drone.Weight,
                BatteryDrone = drone.BatteryStatus,
                Location = new Location() { Lattitude = drone.DroneLocation.Lattitude, Longitude = drone.DroneLocation.Longitude },
                ParcelId = 0,
                DroneStatus = drone.DroneStatus,
            });
            dal.AddDrone(drone.DroneId, drone.DroneModel, (IDAL.DO.WeightCategories)drone.Weight);
        }

        ///  <summary>
        /// Find if the id is unique in a spesific list
        /// </summary>
        /// <typeparam name="T">the type of list</typeparam>
        /// <param name="list">the spesific list </param>
        /// <param name="id">the id to check</param>
        private static bool ExistsIDCheck<T>(IEnumerable<T> list, int id)
        {
            // no item in the list
            if (!list.Any())
                return false;
            T temp = list.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item, null) == id);
            return !(temp.Equals(default(T)));





        }
    }
}

    

   
    
    