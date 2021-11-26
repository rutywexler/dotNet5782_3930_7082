﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Stations
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlots { get; set; }
            /// <summary>
            /// the method override ToString method
            /// </summary>
            public override string ToString()
            {
                return $"base-station id: {Id} name: {Name}";
            }



        }
    }
}
