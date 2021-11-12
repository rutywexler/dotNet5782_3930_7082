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
        public class PackageList
        {
            public int PackageListId { get; set; }
            public string SendCustomer { get; set; }
            public string ReceivesCustomer  { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public Priorities priority { get; set; }


        }
    }
}
