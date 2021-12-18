using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
      
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }

            public DateTime Requested { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime ? PickedUp { get; set; }
            public DateTime ? Delivered { get; set; }
            /// <summary>
            /// the method override ToString method
            /// </summary>
            public override string ToString()
            {
                return $"ParcelId: {Id } SenderId: {SenderId} GetterId:" +
                    $" {TargetId} Weight: {Weight} Status: {Priority} DroneId: {DroneId}" +
                    $" Scheduled: {Scheduled}  Requested:{Requested} " +
                    $"PickedUp:{PickedUp} Delivered:{Delivered}";
            }
        }
    }
}
