using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IblParcel
    {
        public void AddParcel(Parcel parcel);
        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, BO.WeightCategories Weight, BO.Priorities priority);
        public void AssignPackageToDrone(int droneId);
        public void PackageCollectionByDrone(int droneId);
        public void DeliveryParcelByDrone(int droneId);
        public Parcel GetParcel(int id);
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone();
    }
}
