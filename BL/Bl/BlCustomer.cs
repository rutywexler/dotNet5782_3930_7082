using System;
using System.Collections.Generic;
using System.Linq;
using BO;
using BlApi;
using static BO.Enums;

namespace BL
{
    partial class Bl : IblCustomer
    {
        /// <summary>
        /// add customer to the data
        /// </summary>
        /// <param name="id">the customer id</param>
        /// <param name="name">the customer name</param>
        /// <param name="phoneNumber"> the customer phoner number</param>
        /// <param name="location">the customer location</param>
        public void AddCustomer(Customer customerBL)
        {
           // try
            {
                dal.AddCustomer(customerBL.Id, customerBL.PhoneNumber, customerBL.Name, customerBL.Location.Longitude, customerBL.Location.Lattitude);
            }
            //catch (Dal.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            //{
            //    throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            //}
        }
        /// <summary>
        /// the function return the customer that the ID blongs to him
        /// </summary>
        /// <param name="id">the id of the customer i want to get</param>
        /// <returns></returns>
        public Customer GetCustomer(int id)
        {
            try
            {
                var customer = dal.GetCustomer(id);
                return new Customer()
                {
                    Id = customer.Id,
                    Location = new Location() { Lattitude = customer.Lattitude, Longitude = customer.Longitude },
                    Name = customer.Name,
                    PhoneNumber = customer.Phone,
                    GetCustomerSendParcels = (from parcel in dal.GetParcels()
                                              where parcel.SenderId == id
                                              select ParcelToParcelAtCustomer(GetParcel(parcel.Id), "sender")).ToList(),
                    GetCustomerReceivedParcels = (from parcel in dal.GetParcels()
                                                  where parcel.TargetId == id
                                                  select ParcelToParcelAtCustomer(GetParcel(parcel.Id), "Target")).ToList(),
                };
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }


        /// <summary>
        /// the function return the kist of the customers from the data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerForList> GetCustomers()
        {
            return dal.GetCustomers().Select(customer => CustomerToList(customer));
        }


        private CustomerForList CustomerToList(DO.Customer customer)
        {
            var parcels = dal.GetParcels();
            return new CustomerForList()
            {
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                CustomerPhone = customer.Phone,
                NumOfParcelsSentAndDelivered = parcels.Where(parcel => parcel.SenderId == customer.Id && !parcel.Delivered.Equals(default)).Count(),
                NumOfParcelsSentAndNotDelivered = parcels.Where(parcel => parcel.SenderId == customer.Id && !parcel.Delivered.Equals(default)).Count(),
                NumOfRecievedParcels = parcels.Where(parcel => parcel.TargetId == customer.Id && !parcel.Delivered.Equals(default)).Count(),
                NumOfParcelsOnTheWay = parcels.Where(parcel => parcel.TargetId == customer.Id && !parcel.Delivered.Equals(default)).Count(),
            };
        }

        /// <summary>
        /// the function update the details of the customer the user want to change
        /// </summary>
        /// <param name="id">the customer id that i want to change him</param>
        /// <param name="name"> the name the user want to change from the old name</param>
        /// <param name="PhoneNumber">the phone numberr the user want to change from the old phone number</param>
        public void UpdateCustomer(int id, string name = null, string PhoneNumber = null)
        {
            if (name.Equals(string.Empty) && PhoneNumber.Equals(string.Empty))
                throw new ArgumentNullException("There is not field to update");
            DO.Customer customer = dal.GetCustomer(id);
            try
            {
               
                if (name.Equals(default))
                    name = customer.Name;
                else
                    customer.Name = name;
                if (PhoneNumber.Equals(default))
                    PhoneNumber = customer.Phone;
                else
                    customer.Phone = PhoneNumber;       
                dal.UpdateCustomer(customer);
            }
            catch (Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {
                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
        }
        //public void UpdateDrone(int id, string name)
        //{
        //    if (ExistsIDCheck(dal.GetDrones(), id))
        //        throw new KeyNotFoundException();
        //    DO.Drone droneDl = dal.GetDrone(id);
        //    if (name.Equals(default))
        //        throw new ArgumentNullException("For updating, you must enter the name! ");
        //    dal.UpdateDrone(droneDl);

        //}

        /// <summary>
        /// Convert a BL parcel to Parcel At Customer
        /// </summary>
        /// <param name="parcel">The parcel to convert</param>
        /// <param name="type">The type of the customer</param>
        /// <returns>The converted parcel</returns>
        private ParcelInCustomer ParcelToParcelAtCustomer(Parcel parcel, string type)
        {
            ParcelInCustomer newParcel = new()
            {
                Id = parcel.Id,
                Weight = parcel.WeightParcel,
                Priority = parcel.Priority,
                Status = parcel.AssignmentTime == default ? PackageStatuses.DEFINED : parcel.CollectionTime == default ? PackageStatuses.ASSOCIATED : parcel.DeliveryTime == default ? PackageStatuses.COLLECTED : PackageStatuses.PROVIDED
            };


            if (type == "sender")
            {
                newParcel.CustomerInDelivery = new CustomerInParcel()
                {
                    Id = parcel.CustomerReceivesTo.Id,
                    Name = parcel.CustomerReceivesTo.Name
                };
            }
            else
            {
                newParcel.CustomerInDelivery = new CustomerInParcel()
                {
                    Id = parcel.CustomerSendsFrom.Id,
                    Name = parcel.CustomerSendsFrom.Name
                };
            }

            return newParcel;
        }
        public void DeleteCustomer(int id)
        {
            var customer = dal.GetCustomer(id);
            dal.RemoveCustomer(customer.Id);
        }
    }
}
