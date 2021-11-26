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

            public enum DroneStatuses
            {
                AVAILABLE,
                MAINTENANCE,
                DELIVERY
            }
        }
    }
}
