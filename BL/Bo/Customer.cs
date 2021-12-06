
using BL.Bo;
using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
       public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public Location Location { get; set; }
            public List<ParcelInCustomer> GetCustomerSendParcels { get; set; }
            public List<ParcelInCustomer> GetCustomerReceivedParcels { get; set; }
            public override string ToString() => this.ToStringProps();



        }
    }
   
}
