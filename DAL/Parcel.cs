using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public int Weight { get; set; }
            public int Priority { get; set; }
            public int Requested { get; set; }
            public int Droneld { get; set; }
            public int Scheduled { get; set; }
            public int PickedUp { get; set; }
            public int Delivered { get; set; }
        }
    }
}
