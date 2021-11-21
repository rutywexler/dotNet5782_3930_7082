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
            public int PackageId { get; set; }
            public Customer CustomerSends { get; set; }
            public Customer CustomerReceives { get; set; }
            public WeightCategories WeightPackage { get; set; }
            public Priorities PackagesPriority { get; set; }
            public Drone DronePackage { get; set; }
            public int TimeCreatedThePackage { get; set; }
            public int AssignmentTime { get; set; }
            public int CollectionTime { get; set; }
            public int DeliveryTime { get; set; }
        }

    }
