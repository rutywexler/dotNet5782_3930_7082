using System;
using System.Collections.Generic;
using System.Linq;
using BO;
using BlApi;
using static BO.Enums;
using System.Runtime.CompilerServices;


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
       //////// [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customerBL)
        {
            try
            {
                lock (dal)
                    dal.AddCustomer(customerBL.Id, customerBL.PhoneNumber, customerBL.Name, customerBL.Location.Longitude, customerBL.Location.Lattitude);
            }
            catch (Dal.ThereIsAnotherObjectWithThisUniqueID ex)
            {
                throw new ThereIsAnotherObjectWithThisUniqueID(ex.Message);
            }
        }
        /// <summary>
        /// the function return the customer that the ID blongs to him
        /// </summary>
        /// <param name="id">the id of the customer i want to get</param>
        /// <returns></returns>
       //////// [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            DO.Customer customer;
            try
            {
                lock (dal)
                    customer = dal.GetCustomer(id);
                lock (dal)
                {
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

            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Dal.ThereIsAnotherObjectWithThisUniqueID ex)
            {
                throw new ThereIsAnotherObjectWithThisUniqueID(ex.Message);
            }
        }


        /// <summary>
        /// the function return the kist of the customers from the data
        /// </summary>
        /// <returns></returns>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerForList> GetCustomers()
        {
            lock (dal)
                return dal.GetCustomers().Select(customer => CustomerToList(customer));
        }


        private CustomerForList CustomerToList(DO.Customer customer)
        {
            IEnumerable<DO.Parcel> parcels;
            lock (dal)
                parcels = dal.GetParcels();
            return new CustomerForList()
            {
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                CustomerPhone = customer.Phone,
                NumOfParcelsSentAndDelivered = parcels.Where(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default)).Count(),
                NumOfParcelsSentAndNotDelivered = parcels.Where(parcel => parcel.SenderId == customer.Id && !parcel.Delivered.Equals(default)).Count(),
                NumOfRecievedParcels = parcels.Where(parcel => parcel.TargetId == customer.Id && parcel.Delivered.Equals(default)).Count(),
                NumOfParcelsOnTheWay = parcels.Where(parcel => parcel.TargetId == customer.Id && parcel.Associated.Equals(default)).Count(),
            };

        }

        /// <summary>
        /// the function update the details of the customer the user want to change
        /// </summary>
        /// <param name="id">the customer id that i want to change him</param>
        /// <param name="name"> the name the user want to change from the old name</param>
        /// <param name="PhoneNumber">the phone numberr the user want to change from the old phone number</param>
       // [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int id, string name = null, string PhoneNumber = null)
        {
            if (name.Equals(string.Empty) && PhoneNumber.Equals(string.Empty))
                throw new ArgumentNullException("There is not field to update");
            DO.Customer customer;
            lock (dal)
                customer = dal.GetCustomer(id);
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
                lock (dal)
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
                Status = parcel.AssignmentTime == default ? PackageStatuses.CREATED : parcel.CollectionTime == default ? PackageStatuses.ASSOCIATED : parcel.DeliveryTime == default ? PackageStatuses.COLLECTED : PackageStatuses.PROVIDED
            };


            if (type == "sender")
            {
                newParcel.CustomerInDelivery = new CustomerInParcel()
                {
                    Id = parcel.CustomerSendsFrom.Id,
                    Name = parcel.CustomerSendsFrom.Name
                };
            }
            else
            {
                newParcel.CustomerInDelivery = new CustomerInParcel()
                {

                    Id = parcel.CustomerReceivesTo.Id,
                    Name = parcel.CustomerReceivesTo.Name
                };
            }

            return newParcel;
        }





    }
}
