using PL.Model;
using PL.Model.Po;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Converters
{
    public class CustomerConverter
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
    }
}
