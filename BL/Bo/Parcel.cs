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
            public Customer CustomerSends { get; set; }
            public Customer CustomerReceives { get; set; }
            public WeightCategories WeightParcel { get; set; }
            public Priorities Priority { get; set; }
            public Drone DroneParcel { get; set; }
            public int TimeCreatedThePackage { get; set; }
            public int AssignmentTime { get; set; }
            public int CollectionTime { get; set; }
            public int DeliveryTime { get; set; }
        }

    }
