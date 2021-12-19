using Bo;
using System;
using static BO.Enums;

namespace BO
{
    public class Parcel
    {
        public int Id { get; set; }
        public CustomerInParcel CustomerSendsFrom { get; set; }
        public CustomerInParcel CustomerReceivesTo { get; set; }
        public WeightCategories WeightParcel { get; set; }
        public Priorities Priority { get; set; }
        public DroneInPackage DroneParcel { get; set; }
        public DateTime? TimeCreatedTheParcel { get; set; }
        public DateTime? AssignmentTime { get; set; }
        public DateTime? CollectionTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public override string ToString() => this.ToStringProps();

    }

}


