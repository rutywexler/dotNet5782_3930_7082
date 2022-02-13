using System;



namespace ConsoleUI_BL
{
    partial class Program
    {
        /// <summary>
        /// /// Receiving input from the user: type of organ to print, ID number and calls to the appropriate update method
        /// <param name="dalObject"></param>
        public static void SwitchUpdate(BlApi.IBL bl)
        {
            if (!Enum.TryParse(Console.ReadLine(), out Update option))
            {
                option = Update.False;
            }

            int id;
            switch (option)
            {
                case Update.DroneName:
                    {
                        Console.WriteLine("Enter ID drone:");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Enter a new model-name:");
                            bl.UpdateDrone(id, Console.ReadLine());
                        }
                        else
                            Console.WriteLine("The alternative failed The update was not performed");
                        break;
                    }
                case Update.StationDetails:
                    {
                        Console.WriteLine("Enter ID drone");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("if you just want to update only one details press enter instead of enter an input");
                            Console.WriteLine("the new  number of charge slots ");
                            if (!int.TryParse(Console.ReadLine(), out int chargeSlots) || chargeSlots == default)
                                chargeSlots = 0;
                            if (chargeSlots < 0)
                            {
                                Console.WriteLine("invalid input!");
                            }
                            Console.WriteLine("Enter the new name:");
                            string name = Console.ReadLine();
                            bl.UpdateStation(id, name, chargeSlots);
                        }
                        else
                            Console.WriteLine("The alternative failed The update was not performed");
                        break;
                    }
                case Update.CustomerDedails:
                    {
                        Console.WriteLine("Enter customer ID");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("if you just want to update only one details press enter instead of enter an input");
                            Console.WriteLine("Enter the new name");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter the new phone-number");
                            string phone = Console.ReadLine();
                            bl.UpdateCustomer(id, name, phone);
                        }
                        else
                            Console.WriteLine("The alternative failed The update was not performed");
                        break;
                    }
                case Update.SendDroneForCharg:
                    {
                        Console.WriteLine("Enter drone ID");
                        if (int.TryParse(Console.ReadLine(), out id))
                            bl.SendDroneForCharge(id);
                        break;

                    }
                case Update.RealsDroneFromChargh:
                    {
                        Console.WriteLine("Enter drone ID");
                        if (int.TryParse(Console.ReadLine(), out id) )
                            bl.ReleaseDroneFromCharging(id);
                        break;
                    }
                case Update.AssingParcelToDrone:
                    {
                        Console.WriteLine("Enter drone ID");
                        if (int.TryParse(Console.ReadLine(), out id))
                            bl.AssignParcelToDrone(id);
                        break;
                    }
                case Update.CollectParcelByDrone:
                    {
                        Console.WriteLine("Enter drone ID:");
                        if (int.TryParse(Console.ReadLine(), out id))
                            bl.ParcelCollectionByDrone(id);
                        break;
                    }
                case Update.SupplyParcelToDestination:
                    {
                        Console.WriteLine("Enter the drone ID:");
                        if (int.TryParse(Console.ReadLine(), out id))
                            bl.DeliveryParcelByDrone(id);
                        break;
                    }
                case Update.False:
                    Console.WriteLine("The convertion faild  therefore the no option choose ");
                    break;

            }
        }
    }
}

