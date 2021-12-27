using System;
//using DalApi.DO;



//לא צריך אותו לתרגיל 2
//namespace ConsoleUI
//{
//    class Program
//    {
//        public enum Options { Insert, Update, Display, ViewTheLists, Exit };
//        public enum Insert { Station, Drone, Customer, Parcel };
//        public enum Update { AssigningParcelToDrone, CollectParcel, ParcelSupply, SendingDroneforCharging, ReleasingDroneFromCharging };
//        public enum Display { BaseStation, Drone, Customer, Parcel };
//        public enum ViewTheLists { baseStations, Drone, Customers, Parcel, ParcelNotDrone, FreeBaseStations }

        
        //static void Main(string[] args)
        //{
//            DalObject.DalObject dalObject = new DalObject.DalObject();
//            Options choice;
//            int tryInput;
//            do
//            {
//                foreach (Options item in Enum.GetValues(typeof(Options)))
//                {
//                    Console.WriteLine($"{(int)item} - {item}");
//                }
//                DalObject.DalObject.Valid(out tryInput);
//                choice = (Options)tryInput;
//                switch (choice)
//                {
//                    case Options.Insert:
//                        foreach (Insert item in Enum.GetValues(typeof(Insert)))
//                        {
//                            Console.WriteLine($"{(int)item} - {item}");
//                        }
//                        DalObject.DalObject.Valid(out tryInput);
//                        Insert insert = (Insert)tryInput;
//                        InsertOption(insert);
//                        break;
//                    case Options.Update:
//                        foreach (Update item in Enum.GetValues(typeof(Update)))
//                        {
//                            Console.WriteLine($"{(int)item} - {item}");
//                        }
//                        DalObject.DalObject.Valid(out tryInput);
//                        Update update = (Update)tryInput;
//                        UpdateOption(update);
//                        break;
//                    case Options.Display:
//                        foreach (Display item in Enum.GetValues(typeof(Display)))
//                        {
//                            Console.WriteLine($"{(int)item} - {item}");
//                        }
//                        DalObject.DalObject.Valid(out tryInput);
//                        Display display = (Display)tryInput;
//                        DisplayOption(display);
//                        break;
//                    case Options.ViewTheLists:
//                        foreach (ViewTheLists item in Enum.GetValues(typeof(ViewTheLists)))
//                        {
//                            Console.WriteLine($"{(int)item} - {item}");
//                        }
//                        DalObject.DalObject.Valid(out tryInput);
//                        ViewTheLists ViewTheLists = (ViewTheLists)tryInput;
//                        ViewTheListsOption(ViewTheLists);
//                        break;
//                    case Options.Exit:
//                        break;
//                    default:
//                        throw new FormatException();



//                }

//            } while (choice != Options.Exit);

//            void InsertOption(Insert choice)
//            {
//                switch (choice)
//                {
//                    case Insert.Station:
//                        dalObject.InsertStation(InputFunctions.GetStation());
//                        break;
//                    case Insert.Drone:
//                        dalObject.InsertDrone(InputFunctions.GetDrone());
//                        break;
//                    case Insert.Customer:
//                        dalObject.InsertCustomer(InputFunctions.GetCustomer());
//                        break;
//                    case Insert.Parcel:
//                        InputFunctions.GetParcel());
//                        break;
//                    default:
//                        break;
//                }
//            }

//            void UpdateOption(Update choice)
//            {
//                switch (choice)
//                {
//                    case Update.AssigningParcelToDrone:
//                        {
//                            Console.WriteLine("enter parcel id");
//                            int id;
//                            DalObject.DalObject.Valid(out id);
//                            dalObject.UpdateScheduled(id);
//                            break;
//                        }
//                    case Update.CollectParcel:
//                        {
//                            Console.WriteLine("enter parcel id");
//                            int id;
//                            DalObject.DalObject.Valid(out id);
//                            dalObject.UpdatePickedUp(id);
//                            break;
//                        }
//                    case Update.ParcelSupply:
//                        {
//                            Console.WriteLine("enter parcel id");
//                            int id;
//                            DalObject.DalObject.Valid(out id);
//                            dalObject.UpdateSupply(id);
//                            break;
//                        }
//                    case Update.SendingDroneforCharging:
//                        {
//                            Console.WriteLine("enter drone id");
//                            int id;
//                            DalObject.DalObject.Valid(out id);
//                            if (!dalObject.UpdateCharge(id))
//                            {
//                                Console.WriteLine("there wasn't an available charge slot");
//                            }
//                            break;
//                        }
//                    case Update.ReleasingDroneFromCharging:
//                        {
//                            Console.WriteLine("enter drone id");
//                            int id;
//                            DalObject.DalObject.Valid(out id);
//                            dalObject.UpdateRelease(id);
//                            break;
//                        }
//                    default:
//                        throw new FormatException();
//                }
//            }

//            void DisplayOption(Display choice)
//            {
//                switch (choice)
//                {
//                    case Display.BaseStation:
//                        {
//                            dalObject.DisplayBaseStation();
//                            break;
//                        }
//                    case Display.Drone:
//                        {
//                            dalObject.DisplayDrone();
//                            break;
//                        }
//                    case Display.Customer:
//                        {
//                            dalObject.DisplayCustomer();
//                            break;
//                        }
//                    case Display.Parcel:
//                        {
//                            dalObject.DisplayParcel();
//                            break;
//                        };
//                    default:
//                        throw new FormatException();
//                }
//            }
//            void ViewTheListsOption(ViewTheLists choice)
//            {
//                switch (choice)
//                {
//                    case ViewTheLists.baseStations:
//                        {
//                            dalObject.ViewListBaseStations();
//                            break;
//                        }
//                    case ViewTheLists.Drone:
//                        {
//                            dalObject.ViewListDrones();
//                            break;
//                        }
//                    case ViewTheLists.Customers:
//                        {
//                            dalObject.ViewListCustomers();
//                            break;
//                        }
//                    case ViewTheLists.Parcel:
//                        {
//                            dalObject.ViewListParcels();
//                            break;
//                        }
//                    case ViewTheLists.ParcelNotDrone:
//                        {
//                            dalObject.ViewListPendingParcels();
//                            break;
//                        }
//                    case ViewTheLists.FreeBaseStations:
//                        {
//                            dalObject.ViewListAvailableChargeSlots();
//                            break;
//                        };
//                    default:
//                        throw new FormatException();
//                }
//            }



//        }
//    }
    
    
//}
