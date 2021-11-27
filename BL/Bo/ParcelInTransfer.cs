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
        public class ParcelInTransfer
        {
            public int Id { get; set; }
            public Priorities Priority { get; set; }
            public CustomerInParcel Sender { get; set; }
            public CustomerInParcel Recipient { get; set; }
            public bool ParcelStatus { get; set; }
            public WeightCategories Weight { get; set; }
            public Location CollectParcelLocation { get; set; }//מיקום איסוף
            public Location DeliveryDestination { get; set; }//מיקום יעד לאספקה
            public double DeliveryDistance { get; set; }//מרחק הובלה
        }
    }
   
}
