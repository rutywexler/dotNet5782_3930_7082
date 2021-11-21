using BL.BO;
using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    public partial class BL : IblParcel
    {
        public void AssignPackageToDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public void DeliveryParcelByDrone(int droneId)
        {
            Drone drone = GetDrone(droneId);
            if(PackageInTransfer
        }

        

        public Parcel GetParcel(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcelsNotAssignedToDrone()
        {
            throw new NotImplementedException();
        }

        public void PackageCollectionByDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, IBL.BO.WeightCategories Weight, IBL.BO.Priorities priority)
        {
            throw new NotImplementedException();
        }

        public void ReceiptParcelForDelivery(int senderCustomerId, int recieveCustomerId, WeightCategories Weight, IBL.BO.Priorities priority)
        {
            throw new NotImplementedException();
        }
    }
}
