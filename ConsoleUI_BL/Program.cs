using System;

namespace ConsoleUI_BL
{
    partial class Program
    {
        enum Menu { Add, Update, Display, DisplayList, Exit , False}
        enum Add { Station, Drone, Customer, Parcel ,False }
        enum Update { DroneName, StationDetails, CustomerDedails, SendDroneForCharg, RealsDroneFromChargh, AssingParcelToDrone, CollectParcelByDrone, SupplyParcelToDestination , False }
        enum DisplayList { Sations, Drones, Customers, Parcels, ParcelNotAssignToDrone, AvailableChargingSations, False }
        enum Display { Station, Drone, Customer, Parcel, False }

        static void Main()
        {
            IBL.IBL bl = new IBL.BL();
            Menu option;
            do
            {
                DisplayMenus(typeof(Menu));
                try
                {
                    Enum.TryParse(Console.ReadLine(), out option);
                }
                catch (Exception)
                {

                    throw new FormatException();
                }
                try
                {
                    switch (option)
                    {
                        case Menu.Add:
                            {
                                DisplayMenus(typeof(Add));
                                SwitchAdd(bl);
                                break;
                            }

                        case Menu.Update:
                            {

                                DisplayMenus(typeof(Update));
                                SwitchUpdate(ref bl);
                                break;
                            }
                        case Menu.Display:
                            {
                                DisplayMenus(typeof(Display));
                                SwitchDisplay(ref bl);
                                break;
                            }
                        case Menu.DisplayList:
                            {
                                DisplayMenus(typeof(DisplayList));
                                SwitchDisplayList(ref bl);
                                break;
                            }
                        case Menu.Exit:
                            break;
                        default:
                            break;
                    }
                }

            } while (option != Menu.Exit);
        }
        /// <summary>
        /// gets enum and prints his values
        /// </summary>
        /// <param name="enum"> type of enum</param>
        static public void DisplayMenus(Type enumOption)
        {
            int index = 0;
            foreach (var item in Enum.GetValues(enumOption))
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine(" press " + index++);
            }

        }
    }
}

