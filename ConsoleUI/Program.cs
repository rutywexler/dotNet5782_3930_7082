using System;
using IDAL.DO;




namespace ConsoleUI
{
    class Program
    {
        public enum Options { Insert, Update, Display, ViewTheLists, Exit };
        public enum Insert { Station, Drone, Customer, Parcel };
        public enum Update { AssigningParcelToDrone, CollectParcel, ParcelSupply, SendingDroneforCharging, ReleasingDroneFromCharging };
        public enum Display { BaseStation, Drone, Customer, Parcel };
        public enum ViewTheLists { baseStations, Drone, Customers, Parcel, ParcelNotDrone, FreeBaseStations }

        
        static void Main(string[] args)
        {
            DalObject.DalObject dalObject = new DalObject.DalObject();
            Options choice;
            int tryInput;
            do
            {
                foreach (Options item in Enum.GetValues(typeof(Options)))
                {
                    Console.WriteLine($"{(int)item} - {item}");
                }
                DalObject.DalObject.Valid(out tryInput);
                choice = (Options)tryInput;
                switch (choice)
                {
                    case Options.Insert:
                        foreach (Insert item in Enum.GetValues(typeof(Insert)))
                        {
                            Console.WriteLine($"{(int)item} - {item}");
                        }
                        InsertOption((Insert)choice);
                        break;
                    case Options.Update:
                        foreach (Update item in Enum.GetValues(typeof(Update)))
                        {
                            Console.WriteLine($"{(int)item} - {item}");
                        }
                        UpdateOption((Update)choice);
                        break;
                    case Options.Display:
                        foreach (Display item in Enum.GetValues(typeof(Display)))
                        {
                            Console.WriteLine($"{(int)item} - {item}");
                        }
                        DisplayOption((Display)choice);
                        break;
                    case Options.ViewTheLists:
                        foreach (ViewTheLists item in Enum.GetValues(typeof(ViewTheLists)))
                        {
                            Console.WriteLine($"{(int)item} - {item}");
                        }
                        ViewTheListsOption((ViewTheLists)choice);
                        break;



                }

            } while (choice != Options.Exit);

            void InsertOption(Insert choice)
            {
                switch (choice)
                {
                    case Insert.Station:
                        dalObject.InsertStation(InputFunctions.GetStation());
                        break;
                    case Insert.Drone:
                        dalObject.InsertDrone(InputFunctions.GetDrone());
                        break;
                    case Insert.Customer:
                        dalObject.InsertCustomer(InputFunctions.GetCustomer());
                        break;
                    case Insert.Parcel:
                        dalObject.InsertParcel(InputFunctions.GetParcel());
                        break;
                    default:
                        break;
                }
            }

            void UpdateOption(Update choice)
            {
                switch (choice)
                {
                    case Update.AssigningParcelToDrone:
                        {
                            Console.WriteLine("enter parcel id");
                            int id;
                            DalObject.DalObject.Valid(out id);
                            dalObject.UpdateScheduled(id);
                            break;
                        }
                    case Update.CollectParcel:
                        {
                            Console.WriteLine("enter parcel id");
                            int id;
                            DalObject.DalObject.Valid(out id);
                            dalObject.UpdatePickedUp(id);
                            break;
                        }
                    case Update.ParcelSupply:
                        {
                            Console.WriteLine("enter parcel id");
                            int id;
                            DalObject.DalObject.Valid(out id);
                            dalObject.UpdateSupply(id);
                            break;
                        }
                    case Update.SendingDroneforCharging:
                        {
                            Console.WriteLine("enter drone id");
                            int id;
                            DalObject.DalObject.Valid(out id);
                            if (!dalObject.UpdateCharge(id))
                            {
                                Console.WriteLine("there wasn't an available charge slot");
                            }
                            break;
                        }
                    case Update.ReleasingDroneFromCharging:
                        {
                            Console.WriteLine("enter drone id");
                            int id;
                            DalObject.DalObject.Valid(out id);
                            dalObject.UpdateRelease(id);
                            break;
                        }
                    default:
                        throw new FormatException();
                }
            }

            void DisplayOption(Display choice)
            {

            }
            void ViewTheListsOption(ViewTheLists choice)
            {

            }



        }
    }
    
    
}
