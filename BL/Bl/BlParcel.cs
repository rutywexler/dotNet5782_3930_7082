﻿using BL.Bl;
using BL.BO;
using DalObject;
using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;

namespace IBL
{
    public partial class BL : IblParcel
    {
        /// <summary>
        /// the function adds the parcel to the list in the data
        /// </summary>
        /// <param name="parcel">the parcel the user add</param>
        public void AddParcel(Parcel parcel)
        {
            if (ExistsIDCheck(dal.GetCustomers(), parcel.CustomerSendsFrom.Id))
                throw new KeyNotFoundException("Sender not exist");
            if (ExistsIDCheck(dal.GetCustomers(), parcel.CustomerReceivesTo.Id))
                throw new KeyNotFoundException("Target not exist");
            try
            {
                dal.AddParcel(
                    parcel.CustomerSendsFrom.Id,
                    parcel.CustomerReceivesTo.Id,
                   (IDAL.DO.WeightCategories)parcel.WeightParcel,
                  (IDAL.DO.Priorities)parcel.Priority
                );
            }
            catch (IDAL.DO.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {

                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
        }

        /// <summary>
        /// the function returns parcelin transer that the id belongs to him
        /// </summary>
        /// <param name="id">the id of the parcel in transfer</param>
        /// <returns>new ParcelInTransfer</returns>
        public ParcelInTransfer GetParcelInTransfer(int id)
        {
            try
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
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException(ex.Message);
            }
        }

        /// <summary>
        /// the function assigns parcel to drone
        /// </summary>
        /// <param name="droneId"> the drone id</param>
        public void AssignParcelToDrone(int droneId)
        {
            Drone drone = GetDrone(droneId);

            if (drone.DroneStatus == DroneStatus.Delivery)
            {
                throw new InvalidEnumArgumentException("Because that The drone is not available  its not possible to send it for charging ");
            }

            var parcels = (dal.GetUnAssignmentParcels() as List<IDAL.DO.Parcel>)
                          .FindAll(parcel =>
                               IsDroneCanTakeTheParcel(drone, GetParcelInTransfer(parcel.Id)) &&
                               (int)parcel.Weight < (int)drone.Weight)
                          .OrderBy(parcel => parcel.Priority)
                          .ThenBy(parcel => parcel.Priority)
                          .ThenBy(parcel => parcel.Weight)
                          .ThenBy(parcel => Distance(GetCustomer(parcel.SenderId).Location, drone.DroneLocation))
                          .ThenBy(parcel => Distance(GetCustomer(parcel.SenderId).Location, drone.DroneLocation))
                          .ToList();

            if (parcels.Count == 0)
            {
                throw new InvalidEnumArgumentException();
            }

            dal.AssignParcelToDrone(parcels.First().Id, droneId);

            drone.DroneStatus = DroneStatus.Delivery;
        }

        /// <summary>
        /// the function responsible to make the delivery parcel by the drone id the function get
        /// </summary>
        /// <param name="droneId"> the drone id</param>
        public void DeliveryParcelByDrone(int droneId)
        {
            DroneToList droneToList = drones.Find(drone => drone.DroneId == droneId);
            
            IDAL.DO.Parcel parcel = dal.GetParcel((int)droneToList.ParcelId);
            drones.Remove(droneToList);
            IDAL.DO.Customer customer = dal.GetCustomer(parcel.TargetId);
            Location receiverLocation = new() { Longitude = customer.Longitude, Lattitude = customer.Lattitude };
            droneToList.BatteryDrone -= Distance(droneToList.Location, receiverLocation) * dal.GetPowerConsumptionByDrone()[1 + (int)parcel.Weight];
            droneToList.Location = receiverLocation;
            droneToList.DroneStatus = DroneStatus.Available;
            drones.Add(droneToList);
            ParcelDeliveredDrone(parcel.Id);
        }

        /// <summary>
        /// the function make the parcel with the id that the function get to deliverd drone
        /// </summary>
        /// <param name="parcelId">the parcel id</param>
        private void ParcelDeliveredDrone(int parcelId)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
            dal.RemoveParcel(parcel);
            parcel.Delivered = DateTime.Now;
            dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority, parcel.Id);
        }

        /// <summary>
        /// the function get the parcel that the id the function get belongs to him
        /// </summary>
        /// <param name="id"></param>
        /// <returns>new Parcel</returns>
        public Parcel GetParcel(int id)
        {
            try { 
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
                    CollectionTime = (DateTime)parcel.PickedUp,
                    DeliveryTime = parcel.Delivered,
                };
            }
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException(ex.Message);
            }

        }

        /// <summary>
        /// the function convert customer to customer in parcel
        /// </summary>
        /// <param name="customer">customer in type of idal</param>
        /// <returns>new CustomerInParcel</returns>
        private CustomerInParcel CustomerToCustomerInParcel(IDAL.DO.Customer customer)
        {
            return new CustomerInParcel()
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }

        /// <summary>
        /// the function returns the list of the parcels from the data
        /// </summary>
        /// <returns>the parcel list from the data</returns>
        public IEnumerable<Parcel> GetParcels()
        {
            return dal.GetParcels().Select(Parcel => GetParcel(Parcel.Id));
        }

        /// <summary>
        /// the function get Parcels Not Assigned To Drone
        /// </summary>
        /// <returns>Parcels Not Assigned To Drone</returns>
        public IEnumerable<ParcelList> GetParcelsNotAssignedToDrone()
        {
            return dal.GetUnAssignmentParcels().Select(parcel => ParcelToParcelForList(parcel.Id));
        }

        /// <summary>
        /// the function responsible to Parcel Collection By Drone
        /// </summary>
        /// <param name="droneId"> drone id</param>
        public void ParcelCollectionByDrone(int droneId)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.DroneId == droneId);
            if (droneToList == default)
                throw new ArgumentNullException(" There is no a drone with the same id in data");
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("No parcel has been associated yet");
            drones.Remove(droneToList);
            IDAL.DO.Parcel parcel = default;
            try
            {
                parcel = dal.GetParcel((int)droneToList.ParcelId);
                if (parcel.PickedUp != default)
                    throw new ArgumentNullException("The package has already been collected");
                IDAL.DO.Customer customer = dal.GetCustomer(parcel.SenderId);
                Location senderLocation = new() { Longitude = customer.Longitude, Lattitude = customer.Lattitude };
                droneToList.BatteryDrone -= Distance(droneToList.Location, senderLocation) * Available;
                droneToList.Location = senderLocation;
                colloctDalParcel(parcel.Id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {
                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
            finally
            {
                drones.Add(droneToList);
             
            }
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





