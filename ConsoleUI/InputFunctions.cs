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
        public static Station GetStation()
        {
            Station tempStation = new Station();
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

        public static Drone GetDrone()
        {
            Drone tempDrone = new Drone();

            Console.Write("Enter The Id Of The Drone:");
            tempDrone.Id = int.Parse(Console.ReadLine());
             
            Console.Write("Enter The Model Of The Drone:");
            tempDrone.Model = Console.ReadLine();
          
            Console.Write("Enter max weight, 1-medium,2-heavy,3-light");
            tempDrone.MaxWeight = (WeightCategories)(int.Parse(Console.ReadLine()));

            Console.Write("Enter The BatteryStatus Of The Drone:");
            tempDrone.Battery = double.Parse(Console.ReadLine());

            Console.Write("Enter status, 1-available,2- maintenance,3-delivery");
            tempDrone.Status = (DroneStatuses)(int.Parse(Console.ReadLine()));
            return tempDrone;
        }
        public static Parcel GetParcel()
        {
            Parcel tempParcel = new Parcel();
            Console.WriteLine("Enter The Id Of The Parcel:");
            tempParcel.Id= int.Parse(Console.ReadLine());

            Console.WriteLine("Enter The Id Of The Sender:");
            tempParcel.SenderId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter The Id Of The Getter: ");
            tempParcel.TargetId= int.Parse(Console.ReadLine());

            Console.WriteLine("Enter The Weight Of The Parcel: ");
            tempParcel.Weight = (WeightCategories)int.Parse(Console.ReadLine());

            Console.WriteLine("Enter The Status Of The Parcel:");
            foreach (Priorities item in Enum.GetValues(typeof(Priorities)))
            {
                Console.WriteLine($"{(int)item} - {item}");
            }
            tempParcel.Priority= (Priorities)int.Parse(Console.ReadLine());

            Console.WriteLine("Enter The DroneId Of The Parcel:");
            tempParcel.DroneId= int.Parse(Console.ReadLine());

            tempParcel.Scheduled = DateTime.Now;
            tempParcel.PickedUp = new DateTime();
            tempParcel.Delivered= new DateTime();
            tempParcel.Requested = new DateTime();
            return tempParcel;
        }

        public static Customer GetCustomer()
        {
            Customer tempCustomer = new Customer();
            Console.WriteLine("Enter The Id Of The Customer:");
            tempCustomer.Id= int.Parse(Console.ReadLine());
            Console.WriteLine("Enter The Name Of The Customer:");
            tempCustomer.Name = Console.ReadLine();
            Console.WriteLine("Enter The Phone Of The Customer:");
            tempCustomer.Phone = Console.ReadLine();
            Console.WriteLine("Enter The Longitude:");
            tempCustomer.Longitude = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Latitude:");
            tempCustomer.Lattitude = double.Parse(Console.ReadLine());
            return tempCustomer;
        }

    }
}

    

