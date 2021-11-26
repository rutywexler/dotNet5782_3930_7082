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
                medium,
                weighty
            }

            public enum Priorities
            {
                Regular,
                Fast,
                Emergency
            }

            public enum PackageStatuses
            {
                Defined,
                associated,
                collected,
                provided
            }

            public enum DroneStatuses
            {
                Available,
                Meintenence,
                Delivery

            }
        }
    }
}
