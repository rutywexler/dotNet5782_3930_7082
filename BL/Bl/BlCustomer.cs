using BL.BO;
using IBL;
using IBL.BO;
using IDAL;
using DalObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;
using BL.Bl;


namespace IBL
{
    public partial class BL : IblCustomer
    {
        /// <summary>
        /// add customer to the data
        /// </summary>
        /// <param name="id">the customer id</param>
        /// <param name="name">the customer name</param>
        /// <param name="phoneNumber"> the customer phoner number</param>
        /// <param name="location">the customer location</param>
        public void AddCustomer(int id, string name, string phoneNumber, Location location)
        {
            try { 
                 dal.AddCustomer(id, phoneNumber, name, location.Longitude, location.Lattitude);
            }
            catch (IDAL.DO.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {

                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
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
                    getCustomerSendParcels = (List<ParcelInCustomer>)(from parcel in dal.GetParcels()
                                                                       where parcel.SenderId == id
                                                                       select GetParcel(parcel.Id)),
                    getCustomerReceivedParcels = (List<ParcelInCustomer>)(from parcel in dal.GetParcels()
                                                                        where parcel.TargetId == id
                                                                        select GetParcel(parcel.Id)),
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

        private CustomerForList CustomerToList(IDAL.DO.Customer customer)
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
        public void UpdateCustomer(int id, string name=null, string PhoneNumber=null)
        {
            if (name.Equals(string.Empty) && PhoneNumber.Equals(string.Empty))
                throw new ArgumentNullException("There is not field to update");
            
            IDAL.DO.Customer customer = dal.GetCustomer(id);
            try {
                dal.RemoveCustomer(customer);
                if (name.Equals(default))
                    name = customer.Name;
                else if (PhoneNumber.Equals(default))
                    PhoneNumber = customer.Phone;
                dal.AddCustomer(id, PhoneNumber, name, customer.Longitude, customer.Lattitude); 
            }
            catch (IDAL.DO.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {
                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception_ThereIsInTheListObjectWithTheSameValue(ex.Message);
            }
        }
    }
}
