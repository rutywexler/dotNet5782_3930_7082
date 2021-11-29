using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    namespace BO

    {
        public class Enums
        {
            public enum WeightCategories
            {
                Light,
                Medium,
                Heavy
            }

            public enum Priorities
            {
                Regular,
                Fast,
                Emergency
            }

            public enum PackageStatuses
            {
                AVAILABLE,
                MAINTENANCE,
                DELIVERY
            }

            public enum DroneStatus
            {
                Available,
                Meintenence,
                Delivery

            }
        }
    }
}
