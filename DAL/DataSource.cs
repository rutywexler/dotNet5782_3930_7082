using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    class DataSource
    {
        internal static Drone[] Drones = new Drone[10];
        internal class Config
        {
            internal static int NumOfDrons = 0;
            internal static int NumOfBaseStations = 0;
            internal static int  NumOfCustomers= 0;
            internal static int NumOfParcels = 0;
            internal int ParcelId;

        }

        internal static Station[] BaseStations = new Station[5];
        internal static Customer[] Customers = new Customer[100];
        internal static Parcel[] Parcels = new Parcel[1000];


    }
}
