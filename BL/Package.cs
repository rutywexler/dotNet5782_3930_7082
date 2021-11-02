using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;

namespace BL
{
    namespace BO
    {
        class Package
        {
            public int PackageId { get; set; }
            public Customer CustomerSends { get; set; }
            public Customer CustomerReceives { get; set; }
            public WeightCategories WeightPackage { get; set; }
            public Priorities PackagesPriority { get; set; }
            public Drone DronePackage { get; set; }
            public int TimereatedTheShipment { get; set; }
            public int AssignmentTime { get; set; }
            public int CollectionTime { get; set; }
            public int DeliveryTime { get; set; }
        }

    }
