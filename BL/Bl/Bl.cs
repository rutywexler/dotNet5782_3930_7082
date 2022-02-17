using System;
using System.Collections.Generic;
using System.Linq;
using BO;
using BlApi;
using static BO.Enums;
using Singelton;
using DalApi;
using System.Runtime.CompilerServices;


namespace BL
{
    sealed partial class Bl : Singleton<Bl>, IBL
    {
        public const int MAXINITBATTARY = 20;
        public const int MININITBATTARY = 0;
        public const int FULLBATTRY = 100;
        private static readonly Random rand = new();
        public Idal dal { get; } = DalFactory.GetDL();
        public List<DroneToList> drones = new();
        public double Available { get; set; }
        public double LightWeightCarrier { get; set; }
        public double MediumWeightBearing { get; set; }
        public double CarryingHeavyWeight { get; set; }
        public  double DroneLoadingRate { get; set; }
        Bl()
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
        static Bl()
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

            foreach (var drone in Drones)
            {

                var parcel = parcels.FirstOrDefault(parcel => parcel.DroneId == drone.Id);
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
                    var deliveredParcels = parcels.FindAll(p => p.Delivered.HasValue);
                    if (deliveredParcels.Count > 0)
                    {
                        var randomParcel = deliveredParcels[rand.Next(deliveredParcels.Count)];
                        var customer = dal.GetCustomer(randomParcel.TargetId);
                        return new Location() { Lattitude = customer.Lattitude, Longitude = customer.Longitude };
                    }
                    return new Location();
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

                if (status == DroneStatus.Meintenence)
                {
                    //dal.AddDRoneCharge(drone.Id, dal.GetStations().FirstOrDefault(station => station.Lattitude == location.Lattitude && station.Longitude == location.Longitude).Id);
                }

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
                                                  LocationExtensions.Distance(location, senderLocation) * Available +
                                                  LocationExtensions.Distance(senderLocation, targetLocation) * GetElectricity((WeightCategories)parcel.Weight) +
                                                  LocationExtensions.Distance(targetLocation, FindClosest(targetLocation, availableStationsLocations)) * Available
                                              ), 80)
                                             , 100
                                          ),
                };

                drones.Add(
                        new DroneToList()
                        {
                            DroneId = drone.Id,
                            ModelDrone = drone.Model,
                            DroneWeight = (WeightCategories)drone.MaxWeight,
                            BatteryDrone = battery,
                            DroneStatus = status,
                            Location = location,
                            ParcelId = parcelInTransfer
                        }
                    );
                if (status == DroneStatus.Meintenence)
                    dal.AddDRoneCharge(drone.Id, dal.GetStations().FirstOrDefault(station => (station.Lattitude == location.Lattitude && station.Longitude == location.Longitude)).Id);
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
            return locations.OrderBy(l => LocationExtensions.Distance(location, l)).First();
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






