using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;
using static DalObject.DataSource.Config;

namespace DalObject
{
    public class DalObject
    {
        static Random ran = new Random();

        /// <summary>
        /// DalObject constructor call to intilialize
        /// </summary>
        public DalObject()
        {
            Initalize();  
        }

        /// <summary>
        /// AddBaseStation is a method in the DalObject class.
        /// the method adds a new base station
        /// </summary>
        public void InsertStation(Stations station)
        {
            DataSource.BaseStations[DataSource.Config.NumOfBaseStations++] = station;
        }

        /// <summary>
        /// AddDrone is a method in the DalObject class.
        /// the method adds a new drone
        /// </summary>
        public void InsertDrone(Drone drone)
        {
            DataSource.Drones[DataSource.Config.NumOfDrons++] = drone;
        }

        /// <summary>
        /// AddParcel is a method in the DalObject class.
        /// the method adds a new parcel
        /// </summary>
        /// <param name="senderId">the first int value</param>
        /// <param name="targetId">the second int value</param>
        /// <param name="weight">the 3th WeightCategories value</param>
        /// <param name="priority">the 4th Priorities value</param>
        public void InsertParcel(Parcel parcel)
        {
            DataSource.Parcels[DataSource.Config.NumOfParcels++] = parcel;
        }

        /// <summary>
        /// AddCustomer is a method in the DalObject class.
        /// the method adds a new customer
        /// </summary>
        /// <param name="name">the first string value</param>
        /// <param name="phone">the second string value</param>
        /// <param name="longitude">the 3th double value</param>
        /// <param name="latitude">the 4th double value</param>
        public void InsertCustomer(Customer customer)
        {
            DataSource.Customers[DataSource.Config.NumOfCustomers++] = customer;
        }
        /// <summary>
        /// UpdateScheduled is a method in the DalObject class.
        /// the method assigns a package to the drone
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateScheduled(int id)
        {
            checkValid(id, 0, NumOfParcels);
            for (int i = 0; i < Drones.Length; i++)
            {
                if ((Drones[i].MaxWeight >= Parcels[NumOfParcels].Weight) && (Drones[i].Battery >= 30) && Drones[i].Status == 0)
                {
                    Parcels[id].DroneId = Drones[i].Id;
                    Parcels[id].Scheduled = DateTime.Now;
                    return;
                }
            }
        }
        /// <summary>
        /// UpdatePickedUp is a method in the DalObject class.
        /// the method updates package assembly by drone
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdatePickedUp(int id)
        {
            checkValid(id, 0, NumOfParcels);
            Parcels[id].PickedUp = DateTime.Now;
            Drones[Parcels[id].DroneId - 1].Status = (DroneStatuses)2;
        }
        /// <summary>
        /// UpdateSupply is a method in the DalObject class.
        /// the method updates delivery of a package to the destination
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateSupply(int id)
        {
            checkValid(id, 0, NumOfParcels);
            Parcels[id].Delivered = DateTime.Now;
            Drones[Parcels[id].DroneId - 1].Status = (DroneStatuses)0;
        }
        /// <summary>
        /// UpdateCharge is a method in the DalObject class.
        /// the method updates sending a skimmer for charging at a base station
        /// </summary>
        /// <param name="id">int value</param>
        /// <returns>if succeed in finding available charge slot</returns>
        public bool UpdateCharge(int id)
        {
            checkValid(id, 1, NumOfDrons + 1);
            Drones[id].Status = (DroneStatuses)1;
            return FindChargeSlot(id);
        }
        /// <summary>
        /// UpdateRelease is a method in the DalObject class.
        /// the method removes a drone from charging at a base station
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateRelease(int id)
        {
            checkValid(id, 1, NumOfDrons + 1);
            Drones[id - 1].Status = 0;
            int sum = -1;
            foreach (DroneCharge item in DroneCharges)
            {
                sum++;
                if (item.DroneId == id)
                {
                    break;
                }
            }
            DroneCharges.RemoveRange(sum, 1);
        }

