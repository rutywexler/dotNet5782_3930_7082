using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BlApi;
using static BO.Enums;
using System.Runtime.CompilerServices;


namespace BL
{
    partial class Bl : IblParcel
    {
        /// <summary>
        /// the function adds the parcel to the list in the data
        /// </summary>
        /// <param name="parcel">the parcel the user add</param>
        ////////
        ///
        public void AddParcel(Parcel parcel)
        {
            if (!ExistsIDCheck(dal.GetCustomers(), parcel.CustomerSendsFrom.Id))
                throw new KeyNotFoundException("Sender not exist");
            if (!ExistsIDCheck(dal.GetCustomers(), parcel.CustomerReceivesTo.Id))
                throw new KeyNotFoundException("Target not exist");
            try
            {
                lock (dal)
                    dal.AddParcel(
                        parcel.CustomerSendsFrom.Id,
                        parcel.CustomerReceivesTo.Id,
                       (DO.WeightCategories)parcel.WeightParcel,
                      (DO.Priorities)parcel.Priority
                    );
            }
            catch (Dal.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {

                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
        }

        /// <summary>
        /// the function returns parcelin transer that the id belongs to him
        /// </summary>
        /// <param name="id">the id of the parcel in transfer</param>
        /// <returns></returns>
       ////////\
        public ParcelInTransfer GetParcelforlist(int id)
        {
            DO.Parcel parcel;
            DO.Customer targetCustomer, senderCustomer;
            try
            {
                lock (dal)
                    parcel = dal.GetParcel(id);
                lock (dal)
                    targetCustomer = dal.GetCustomer(parcel.TargetId);
                lock (dal)
                    senderCustomer = dal.GetCustomer(parcel.SenderId);

                return new ParcelInTransfer()
                {
                    Id = id,
                    Weight = (WeightCategories)parcel.Weight,
                    Sender = new CustomerInParcel() { Id = senderCustomer.Id, Name = senderCustomer.Name },
                    Recipient = new CustomerInParcel() { Id = targetCustomer.Id, Name = targetCustomer.Name },
                    Priority = (Priorities)parcel.Priority,
                    CollectParcelLocation = new Location { Lattitude = targetCustomer.Lattitude, Longitude = targetCustomer.Longitude },
                    DeliveryDestination = new Location { Lattitude = senderCustomer.Lattitude, Longitude = senderCustomer.Longitude },
                    ParcelStatus = parcel.Delivered != null,
                    DeliveryDistance = LocationExtensions.Distance(new Location() { Longitude = senderCustomer.Longitude, Lattitude = senderCustomer.Lattitude }, new Location() { Longitude = targetCustomer.Longitude, Lattitude = targetCustomer.Lattitude }),
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
       ////////\
        public void AssignParcelToDrone(int droneId)
        {
            Drone drone = GetDrone(droneId);

            if (drone.DroneStatus == DroneStatus.Delivery)
            {
                throw new InvalidEnumArgumentException("Because that The drone is not available  its not possible to send it for charging ");
            }
            var parcels = GetParcelsNotAssignedToDrone()
                 .Select(parcel => GetParcel(parcel.Id))
                 .Where(parcel =>
                      (int)parcel.WeightParcel <= (int)drone.Weight &&
                      IsDroneCanTakeTheParcel(drone, GetParcelforlist(parcel.Id)))
                 .OrderBy(p => p.Priority)
                 .ThenBy(p => p.WeightParcel)
                 .ThenBy(p => LocationExtensions.Distance(GetCustomer(p.CustomerSendsFrom.Id).Location, drone.DroneLocation)).ToList();

            if (!parcels.Any())
            {
                throw new InValidActionException("Couldn't assign any parcel to the drone.");
            }

            Parcel parcel = parcels.First();
            lock (dal)
                dal.AssignParcelToDrone(parcel.Id, droneId);
            DroneToList droneToList = drones.FirstOrDefault(item => item.DroneId == droneId);
            droneToList.ParcelId = parcel.Id;
            droneToList.DroneStatus = DroneStatus.Delivery;
        }

        /// <summary>
        /// the function responsible to make the delivery parcel by the drone id the function get
        /// </summary>
        /// <param name="droneId"> the drone id</param>
       ////////\
        public void DeliveryParcelByDrone(int droneId)
        {
            DroneToList droneToList = drones.Find(drone => drone.DroneId == droneId);
            DO.Parcel parcel;
            lock (dal)
                parcel = dal.GetParcel((int)droneToList.ParcelId);
            drones.Remove(droneToList);
            DO.Customer customer;
            lock (dal)
                customer = dal.GetCustomer(parcel.TargetId);
            Location receiverLocation = new() { Longitude = customer.Longitude, Lattitude = customer.Lattitude };
            lock (dal)
                droneToList.BatteryDrone -= LocationExtensions.Distance(droneToList.Location, receiverLocation) * dal.GetPowerConsumptionByDrone()[1 + (int)parcel.Weight];
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
            DO.Parcel parcel;
            lock (dal)
                parcel = dal.GetParcel(parcelId);
            lock (dal)
                dal.RemoveParcelAbsolute(parcel.Id);
            parcel.Delivered = DateTime.Now;
            lock (dal)
                dal.AddParcel(parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority, parcel.Id, parcel.DroneId, parcel.Requested, parcel.Scheduled, (DateTime)parcel.PickedUp, parcel.Delivered);
        }

        /// <summary>
        /// get parcel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ////////\
        public Parcel GetParcel(int id)
        {
            try
            {
                DO.Parcel parcel;
                lock (dal)
                    parcel = dal.GetParcel(id);
                DroneToList drone = drones.FirstOrDefault(drone => drone.DroneId == parcel.DroneId);
                lock (dal)
                    return new Parcel()
                    {
                        Id = parcel.Id,
                        DroneParcel = drone != default ? DroneToDroneInPackage(drone) : null,
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
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException(ex.Message);
            }

        }

        /// <summary>
        /// Convert a drone To List to Drone With Parcel
        /// </summary>
        /// <param name="drone">The drone to convert</param>
        /// <returns>The converter drone</returns>
        private DroneInPackage DroneToDroneInPackage(DroneToList drone)
        {
            return new DroneInPackage()
            {
                ID = drone.DroneId,
                BatteryStatus = drone.BatteryDrone,
                Location = drone.Location
            };
        }


        private CustomerInParcel CustomerToCustomerInParcel(DO.Customer customer)
        {
            return new CustomerInParcel()
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }

        ////////\
        public IEnumerable<ParcelList> GetParcels()
        {
            lock (dal)
                return dal.GetParcels().Select(parcel => ParcelToParcelForList(parcel.Id));
        }

        ////////\
        public IEnumerable<ParcelList> GetParcelsNotAssignedToDrone()
        {
            lock (dal)
                return dal.GetParcels(parcel => parcel.DroneId == 0)
                    .Select(parcel => ParcelToParcelForList(parcel.Id));

            //return dal.GetUnAssignmentParcels().Select(parcel => ParcelToParcelForList(parcel.Id));
        }

        ////////\
        public void ParcelCollectionByDrone(int droneId)
        {
            DroneToList droneToList = drones.FirstOrDefault(item => item.DroneId == droneId);
            if (droneToList == default)
                throw new ArgumentNullException(" There is no a drone with the same id in data");
            if (droneToList.ParcelId == null)
                throw new ArgumentNullException("No parcel has been associated yet");
            drones.Remove(droneToList);
            DO.Parcel parcel = default;
            try
            {
                lock (dal)
                    parcel = dal.GetParcel((int)droneToList.ParcelId);
                //if (parcel.PickedUp != default)
                //    throw new ArgumentNullException("The package has already been collected");
                DO.Customer customer;
                lock (dal)
                    customer = dal.GetCustomer(parcel.SenderId);
                Location senderLocation = new() { Longitude = customer.Longitude, Lattitude = customer.Lattitude };
                droneToList.BatteryDrone -= LocationExtensions.Distance(droneToList.Location, senderLocation) * Available;
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
            PackageStatuses status;
            var parcel = GetParcel(id);
            if (parcel.DeliveryTime != null)
                status = PackageStatuses.PROVIDED;
            else if (parcel.CollectionTime != null)
                status = PackageStatuses.COLLECTED;
            else if (parcel.AssignmentTime != null)
                status = PackageStatuses.ASSOCIATED;
            else
                status = PackageStatuses.CREATED;

            return new ParcelList()
            {
                Id = parcel.Id,
                Priority = parcel.Priority,
                Weight = parcel.WeightParcel,
                ParcelStatus = status,
                SendCustomer = parcel.CustomerSendsFrom.Name,
                ReceivesCustomer = parcel.CustomerReceivesTo.Name,
            };
        }
        //////
        ///
        public void DeleteParcel(int id)
        {
            DO.Parcel parcel;
            lock (dal)
                parcel = dal.GetParcel(id);
            ParcelList parcelList = ParcelToParcelForList(id);
            if (parcelList.ParcelStatus == PackageStatuses.COLLECTED|| parcelList.ParcelStatus == PackageStatuses.ASSOCIATED)
            {
                throw new InValidActionException();
            }
            lock (dal)
                dal.RemoveParcel(parcel.Id);
        }

    }
}


