﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Bl;

namespace ConsoleUI_BL
{
    partial class Program
    {
        enum Menu { Add, Update, Display, DisplayList, Exit , False }
        enum Add { Station, Drone, Customer, Parcel,False}
        enum Update { DroneName, StationDetails, CustomerDedails, SendDroneForCharg, RealsDroneFromChargh, AssingParcelToDrone, CollectParcelByDrone, SupplyParcelToDestination ,False }
        enum DisplayList { Sations, Drones, Customers, Parcels, ParcelNotAssignToDrone, AvailableChargingSations ,False}
        enum Display { Station, Drone, Customer, Parcel ,False}
        private static readonly BlApi.IBL bal = BlApi.BlFactory.GetBL();

        static void Main()
        {
            Menu option;
            do
            {
                DisplayMenus(typeof(Menu));
                if (!Enum.TryParse(Console.ReadLine(), out option))
                {
                    option = Menu.False;
                }

                try
                {
                    switch (option)
                    {
                        case Menu.Add:
                            {
                                DisplayMenus(typeof(Add));
                                SwitchAdd(bal);
                                break;
                            }

                        case Menu.Update:
                            {

                                DisplayMenus(typeof(Update));
                                SwitchUpdate(bal);
                                break;
                            }
                        case Menu.Display:
                            {
                                DisplayMenus(typeof(Display));
                                ReplacingDisplay(bal);
                                break;
                            }
                        case Menu.DisplayList:
                            {
                                DisplayMenus(typeof(DisplayList));
                                ReplacingDisplayList(bal);
                                break;
                            }
                        case Menu.Exit:
                            break;
                        case Menu.False:
                            Console.WriteLine("Invalid input!");
                            break;
                    }
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message == string.Empty ? ex : ex.Message);
                }
                catch (Exception_NotExistCloseStationForTheDrone ex)
                {
                    Console.WriteLine(ex);
                }
                catch (Exception_ThereIsInTheListObjectWithTheSameValue ex)
                {
                    Console.WriteLine(ex);
                }
                catch (ArgumentNullException ex)
                {

                    Console.WriteLine(ex.Message == string.Empty ? ex : ex.Message);
                }
                catch (InvalidEnumArgumentException ex)
                {

                    Console.WriteLine(ex.Message == string.Empty ? ex : ex.Message);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message == string.Empty ? ex : ex.Message);
                }
            } while (option != Menu.Exit);
        }
        /// <summary>
        /// gets enum and prints his values
        /// </summary>
        /// <param name="en"> type of enum</param>
        static public void DisplayMenus(Type en)
        {
            int idx = 0;
            foreach (var item in Enum.GetValues(en))
            {
                string tmp = item.ToString();
                if (tmp == "False")
                    continue;
                Console.WriteLine(tmp);
                Console.WriteLine(" press " + idx++);
            }
        }
    }
}




