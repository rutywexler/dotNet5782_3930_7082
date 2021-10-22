using System;

namespace ConsoleUI
{
    class Program
    {
        public enum Options { Insert, Update, Display, ViewTheLists };
        public enum Insert { Station, Drone, Customer, Parcel };
        public enum Update { AssigningParcelToDrone, CollectParcel, ParcelSupply, SendingDroneforCharging, ReleasingDroneFromCharging };
        public enum Display { BaseStation, Drone, Customer, Parcel };
        public enum ViewTheLists { baseStations, Drone, Customers, Parcel, ParcelNotDrone, FreeBaseStations }

        static void Main(string[] args)
        {

        }
    }
    
    
}
