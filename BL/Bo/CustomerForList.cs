using BO;


namespace BO
{
    public class CustomerForList
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public int NumOfParcelsOnTheWay { get; set; }
        public int NumOfRecievedParcels { get; set; }
        public int NumOfParcelsSentAndDelivered { get; set; }
        public int NumOfParcelsSentAndNotDelivered { get; set; }
        public override string ToString() => this.ToStringProps();

    }
}

