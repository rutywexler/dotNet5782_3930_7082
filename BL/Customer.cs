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
            public int ID { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public Location Location { get; set; }
            internal static List<Package> OackageInCustomerFromCustomer = new List<Package>();
            internal static List<Package> OackageInCustomerToCustomer = new List<Package>();


        }
    }
   
}
