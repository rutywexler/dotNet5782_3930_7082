using static BO.Enums;

namespace BO
{
    public class ParcelList
    {
        public int Id { get; set; }
        public string SendCustomer { get; set; }
        public string ReceivesCustomer { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public override string ToString() => this.ToStringProps();



    }
}

