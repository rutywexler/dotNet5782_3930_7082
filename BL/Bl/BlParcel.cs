﻿using BL.BO;
using DalObject;
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
            dal.AddParcel(
                parcel.CustomerSendsFrom.Id,
                parcel.CustomerReceivesTo.Id,
               (IDAL.DO.WeightCategories)parcel.WeightParcel,
              (IDAL.DO.Priorities)parcel.Priority
            );
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

            if (drone.DroneStatus == DroneStatus.Delivery)
            {
               
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

            drone.DroneStatus = DroneStatus.Delivery;
        }


        public void DeliveryParcelByDrone(int droneId)
        {
            DroneToList droneToList = drones.Find(drone => drone.DroneId == droneId);
            IDAL.DO.Parcel parcel = dal.GetParcel(droneToList.ParcelId);
            drones.Remove(droneToList);
            IDAL.DO.Customer customer = dal.GetCustomer(parcel.TargetId);
            Location receiverLocation = new() { Longitude = customer.Longitude, Lattitude = customer.Lattitude };
            droneToList.BatteryDrone -= Distance(droneToList.Location, receiverLocation) * dal.GetPowerConsumptionByDrone()[1 + (int)parcel.Weight];
            droneToList.Location = receiverLocation;
            droneToList.DroneStatus = DroneStatus.Available;
            drones.Add(droneToList);
            ParcelDeliveredDrone(parcel.Id);
        }

        private void ParcelDeliveredDrone(int parcelId)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
            dal.RemoveParcel(parcel);
            parcel.Delivered = DateTime.Now;
            dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority, parcel.Id);
        }

        public Parcel GetParcel(int id)
        {
            var parcel = dal.GetParcel(id);
            return new Parcel()
            {
                Id = parcel.Id,
                DroneParcel = GetDrone(parcel.DroneId),
                CustomerSendsFrom = CustomerToCustomerInParcel(dal.GetCustomer(parcel.SenderId)),
                CustomerReceivesTo = CustomerToCustomerInParcel(dal.GetCustomer(parcel.TargetId)),
                WeightParcel = (WeightCategories)parcel.Weight,
                Priority = (Priorities)parcel.Priority,
                TimeCreatedTheParcel = parcel.Requested,
                AssignmentTime = parcel.Scheduled,
                CollectionTime = parcel.PickedUp,
                DeliveryTime = parcel.Delivered,
            };
        }

        private CustomerInParcel CustomerToCustomerInParcel(IDAL.DO.Customer customer)
        {
            return new CustomerInParcel()
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }

        public IEnumerable<Parcel> GetParcels()
        {
            return dal.GetParcels().Select(Parcel => GetParcel(Parcel.Id));
        }


        public IEnumerable<ParcelList> GetParcelsNotAssignedToDrone()
        {
            return dal.GetUnAssignmentParcels().Select(parcel => ParcelToParcelForList(parcel.Id));
        }

        public void ParcelCollectionByDrone(int droneId)
        {
            DroneToList droneToList = drones.Find(item => item.DroneId == droneId);
            var parcel = dal.GetParcel(droneToList.ParcelId);

            ParcelInTransfer parcelInDeliver = GetParcelInTransfer(parcel.Id);

            droneToList.BatteryDrone -= Distance(droneToList.Location, parcelInDeliver.CollectParcelLocation) * Available;
            droneToList.Location = parcelInDeliver.CollectParcelLocation;

            dal.CollectParcel(parcel.Id);
        }

        /// <summary>
        /// return converted parcel to parcel for list
        /// </summary>
        /// <param name="id">id of requested parcel</param>
        /// <returns>parcel for list</returns>
        public ParcelList ParcelToParcelForList(int id)
        {
            var parcel = GetParcel(id);

            return new ParcelList()
            {
                Id = parcel.Id,
                Priority = parcel.Priority,
                Weight = parcel.WeightParcel,
                SendCustomer = parcel.CustomerSendsFrom.Name,
                ReceivesCustomer = parcel.CustomerReceivesTo.Name,
            };
        }
    }
}


