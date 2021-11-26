using IBL.BO;
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
       public class Parcel
        {
            public int Id { get; set; }
            public CustomerInParcel CustomerSends { get; set; }
            public CustomerInParcel CustomerReceives { get; set; }
            public WeightCategories WeightParcel { get; set; }
            public Priorities Priority { get; set; }
            public Drone DroneParcel { get; set; }
            public DateTime TimeCreatedTheParcel { get; set; }
            public DateTime AssignmentTime { get; set; }
            public DateTime CollectionTime { get; set; }
            public DateTime DeliveryTime { get; set; }
        }

    }
