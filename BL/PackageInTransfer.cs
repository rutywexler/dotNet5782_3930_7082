using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;

namespace IBL
{
    namespace BO
    {
        public class PackageInTransfer
        {
            public int ID { get; set; }
            public Priorities Priority { get; set; }
            public CustomerInDelivery Sender { get; set; }
            public CustomerInDelivery Recipient { get; set; }
        }
    }
   
}