        /// <summary>
        /// DisplayBaseStation is a method in the DalObject class.
        /// the method allows base station view
        /// </summary>
        public void DisplayBaseStation()
        {
            Console.WriteLine("enter base station id:");
            int input;
            Valid2(0, NumOfBaseStations, out input);
            Console.WriteLine(BaseStations[input]);
        }
        /// <summary>
        /// DisplayDrone is a method in the DalObject class.
        /// the method allows drone display
        /// </summary>
        public void DisplayDrone()
        {
            Console.WriteLine("enter drone id:");
            int input;
            Valid2(1, NumOfDrons + 1, out input);
            Console.WriteLine(Drones[input]);
        }
        /// <summary>
        /// DisplayCustomer is a method in the DalObject class.
        /// the method allows customer view
        /// </summary>
        public void DisplayCustomer()
        {
            Console.WriteLine("enter customer id:");
            int input;
            Valid2(0,NumOfCustomers, out input);
            Console.WriteLine(Customers[input]);
        }
        /// <summary>
        /// DisplayParcel is a method in the DalObject class.
        /// the method allows parcel view
        /// </summary>
        public void DisplayParcel()
        {
            Console.WriteLine("enter parcel id:");
            int input;
            Valid2(0, NumOfParcels, out input);
            Console.WriteLine(Parcels[input]);
        }
        /// <summary>
        /// ViewListBaseStations is a method in the DalObject class.
        /// the method displays a list of base stations
        /// </summary>
        public void ViewListBaseStations()
        {
            foreach (Stations item in BaseStations)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// ViewListDrones is a method in the DalObject class.
        /// the method displays the list of drones
        /// </summary>
        public void ViewListDrones()
        {
            foreach (Drone item in Drones)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// ViewListParcels is a method in the DalObject class.
        /// the method displays View the customer list
        /// </summary>
        public void ViewListCustomers()
        {
            foreach (Customer item in Customers)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// ViewListCustomers is a method in the DalObject class.
        /// the method displays the list of parcels
        /// </summary>
        public void ViewListParcels()
        {
            foreach (Parcel item in Parcels)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// ViewListPendingParcels is a method in the DalObject class.
        /// the method displays a list of packages that have not yet been assigned to the drone
        /// </summary>
        public void ViewListPendingParcels()
        {
            foreach (Parcel item in Parcels)
            {
                if (item.DroneId == 0)
                {
                    Console.WriteLine(item);
                }
            }
        }
        /// <summary>
        /// ViewListAvailableChargeSlots is a method in the DalObject class.
        /// the method  display base stations with available charging stations
        /// </summary>
        public void ViewListAvailableChargeSlots()
        {
            foreach (Stations item in BaseStations)
            {
                int sum = 0;
                foreach (DroneCharge item1 in DroneCharges)
                {
                    if (item1.DroneId == item.Id)
                        sum++;
                    if (item1.DroneId > item.Id)
                        break;
                }
                if (sum < item.ChargeSlots)
                    Console.WriteLine(item);
            }
        }
        /// <summary>
        /// Valid is a static method in the DalObject class.
        /// the helper method check if input is valid 
        /// </summary>
        /// <param name="input">out int value</param>
        public static void Valid(out int input)
        {
            bool parse = int.TryParse(Console.ReadLine(), out input);
            while (!parse)
            {
                Console.WriteLine("not valid input! please enter again");
                parse = int.TryParse(Console.ReadLine(), out input);
            }
        }
        /// <summary>
        /// Valid2 is a static method in the DalObject class.
        /// the helper method check if input is valid in range 
        /// </summary>
        /// <param name="min">the first int value</param>
        /// <param name="max">second int vaalue</param>
        /// <param name="input">3th bout int value</param>
        public static void Valid2(int min, int max, out int input)
        {
            bool parse = int.TryParse(Console.ReadLine(), out input);
            while (!parse || input > max || input < min)
            {
                Console.WriteLine("not valid input! please enter again");
                parse = int.TryParse(Console.ReadLine(), out input);
            }
        }
        /// <summary>
        /// AddONeBaseStation is a static method in the DalObject class.
        /// the method adds a new base station
        /// </summary>
        public static void AddONeBaseStation()
        {
            BaseStations[NumOfBaseStations] = new Stations();
            BaseStations[NumOfBaseStations].Id = NumOfBaseStations;
            BaseStations[NumOfBaseStations].Name =  NumOfBaseStations;
            BaseStations[NumOfBaseStations].Longitude = ran.Next(0, 90);
            BaseStations[NumOfBaseStations].Lattitude = ran.Next(0, 180);
            int chargeSlots = ran.Next(3, 8);
            BaseStations[NumOfBaseStations].ChargeSlots = chargeSlots;
            NumOfBaseStations++;
        }
        /// <summary>
        /// AddOneDrone is a static method in the DalObject class.
        /// the method adds a new drone
        /// </summary>
        public static bool AddOneDrone()
        {
            bool find = true;
            Drones[NumOfDrons] = new Drone();
            Drones[NumOfDrons].Id = NumOfDrons + 1;
            Drones[NumOfDrons].Model = $"model_{ran.Next(9000, 9011)}";
            Drones[NumOfDrons].MaxWeight = (WeightCategories)ran.Next(0, 3);
            Drones[NumOfDrons].Battery = ran.Next(0, 101);
            Drones[NumOfDrons].Status = (DroneStatuses)ran.Next(0, 3);
            if (Drones[NumOfDrons].Status == (DroneStatuses)1)
            {
                find = FindChargeSlot(NumOfDrons);
            }
            NumOfDrons++;
            return find;
        }
        /// <summary>
        /// AddOneParcel is a static method in the DalObject class.
        /// the method adds a new parcel
        /// </summary>
        public static void AddOneParcel()
        {
            Parcels[NumOfParcels].Id = NumOfParcels;
            Parcels[NumOfParcels].Requested = DateTime.Now;
            Parcels[NumOfParcels].DroneId = 0;
            Parcels[NumOfParcels].Scheduled = new DateTime();
            Parcels[NumOfParcels].PickedUp = new DateTime();
            Parcels[NumOfParcels].Delivered = new DateTime();
            NumOfParcels++;
        }

        /// <summary>
        /// checkValid is a static method in the DalObject class.
        /// the helper method check if input is valid if not the func throws exception 
        /// </summary>
        /// <param name="id">the first int value</param>
        /// <param name="min">the second int value</param>
        /// <param name="max">3th out int value</param>
        public void checkValid(int id, int min, int max)
        {
            if (id < min || id >= max)
            {
                throw new FormatException();
            }
        }

        /// <summary>
        /// FindChargeSlot is a static method in the DalObject class.
        /// the method finds charge slot to a drone
        /// </summary>
        /// <param name="id">int value</param>
        /// <returns>if succeed in finding charge slot to a drone</returns>
        public static bool FindChargeSlot(int id)
        {
            foreach (Stations item in BaseStations)
            {
                int sum_chargeSlots = 0;
                foreach (DroneCharge item2 in DroneCharges)
                {
                    if (item2.DroneId == item.Id)
                        sum_chargeSlots++;
                    if (item2.DroneId > item.Id)
                        break;
                }
                if (sum_chargeSlots < item.ChargeSlots)
                {
                    DroneCharges.Add(new DroneCharge(id, item.Id));
                    DroneCharges.Sort();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Valid is a static method in the DalObject class.
        /// the helper method check if input is valid 
        /// </summary>
        /// <param name="input">out int value</param>
        
    }


}

