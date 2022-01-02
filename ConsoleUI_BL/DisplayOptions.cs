//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace ConsoleUI_BL
//{
//    partial class Program
//    {

//        /// <summary>
//        /// Receives input from the user: type of organ to print, ID number and calls to the appropriate printing method
//        /// </summary>
//        /// <param name="dalObject"></param>
//        public static void ReplacingDisplay(BlApi.IBL bl)
//        {
     
//           if (!Enum.TryParse(Console.ReadLine(), out Display option))
//            {
//                option = Display.False;
//            }
//            int id;
//            switch (option)
//            {
//                case Display.Station:
//                    {
//                        Console.WriteLine("Enter the ststion ID:");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                            Console.WriteLine(bl.GetStation(id));
//                        break;
//                    }
//                case Display.Drone:
//                    {
//                        Console.WriteLine("Enter the Drone ID:");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                            Console.WriteLine(bl.GetDrone(id));
//                        break;
//                    }
//                case Display.Customer:
//                    {
//                        Console.WriteLine("Enter the customer ID:");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                            Console.WriteLine(bl.GetCustomer(id));
//                        break;
//                    }
//                case Display.Parcel:
//                    {
//                        Console.WriteLine("Enter the parcel ID:");
//                        if (int.TryParse(Console.ReadLine(), out id))
//                            Console.WriteLine(bl.GetParcel(id));
//                        break;
//                    }
//                case Display.False:
//                    {
//                        Console.WriteLine("The input is invalid");
//                        break;
//                    }
                    
//            }

//        }

//        /// <summary>
//        /// Receives input from the user and calls the printing method  
//        /// </summary>
//        /// <param name="dalObject"></param>
//        public static void ReplacingDisplayList(BlApi.IBL bl)
//        {
//            if (!Enum.TryParse(Console.ReadLine(), out DisplayList option))
//            {
//                option = DisplayList.False;
//            }
//            switch (option)
//            {
//                case DisplayList.Sations:
//                    PrintTheList(bl.GetStations());
//                    break;
//                case DisplayList.Drones:
//                    PrintTheList(bl.GetDrones());
//                    break;
//                case DisplayList.Customers:
//                    PrintTheList(bl.GetCustomers());
//                    break;
//                case DisplayList.Parcels:
//                    PrintTheList(bl.GetParcels());
//                    break;
//                case DisplayList.AvailableChargingSations:
//                    PrintTheList(bl.GetStaionsWithEmptyChargeSlots());
//                    break;
//                case DisplayList.ParcelNotAssignToDrone:
//                    PrintTheList(bl.GetParcelsNotAssignedToDrone());
//                    break;
//                case DisplayList.False:
//                    Console.WriteLine("Invalid input");
//                    break;
//            }
//        }

//        /// <summary>
//        /// Prints the whole items in the console
//        /// </summary>
//        /// <param name="list">collection for printing</param>
//        public static void PrintTheList<T>(IEnumerable<T> list)
//        {
//            if (!list.Any())
//                Console.WriteLine("the list is empty!");
//            foreach (var item in list)
//            {
//                Console.WriteLine(item);
//            }

//        }
//    }
//}
