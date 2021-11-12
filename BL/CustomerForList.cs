using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    namespace BO
    {
        public class CustomerForList
        {
            public int CustomerId { get; set; }
            public string  CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public int NumOfPackageOnTheWay { get; set; }
            public int NumOfGettersPackage { get; set; }
            public int NumOfPackagesSentAndDelivered { get; set; }
            public int NumOfPackagesSentAndNotDelivered { get; set; }
        }
    }
}
