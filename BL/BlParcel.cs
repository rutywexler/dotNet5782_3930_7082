﻿using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class Bl : IblParcel
    {
        public void AssignPackageToDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public void DeliveryParcelByDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public Package GetParcel(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Package> GetParcels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Package> GetParcelsNotAssignedToDrone()
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
    }
}
