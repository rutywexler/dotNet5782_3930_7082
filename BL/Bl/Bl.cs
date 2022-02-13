using System;
using System.Collections.Generic;
using System.Linq;
using BO;
using BlApi;
using static BO.Enums;
using Singelton;
using DalApi;

namespace Bl
{
    public sealed partial class BL : Singleton<BL>, IBL
    {
        public const int MAXINITBATTARY = 20;
        public const int MININITBATTARY = 0;
        public const int FULLBATTRY = 100;
        private static readonly Random rand = new();
        DalApi.Idal dal { get; } = DalFactory.GetDL();
        public List<DroneToList> drones = new();
        public double Available { get; set; }
        public double LightWeightCarrier { get; set; }
        public double MediumWeightBearing { get; set; }
        public double CarryingHeavyWeight { get; set; }
        public static double DroneLoadingRate { get; set; }
        BL()
        {
            //dal = new DalObject.DalObject();
            //drones = new List<DroneToList>();
            double[] arr;
            arr = dal.GetPowerConsumptionByDrone();
            Available = arr[0];
            LightWeightCarrier = arr[1];
            MediumWeightBearing = arr[2];
            CarryingHeavyWeight = arr[3];
            DroneLoadingRate = arr[4];
            Initialize();
        }
        static BL()
        { }

        /// <summary>
        /// Initialize drones
        /// </summary>
        private void Initialize()
        {
            var Drones = dal.GetDrones().ToList();
            var parcels = dal.GetParcels().ToList();
            var stationsLocations = dal.GetStations()
                                       .Select(s => new Location() { Lattitude = s.Lattitude, Longitude = s.Longitude })
                                       .ToList();

            foreach (var Drone in Drones)
            {

                var parcel = parcels.FirstOrDefault(parcel => parcel.DroneId == Drone.Id);
                double battery;
                int? parcelInTransfer = null;
                DroneStatus status;
                Location location;
                Location targetLocation = null;
                Location senderLocation = null;
                var availableStations = GetStaionsWithEmptyChargeSlots()
                                           .Select(station => GetStation(station.IdStation))
                                           .ToList();

                //status
                if (parcel.Equals(default(DO.Parcel)))
                {
                    if (availableStations.Count == 0)
                        status = DroneStatus.Available;
                    else
                        status = (DroneStatus)rand.Next(0, 2);
                }
                else
                {
                    status = DroneStatus.Delivery;
                    parcelInTransfer = parcel.Id;
                    var targetCustomer = dal.GetCustomer(parcel.TargetId);
                    targetLocation = new Location() { Lattitude = targetCustomer.Lattitude, Longitude = targetCustomer.Longitude };
                    var senderCustomer = dal.GetCustomer(parcel.SenderId);
                    senderLocation = new Location() { Lattitude = senderCustomer.Lattitude, Longitude = senderCustomer.Longitude };

                }

                Location RandomSuppliedParcelLocation()
                {
                    var DeliveredParcels = parcels.FindAll(p => p.Delivered != null).ToList();
                    var randomParcel = DeliveredParcels[rand.Next(DeliveredParcels.Count)];

                    var customer = dal.GetCustomer(randomParcel.TargetId);

                    return new Location() { Lattitude = customer.Lattitude, Longitude = customer.Longitude };
                }
               
                //location
                location = status switch
                {
                    DroneStatus.Available => RandomSuppliedParcelLocation(),
                    DroneStatus.Meintenence => stationsLocations[rand.Next(stationsLocations.Count)],
                    DroneStatus.Delivery => parcel.Delivered != null
                                          ? FindClosest(targetLocation, stationsLocations)
                                          : senderLocation,
                };

                var availableStationsLocations = dal.GetAvailableChargingStations()
                                                   .Select(s => new Location() { Lattitude = s.Lattitude, Longitude = s.Longitude })
                                                   .ToList();


                //battery
                battery = status switch
                {
                    DroneStatus.Available => rand.Next(/*(int)((int)Distance(location, FindClosest(location, availableStationsLocations))*/(int)(20 * Available), 100),
                    DroneStatus.Meintenence => rand.NextDouble() * 20,
                    DroneStatus.Delivery => rand.Next(Math.Min(
                                              (int)(
                                                  Distance(location, senderLocation) * Available +
                                                  Distance(senderLocation, targetLocation) * GetElectricity((WeightCategories)parcel.Weight) +
                                                  Distance(targetLocation, FindClosest(targetLocation, availableStationsLocations)) * Available
                                              ), 80)
                                             , 100
                                          ),
                };

                drones.Add(
                        new DroneToList()
                        {
                            DroneId = Drone.Id,
                            ModelDrone = Drone.Model,
                            DroneWeight = (WeightCategories)Drone.MaxWeight,
                            BatteryDrone = battery,
                            DroneStatus = status,
                            Location = location,
                            ParcelId = parcelInTransfer
                        }
                    );
            }
        }

        /// <summary>
        /// The function finds the station closest to the given location
        /// </summary>
        /// <param name="location">location</param>
        /// <param name="locations">locations</param>
        /// <returns></returns>
        public Location FindClosest(Location location, IEnumerable<Location> locations)
        {
            return locations.OrderBy(l => Distance(location, l)).First();
        }





        ///  <summary>
        /// Find if the id is unique in a spesific list
        /// </summary>
        /// <typeparam name="T">the type of list</typeparam>
        /// <param name="list">the spesific list </param>
        /// <param name="id">the id to check</param>
        private bool ExistsIDCheck<T>(IEnumerable<T> list, int id)
        {
            // no item in the list
            if (!list.Any())
                return false;
            T temp = list.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item, null) == id);
            return !temp.Equals(default(T));
        }

        private double GetElectricity(WeightCategories weight)
        {
            return weight switch
            {
                WeightCategories.Light => LightWeightCarrier,
                WeightCategories.Medium => MediumWeightBearing,
                WeightCategories.Heavy => CarryingHeavyWeight,
            };
        }


    }
}






