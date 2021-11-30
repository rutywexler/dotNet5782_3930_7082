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
        public void AssignParcelToDrone(int droneId);
        public void ParcelCollectionByDrone(int droneId);
        public void DeliveryParcelByDrone(int droneId);
        public Parcel GetParcel(int id);
        public IEnumerable<ParcelList> GetParcels();
        public IEnumerable<ParcelList> GetParcelsNotAssignedToDrone();
    }
}
