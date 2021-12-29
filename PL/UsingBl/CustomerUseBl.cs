using PL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.UsingBl
{
    public class CustomerUseBl
    {
        public BlApi.IBL bl { get; set; }
        public CustomerUseBl()
        {
            bl = BlApi.BlFactory.GetBL();
        }
        public IEnumerable<CustomerInParcel> GetCustomers()
        {
            return bl.GetCustomers().Select(customer => ConvertCustomerToList(customer));
        }

        public CustomerInParcel ConvertCustomerToList(BO.CustomerForList customerToList)
        {
            return new CustomerInParcel
            {
                Id = customerToList.CustomerId,
                Name = customerToList.CustomerName,
                //PhoneNumber = customerToList.CustomerPhone,
                //DeliveredPackages = customerToList.NumOfParcelsSentAndDelivered,
                //SendedPackages = customerToList.NumOfParcelsSentAndNotDelivered,
                //AcceptedPackages = customerToList.NumOfRecievedParcels,
                //PackagesInWay = customerToList.NumOfParcelsOnTheWay
            };
        }
    }
}
