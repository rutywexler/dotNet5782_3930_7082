using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IBL.BO;
using static BL.BO.Enums;

namespace ConsoleUI_BL
{
    class FunctionMain
    {
        public static BaseStation AddBaseStation()
        {
            BaseStation tempBaseStation = new BaseStation();

            Console.WriteLine("Enter station id:");
            tempBaseStation.ID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter station name:");
            tempBaseStation.Name = Console.ReadLine();

            Console.WriteLine("Enter station Lattitude:");
            tempBaseStation.location.Lattitude = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter station Longitude:");
            tempBaseStation.location.Longitude = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Number of charging stations:");
            tempBaseStation.NumberOfChargingStations = int.Parse(Console.ReadLine());

            //לאתחל רשימה לריקה   tempBaseStation.SkimmersInCharge =


            Console.WriteLine("-----Entering details successfully-----");

            return tempBaseStation;

        }

        public static Drone AddDrone()
        {
            Drone tempDrone = new Drone();

            Console.WriteLine("Enter Drone id:");
            tempDrone.DroneId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter weight category:  1-Light 2-medium 3-weighty");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        tempDrone.Weight = Enums.WeightCategories.Light;
                        break;
                    }
                case 2:
                    {
                        tempDrone.Weight = Enums.WeightCategories.medium;
                        break;
                    }
                case 3:
                    {
                        tempDrone.Weight = Enums.WeightCategories.weighty;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Input! Enter choice again:");
                        break;
                    }
            }
            Console.WriteLine("Enter a station number Put the skimmer in it for initial charging");

            // foreach (var item in SkimmersInCharge)
            //{

            //בדיקה האם קיימת תחנה עם המספר תחנה שקיבלתי וא"כ לעשות אותו כעמדת טעינה אחת יותר תפוסה ולקחת ארת המיקום שלו ולתת את המיקום הנ"ל לרחפן.
            //
            //}

            Random random = new Random();    
            tempDrone.BatteryStatus =   random.Next(20, 40);
            return tempDrone;


        }

        public static Customer AddCustomer()
        {
            Customer tempCustomer = new Customer();

            Console.WriteLine("Enter customer id:");
            tempCustomer.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Customer name:");
            tempCustomer.Name = Console.ReadLine();

            Console.WriteLine("Enter customer phoneNumber:");
            tempCustomer.PhoneNumber = Console.ReadLine();

            return tempCustomer;

        }

        public static Parcel AddShippingPackage()
        {
            Parcel tempPackage = new Parcel();

            Console.WriteLine("Enter send Customer id");
            tempPackage.CustomerSendsFrom.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter recieve customer id:");
            tempPackage.CustomerReceivesTo.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the package weight: 1- Light, 2- medium, 3- weighty");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        tempPackage.WeightPackage = Enums.WeightCategories.Light;
                        break;
                    }
                case 2:
                    {
                        tempPackage.WeightPackage = Enums.WeightCategories.medium;
                        break;
                    }
                case 3:
                    {
                        tempPackage.WeightPackage = Enums.WeightCategories.weighty;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Input! Enter choice again:");
                        break;
                    }
            }
            Console.WriteLine("Enter  the package Priority: 1-Regular,2-Fast,3-Emergency");
             choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        tempPackage.Priority = Enums.Priorities.Regular;
                        break;
                    }
                case 2:
                    {
                        tempPackage.Priority = Enums.Priorities.Fast;
                        break;
                    }
                case 3:
                    {
                        tempPackage.Priority = Enums.Priorities.Emergency;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Input! Enter choice again:");
                        break;
                    }
            }
            return tempPackage;
        }

        public void UpdateBaseStation(BaseStation baseStationForChange)
        {
            Console.WriteLine("Enter the baseStation id:");
            int tempId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter a new name:");
            string name = Console.ReadLine();


        }

    }
}
