using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
       
        public static void SwitchDisplay( IBL.IBL bl)
        {
     
           if (!Enum.TryParse(Console.ReadLine(), out Display option))
            {
                Console.WriteLine("The input is invalid");
                option = Display.False;
            }
            int id;
            switch (option)
            {
                case Display.Station:
                    {
                        Console.WriteLine("Enter the ststion ID:");
                        if (int.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine(bl.GetStation(id));
                        break;
                    }
                case Display.Drone:
                    {
                        Console.WriteLine("Enter the Drone ID: mk");
                        if (int.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine(bl.GetDrone(id));
                        break;
                    }
                case Display.Customer:
                    {
                        Console.WriteLine("Enter the customer ID:");
                        if (int.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine(bl.GetCustomer(id));
                        break;
                    }
                case Display.Parcel:
                    {
                        Console.WriteLine("Enter the parcel ID:");
                        if (int.TryParse(Console.ReadLine(), out id))
                            Console.WriteLine(bl.GetParcel(id));
                        break;
                    }
                case Display.False:
                    break;
              

            }


        }

    }
}
