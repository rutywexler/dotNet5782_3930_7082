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
        private const int DRONESTATUSESLENGTH = 2;
        public const int MAXINITBATTARY = 20;
        public const int MININITBATTARY = 0;
        public const int FULLBATTRY = 100;
        private static readonly Random rand = new();
        private readonly IDal dal;
        private List<DroneToList> drones;
        public double Available { get; set; }
        public double LightWeightCarrier { get; set; }
        public double MediumWeightBearing { get; set; }
        public double CarryingHeavyWeight { get; set; }
        public static double DroneLoadingRate { get; set; }
        public BL()
        {
            dal = new DalObject.DalObject();
            double[] arr;
            arr = dal.GetPowerConsumptionByDrone();
            Available = arr[0];
            LightWeightCarrier = arr[1];
            MediumWeightBearing = arr[2];
            CarryingHeavyWeight = arr[3];
            DroneLoadingRate = arr[4];
            Initialize();
        }
        private void Initialize()
        {
            var tmpDrones = dal.GetDrones();
            var parcels = dal.GetParcels();
            // create list of stations' location
            var locationOfStation = dal.GetStations().Select(Station => new Location() { Lattitude = Station.Lattitude, Longitude = Station.Longitude }).ToList();
            var customersGotParcelLocation = GetCustomers().Where(customer => customer.NumOfRecievedParcels > 0)
                     .Select(Customer => new Location()
                     {
                         Lattitude = dal.GetCustomer(Customer.CustomerId).Lattitude,
                         Longitude = dal.GetCustomer(Customer.CustomerId).Longitude
                     })
                     .ToList();
            foreach (var drone in tmpDrones)
            {
                bool canTakeParcel = true;
                var parcel = parcels.FirstOrDefault(parcel => parcel.DroneId == drone.Id && parcel.Delivered == default);
                double BatteryStatus;
                double tmpBatteryStatus = default;
                Location tmpLocaiton = default;
                Location Location;
                DroneStatus status = default;
                //set status
                // if the drone makes delivery
                if (parcel.DroneId != 0)
                {
                    status = DroneStatus.Delivery;
                    tmpBatteryStatus = MinBattary(parcel, ref canTakeParcel);
                    if (!canTakeParcel)
                    {
                        status = default;
                        parcel.DroneId = 0;
                    }

                }
                else if (status == default)
                {
                    if (customersGotParcelLocation.Count > 0)
                        status = (DroneStatus)rand.Next(0, DRONESTATUSESLENGTH);
                    else
                        status = DroneStatus.Meintenence;

                }
                // set location and battery
                (Location, BatteryStatus) = status switch
                {
                    DroneStatus.Available => (tmpLocaiton = customersGotParcelLocation[rand.Next(0, customersGotParcelLocation.Count)], rand.Next((int)MinBatteryForAvailAble(tmpLocaiton) + 1, FULLBATTRY)
                    ),
                    DroneStatus.Meintenence => (locationOfStation[rand.Next(0, locationOfStation.Count)],
                    rand.NextDouble() + rand.Next(MININITBATTARY, MAXINITBATTARY)),
                    DroneStatus.Delivery => (FindLocationDroneWithParcel(parcel), tmpBatteryStatus)
                };
                // add the new drone to drones list
                drones.Add(new DroneToList()
                {
                    DroneId = drone.Id,
                    DroneWeight = (WeightCategories)drone.MaxWeight,
                    ModelDrone = drone.Model,
                    DroneStatus = status,
                    Location = Location,
                    ParcelId = parcel.DroneId == 0 ? 0 : parcel.Id,
                    BatteryDrone = BatteryStatus
                });

 

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

        private double GetElectricity(WeightCategories weight)
        {
            return weight switch
            {
                WeightCategories.Light => LightWeightCarrier,
                WeightCategories.Medium => MediumWeightBearing,
                WeightCategories.Heavy => CarryingHeavyWeight,
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// find the location for drone that has parcel
        /// </summary>
        /// <param name="drone">drone</param>
        /// <param name="parcel">drone's parcel</param>
        /// <returns>drone location</returns>
        private Location FindLocationDroneWithParcel(IDAL.DO.Parcel parcel)
        {
            //get sender location
            Location locaiton = GetCustomer(parcel.SenderId).Location;
            // if the drone hasn't picked up the parcel
            if (parcel.Delivered == default && parcel.PickedUp != default)
                return locaiton;
            var station = CloseStation(dal.GetStations(), locaiton);
            return new()
            {
                Lattitude = station.Lattitude,
                Longitude = station.Longitude
            };
        }

        /// <summary>
        /// Calculate electricity for drone to take spesipic parcel 
        /// </summary>
        /// <param name="parcel">the drone's parcel</param>
        /// <param name="drone">drone</param>
        /// <param name="canTakeParcel">ref boolian</param>
        /// <returns> min electricity</returns>
        private double MinBattary(IDAL.DO.Parcel parcel, ref bool canTakeParcel)
        {
            var customerSender = dal.GetCustomer(parcel.SenderId);
            var customerReciver = dal.GetCustomer(parcel.TargetId);
            Location senderLocation = new() { Lattitude = customerSender.Lattitude, Longitude = customerSender.Longitude };
            Location targetLocation = new() { Lattitude = customerReciver.Lattitude, Longitude = customerReciver.Longitude };
            // find drone's location 
            var location = FindLocationDroneWithParcel(parcel);
            double electrity = CalculateElectricity(location, null, senderLocation, targetLocation, (WeightCategories)parcel.Weight, out _);
            // if the drone need more electricity 
            if (electrity > FULLBATTRY)
            {
                dal.RemoveParcel(parcel);
                dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority, parcel.Id, 0, parcel.Requested, parcel.Scheduled, parcel.PickedUp, parcel.Delivered);
                canTakeParcel = false;
                return 0;
            }
            return rand.NextDouble() + rand.Next((int)electrity + 1, FULLBATTRY);
        }

        private double CalculateElectricity(Location aviableDroneLocation, double? batteryStatus, Location CustomerSender, Location CustomerReceives, WeightCategories weight, out double distance)
        {
            double electricity;
            double e = weight switch
            {
                WeightCategories.Light => LightWeightCarrier,
                WeightCategories.Medium => MediumWeightBearing,
                WeightCategories.Heavy => CarryingHeavyWeight
            };
            IDAL.DO.Station station;
            electricity = Distance(aviableDroneLocation, CustomerSender) * Available +
                        Distance(CustomerSender, CustomerReceives) * e;
            station = batteryStatus != null ? ClosetStationThatPossible(dal.GetStations(), aviableDroneLocation, (double)batteryStatus - electricity, out _) : CloseStation(dal.GetStations(), aviableDroneLocation);
            electricity += Distance(CustomerReceives,
                         new Location() { Lattitude = station.Lattitude, Longitude = station.Longitude }) * Available;
            distance = Distance(aviableDroneLocation, CustomerSender) +
                Distance(CustomerSender, CustomerReceives) +
                Distance(CustomerReceives, new Location() { Lattitude = station.Lattitude, Longitude = station.Longitude });
            return electricity;
        }

        /// <summary>
        /// Calculate minimum amount of electricity for drone for arraiving to the closet statoin  
        /// </summary>
        /// <param name="location">drose's location</param>
        /// <returns> min electricity</returns>
        private double MinBatteryForAvailAble(Location location)
        {
            var station = CloseStation(dal.GetStations(), location);
            double electricity = Distance(location, new() { Lattitude = station.Lattitude, Longitude = station.Longitude }) * Available;
            return electricity > FULLBATTRY ? MININITBATTARY : electricity;
        }
    }
}

    

   
    
    