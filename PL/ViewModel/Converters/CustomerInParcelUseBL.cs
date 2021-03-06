using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{

    public static class CustomerInParcelUseBl
    {
        public static CustomerInParcel ConvertCustomerInParcel(BO.CustomerInParcel customerInParcel)
        {
            return new CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }
        public static BO.CustomerInParcel ConvertBackCustomerInParcel(CustomerInParcel customerInParcel)
        {
            return new BO.CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }




    }

}
