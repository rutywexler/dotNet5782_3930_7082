using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public static class CustomerInParcelUseBL
    {
        public static CustomerForList ConvertBoCustomerForListToPo(BO.CustomerForList customer)
        {
            return new CustomerForList()
            {
                Id = customer.CustomerId,
                Name = customer.CustomerName,
                PhoneNumber= customer.CustomerPhone,
                PackagesInWay= customer.NumOfParcelsOnTheWay,
                AcceptedPackages=customer.NumOfRecievedParcels,
                DeliveredPackages=customer.NumOfParcelsSentAndDelivered, 
                SendedPackages= customer.NumOfParcelsSentAndNotDelivered
            };
        }

        internal static BO.Customer ConvertPoCustomerToBO(CustomerToAdd customer)
        {
            return new()
            {
                Id= (int)customer.Id,
                Name=customer.Name,
                PhoneNumber=customer.Phone,
                Location = LocationConverter.ConvertBackLocation(customer.Location)

            };
        }
       
        public static SimpleCustomer ConvertCustomer(BO.Customer customer)
        {
            return new SimpleCustomer
            {
                Id = customer.Id,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Location = LocationConverter.ConvertLocation(customer.Location),
                FromCustomer = customer.GetCustomerSendParcels.Select(item => ParcelConverter.ConvertParcelAtCustomer(item)).ToList(),
                ToCustomer = customer.GetCustomerReceivedParcels.Select(item => ParcelConverter.ConvertParcelAtCustomer(item)).ToList()
            };
        }
        public static BO.Customer ConvertCustomerBlToPo(SimpleCustomer customer)
        {
            return new BO.Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Location = LocationConverter.ConvertBackLocation(customer.Location),
                GetCustomerSendParcels = customer.FromCustomer.Select(item => ParcelConverter.ConvertBackParcelAtCustomer(item)).ToList(),
                GetCustomerReceivedParcels = customer.ToCustomer.Select(item => ParcelConverter.ConvertBackParcelAtCustomer(item)).ToList()
            };
        }
    }
}
