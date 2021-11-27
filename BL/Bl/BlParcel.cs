using BL.BO;
using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;

namespace IBL
{
    public partial class BL : IblParcel
    {
        public void AddParcel(Parcel parcel)
        {
            dal.AddParcel(new IDAL.DO.Parcel()
            {
                Id = parcel.Id,
                SenderId = parcel.CustomerSendsFrom.Id,
                TargetId = parcel.CustomerReceivesTo.Id,
                Priority = (IDAL.DO.Priorities)parcel.Priority,
                Weight = (IDAL.DO.WeightCategories)parcel.WeightParcel,
                DroneId = null,
                Requested = parcel.TimeCreatedTheParcel,
            });
        }
        public ParcelInTransfer GetParcelInTransfer(int id)
        {
            var parcel = GetParcel(id);
            var targetCustomer = GetCustomer(parcel.CustomerReceivesTo.Id);
            var senderCustomer = GetCustomer(parcel.CustomerSendsFrom.Id);

            return new ParcelInTransfer()
            {
                Id = id,
                Weight = parcel.WeightParcel,
                Priority = parcel.Priority,
                CollectParcelLocation = targetCustomer.Location,
                DeliveryDestination = senderCustomer.Location,
                ParcelStatus = parcel.DeliveryTime != null,
                DeliveryDistance = Distance(senderCustomer.Location, targetCustomer.Location),
            };
        }

        public void AssignParcelToDrone(int droneId)
        {
            Drone drone = GetDrone(droneId);

            if (drone.DroneStatus == DroneStatuses.Delivery)
            {
                //לתקן  throw new InValidActionException();
            }

            var parcels = (dal.GetUnAssignmentParcels() as List<IDAL.DO.Parcel>)
                          .FindAll(parcel =>
                               IsAbleToPassParcel(drone, GetParcelInTransfer(parcel.Id)) &&
                               (int)parcel.Weight < (int)drone.Weight)
                          .OrderBy(parcel => parcel.Priority)
                          .ThenBy(parcel => parcel.Priority)
                          .ThenBy(parcel => parcel.Weight)
                          .ThenBy(parcel => Distance(GetCustomer(parcel.SenderId).Location, drone.DroneLocation))
                          .ThenBy(parcel => Distance(GetCustomer(parcel.SenderId).Location, drone.DroneLocation))
                          .ToList();

            if (parcels.Count == 0)
            {
                //לתקןthrow new InValidActionException();
            }

            dal.AssignParcelToDrone(parcels.First().Id, droneId);

            drone.DroneStatus = DroneStatuses.Delivery;
        }

        private bool IsAbleToPassParcel(Drone drone, object p)
        {
            throw new NotImplementedException();
        }

        public void DeliveryParcelByDrone(int droneId)
        {
            Drone drone = GetDrone(droneId);
            if (PackageInTransfer
        }



        public Parcel GetParcel(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcels()
        {
            return dal.GetParcels().Select(Parcel => GetParcel(Parcel.Id));
        }

        /// <summary>
        /// return converted parcel to parcel in delivery
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel in delivery</returns>
        public ParcelInTransfer GetParcelInDeliver(int id)
        {
            var parcel = GetParcel(id);
            var targetCustomer = GetCustomer(parcel.CustomerReceivesTo.Id);
            var senderCustomer = GetCustomer(parcel.CustomerSendsFrom.Id);

            return new ParcelInTransfer()
            {
                Id = id,
                Weight = parcel.WeightParcel,
                Priority = parcel.Priority,
                DeliveryDestination = targetCustomer.Location,
                CollectParcelLocation = senderCustomer.Location,
                ParcelStatus = parcel.DeliveryTime != null,
                DeliveryDistance = Distance(senderCustomer.Location, targetCustomer.Location),
            };
        }
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone()
        {
            throw new NotImplementedException();
        }

        public void ParcelCollectionByDrone(int droneId)
        {
            DroneToList droneToList = drones.Find(item => item.DroneId == droneId);
            var parcel = dal.GetParcel(droneToList.ParcelNumberInTransfer);

            ParcelInTransfer parcelInDeliver = GetParcelInDeliver(parcel.Id);

            droneToList.BatteryDrone -= Distance(droneToList.Location, parcelInDeliver.CollectParcelLocation) * ElectricityConfumctiolFree;
            droneToList.Location = parcelInDeliver.CollectParcelLocation;

            dal.CollectParcel(parcel.Id);
        }

        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, IBL.BO.WeightCategories Weight, IBL.BO.Priorities priority)
        {
            throw new NotImplementedException();
        }

        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, WeightCategories Weight, IBL.BO.Priorities priority)
        {
            throw new NotImplementedException();
        }

        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, BO.WeightCategories Weight, BO.Priorities priority)
        {
            throw new NotImplementedException();
        }
        double[] arr;
        arr = dal.RequestElectricity();
            available = arr[0];
            lightWeight = arr[1];
            mediumWeight = arr[2];
            heavyWeight = arr[3];
            chargingRate = arr[4];
        ///להעביר אחכ לבנאי
        public int IdParcel = 0;
        public double Available = 2;
        public double LightWeightCarrier = 10;
        public double MediumWeightBearing = 25;
        public double CarryingHeavyWeight = 40;
        public static double DroneLoadingRate = 10;

        private static double Distance(Location sLocation, Location tLocation)
        {
            var sCoord = new GeoCoordinate(sLocation.Lattitude, sLocation.Longitude);
            var tCoord = new GeoCoordinate(tLocation.Lattitude, tLocation.Longitude);
            return sCoord.GetDistanceTo(tCoord);
        }
    }
}


