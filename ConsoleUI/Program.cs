using System;
using IDAL.DO;




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
            DalObject.DalObject dalObject = new DalObject.DalObject();
            int index = 0;
            int choice = 0;
            while(choice!=5)
            {
                foreach (var option in Enum.GetNames(typeof(Options)))
                {
                    Console.Write((index++) + "-" + option + "\n");
                }
                choice = int.Parse(Console.ReadLine());
                index = 0;
                switch (choice)
                {
                    case 1:
                        foreach (var optionInsert in Enum.GetNames(typeof(Insert)))
                        {
                            Console.Write((index++) + "-" + optionInsert + "\n");
                        }
                        index = 0;
                        choice = int.Parse(Console.ReadLine());
                        Insert(choice);

                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                   
                }

            }

            void Insert(int choice)
            {
                switch (choice)
                {
                    case 1:
                        dalObject.InsertStation(InputFunctions.GetStation());
                        break;
                    case 2:
                        dalObject.InsertDrone(InputFunctions.GetDrone());
                        break;
                    case 3:
                        dalObject.InsertCustomer(InputFunctions.GetCustomer());
                        break;
                    case 4:
                        dalObject.InsertParcel(InputFunctions.GetParcel());
                        break;
                    default:
                        break;
                }
            }



        }
    }
    
    
}
