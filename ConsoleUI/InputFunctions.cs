using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class InputFunctions
    {
        //get
        public static Stations GetStation()
        {
            Stations tempStation = new Stations();
            Console.Write("Enter The Id Of The Station:");
            tempStation.Id = int.Parse(Console.ReadLine());

            Console.Write("Enter The Name Of The Station:");
            tempStation.Name = int.Parse(Console.ReadLine());
           
            Console.Write("Enter longitude");
            tempStation.Longitude = double.Parse(Console.ReadLine());
            
            Console.Write("Enter lattitude");
            tempStation.Lattitude = double.Parse(Console.ReadLine());
            
            Console.Write("Enter The Number Of The Charging Stations:");
            tempStation.ChargeSlots = int.Parse(Console.ReadLine());
            return tempStation;
        }

        public static Drone getDrone()
        {
            int Choice;
            Drone tempDrone = new Drone();

            Console.Write("Enter The Id Of The Drone:");
            tempDrone.Id = int.Parse(Console.ReadLine());
             
            Console.Write("Enter The Model Of The Drone:");
            tempDrone.Model = Console.ReadLine();
          
            Console.Write("Enter max weight, 1-medium,2-heavy,3-light");
            Choice = int.Parse(Console.ReadLine());
            tempDrone.MaxWeight = (WeightCategories)Choice;

            Console.Write("Enter The BatteryStatus Of The Drone:");
            tempDrone.Battery = double.Parse(Console.ReadLine());

            Console.Write("Enter status, 1-available,2- maintenance,3-delivery");
            Choice = int.Parse(Console.ReadLine());
            tempDrone.Status = (DroneStatuses)Choice;
            return tempDrone;
        }
        public static Parcel GetParcel()
        {
            Parcel tempParcel = new Parcel();
            Console.WriteLine("Enter The Id Of The Parcel:");
            tempParcel.Id= int.Parse(Console.ReadLine());
            Console.WriteLine("Enter The Id Of The Sender:");

            return tempParcel;
        }
    }
}

    

