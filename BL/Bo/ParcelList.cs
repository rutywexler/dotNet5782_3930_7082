using BL.Bo;
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
        public class ParcelList
        {
            public int Id { get; set; }
            public string SendCustomer { get; set; }
            public string ReceivesCustomer  { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public override string ToString() => this.ToStringProps();



        }
    }
}
