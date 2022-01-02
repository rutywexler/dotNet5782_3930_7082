using System.Collections.Generic;
using BO;

namespace BlApi
{
    public interface IblParcel
    {
        public void AddParcel(Parcel parcel);
        public void AssignParcelToDrone(int droneId);
        public void ParcelCollectionByDrone(int droneId);
        public void DeliveryParcelByDrone(int droneId);
        public Parcel GetParcel(int id);
        public void DeleteParcel(int id);
        public IEnumerable<ParcelList> GetParcels();
        public IEnumerable<ParcelList> GetParcelsNotAssignedToDrone();
    }
}
