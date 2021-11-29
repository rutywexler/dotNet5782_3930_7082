
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
            public List<ParcelInCustomer> getCustomerSendParcels { get; set; }
            public List<ParcelInCustomer> getCustomerReceivedParcels { get; set; }
            public override string ToString() => this.ToStringProps();



        }
    }
   
}
