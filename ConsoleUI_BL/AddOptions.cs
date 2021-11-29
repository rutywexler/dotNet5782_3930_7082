using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using IBL.BO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;

namespace ConsoleUI_BL
{
    partial class Program
    {

        private const int LATITUDE_MAX = 90;
        private const int LATITUDE_MIN = -90;
        private const int LONGITUDE_MAX = 90;
        private const int LONGITUDE_MIN = 0;
        /// <summary>
        /// Receives input from the user what type of organ to print as well as ID number and calls to the appropriate adding method
        /// </summary>
        /// <param name="dalObject"></param>
        public static void SwitchAdd(IBL.IBL bl)
        {
            Add option;
            try
            {
                Enum.TryParse(Console.ReadLine(), out option);
            }
            catch (Exception)
            {

                throw new FormatException();
            }

            int id;
            switch (option)
            {
                case Add.Station:
                    {

                        Console.WriteLine("Enter details for the base station:id,lattitude,longitude,chargeSlots, name");
                        if (int.TryParse(Console.ReadLine(), out id) && double.TryParse(Console.ReadLine(), out double latitude) && double.TryParse(Console.ReadLine(), out double longitude) && int.TryParse(Console.ReadLine(), out int chargeslots))
                        {
                            if (latitude > LATITUDE_MAX || latitude < LATITUDE_MIN)
                            {
                                Console.WriteLine("invalid latitude");
                                break;
                            }
                            if (longitude > LONGITUDE_MAX || longitude < LONGITUDE_MIN)
                            {
                                Console.WriteLine("invalid longitude");
                                break;
                            }
                            if (chargeslots < 0)
                            {
                                Console.WriteLine("invalid charge slots");
                                break;
                            }
                            string name = Console.ReadLine();
                            Location location = new();
                            location.Longitude = longitude;
                            location.Lattitude = latitude;
                            BaseStation station = new() { Id = id, Name = name, Location = location, NumberOfChargingStations = chargeslots, DronesInCharge = new List<DroneInCharging>() };
                            bl.AddStation(station);
                        }
                        else
                            Console.WriteLine("There were errors in the data entry and the addition was not made");
                        break;
                    }
                case Add.Drone:
                    {
                        Console.WriteLine("Enter details for the drone:id,wheight,station id,model");
                        if (int.TryParse(Console.ReadLine(), out id) && Enum.TryParse(Console.ReadLine(), out WeightCategories MaxWeight) && int.TryParse(Console.ReadLine(), out int stationId))
                        {
                            if ((int)MaxWeight < 0)
                            {
                                Console.WriteLine("invalid max weight");
                                break;
                            }
                            string Model = Console.ReadLine();
                            bl.AddDrone(id, Model, MaxWeight, stationId);
                        }
                        else
                            Console.WriteLine("There were errors in the data entry and the addition was not made");

                        break;
                    }
                case Add.Customer:
                    {
                        Console.WriteLine("Enter details for the customer:id,latitude,longitude, name, phone");
                        if (int.TryParse(Console.ReadLine(), out id) && double.TryParse(Console.ReadLine(), out double latitude) && double.TryParse(Console.ReadLine(), out double longitude))
                        {
                            Location location = new();
                            location.Longitude = longitude;
                            location.Lattitude = latitude;
                            string name = Console.ReadLine();
                            string phone;
                            if (latitude > 90 || latitude < -90)
                            {
                                Console.WriteLine("invalid latitude");
                                break;
                            }
                            if (longitude > 90 || longitude < 0)
                            {
                                Console.WriteLine("invalid longitude");
                                break;
                            }

                            Console.WriteLine("Enter phone");
                            phone = Console.ReadLine();
                            if (!Regex.Match(phone, @"^((?:\+?)[0-9]{10})$").Success)
                            {
                                Console.WriteLine("invalid phone");
                                break;
                            }
                            bl.AddCustomer(id,name,phone,location);
                        }
                        else
                            Console.WriteLine("There were errors in the data entry and the addition was not made");
                        break;
                    }
                case Add.Parcel:
                    {
                        Console.WriteLine("Enter details for the parcel : sender id,target id,weigth,priority");

                        if (int.TryParse(Console.ReadLine(), out int senderId) && int.TryParse(Console.ReadLine(), out int targetId) && Enum.TryParse(Console.ReadLine(), out WeightCategories weigth) && Enum.TryParse(Console.ReadLine(), out Priorities priority))
                        {
                            if ((int)weigth < 0)
                            {
                                Console.WriteLine("invalid weight, weight range is 0-2");
                                break;
                            }
                            if ((int)priority < 0)
                            {
                                Console.WriteLine("invalid priority, priority range is 0-2");
                                break;
                            }
                            bl.AddParcel(new Parcel()
                            {
                                CustomerReceivesTo = new CustomerInParcel()
                                {
                                    Id = targetId
                                },
                                CustomerSendsFrom = new CustomerInParcel()
                                {
                                    Id = senderId
                                },
                                WeightParcel = weigth,
                                Priority = priority

                            });
                        }
                        else
                            Console.WriteLine("There were errors in the data entry and the addition was not made");

                        break;
                    }

                default:
                    break;

            }
        }
    }
}
