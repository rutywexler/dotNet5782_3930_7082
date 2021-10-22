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
        public static Stations getStation()
        {
            int tempID, tempChargeSlots, tempName;
            double tempLongitude, tempLattitude;
            Stations tempStation = new Stations();
            Console.Write("Enter The Id Of The Station:");
            tempID = int.Parse(Console.ReadLine());
            tempStation.Id = tempID;

            Console.Write("Enter The Name Of The Station:");
            tempName = int.Parse(Console.ReadLine());
            tempStation.Name = tempName;

            Console.Write("Enter longitude");
            tempLongitude = double.Parse(Console.ReadLine());
            tempStation.Longitude = tempLongitude;

            Console.Write("Enter lattitude");
            tempLattitude = double.Parse(Console.ReadLine());
            tempStation.Lattitude = tempLattitude;

            Console.Write("Enter The Number Of The Charging Stations:");
            tempChargeSlots = int.Parse(Console.ReadLine());
            tempStation.ChargeSlots= tempChargeSlots;
            return tempStation;
        }
    }
}


