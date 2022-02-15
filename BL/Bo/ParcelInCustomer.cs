using BO;
using static BO.Enums;

namespace BO
{
    public class ParcelInCustomer
    {
        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public PackageStatuses Status { get; set; }
        public CustomerInParcel CustomerInDelivery { get; set; }
        public override string ToString() => this.ToStringProps();


    }
}

