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
        public void InsertStation(Stations station) => BaseStations.Add(station);

        /// <summary>
        /// AddDrone is a method in the DalObject class.
        /// the method adds a new drone
        /// </summary>
        public void InsertDrone(Drone drone) => Drones.Add(drone);

        /// <summary>
        /// AddParcel is a method in the DalObject class.
        /// the method adds a new parcel
        /// </summary>
        public void InsertParcel(Parcel parcel) => Parcels.Add(parcel);

        /// <summary>
        /// AddCustomer is a method in the DalObject class.
        /// the method adds a new customer
        /// </summary>
        public void InsertCustomer(Customer customer) => Customers.Add(customer);

        /// <summary>
        /// UpdateScheduled is a method in the DalObject class.
        /// the method assigns a package to the drone
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateScheduled(int id)
        {
            checkValid(id, 0, Parcels.Count);
            foreach (Drone drone in Drones)
            {
                if ((drone.MaxWeight >= Parcels[Parcels.Count].Weight) && (drone.Battery >= 30) && drone.Status == 0)
                {
                    Parcel parcelTemp = Parcels[id];
                    parcelTemp.DroneId = drone.Id;
                    parcelTemp.Scheduled = DateTime.Now;
                    Parcels[id] = parcelTemp;
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
            checkValid(id, 0, Parcels.Count);
            Parcel tempParcel = Parcels[id];
            tempParcel.PickedUp = DateTime.Now;
            Parcels[id] = tempParcel;
            Drone droneTemp = Drones[Parcels[id].DroneId - 1];
            droneTemp.Status = (DroneStatuses)2;
            Drones[Parcels[id].DroneId - 1] = droneTemp;
        }
        /// <summary>
        /// UpdateSupply is a method in the DalObject class.
        /// the method updates delivery of a package to the destination
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateSupply(int id)
        {
            checkValid(id, 0, Parcels.Count);
            Parcel tempParcel = Parcels[id];
            tempParcel.Delivered = DateTime.Now;
            Parcels[id] = tempParcel;
            Drone tempDrone = Drones[Parcels[id].DroneId - 1];
            tempDrone.Status =0;
        }
        /// <summary>
        /// UpdateCharge is a method in the DalObject class.
        /// the method updates sending a skimmer for charging at a base station
        /// </summary>
        /// <param name="id">int value</param>
        /// <returns>if succeed in finding available charge slot</returns>
        public bool UpdateCharge(int id)
        {
            checkValid(id, 1, Drones.Count + 1);
            Drone tempDrone = Drones[id];
            tempDrone.Status = (DroneStatuses)1;
            return FindChargeSlot(id);
        }
        /// <summary>
        /// UpdateRelease is a method in the DalObject class.
        /// the method removes a drone from charging at a base station
        /// </summary>
        /// <param name="id">int value</param>
        public void UpdateRelease(int id)
        {
            checkValid(id, 1, Drones.Count + 1);
            Drone tempDrone = Drones[id - 1];
            tempDrone.Status = 0;
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
            ValidRange(0, BaseStations.Count, out input);
            Console.WriteLine(BaseStations[input-1]);
        }
        /// <summary>
        /// DisplayDrone is a method in the DalObject class.
        /// the method allows drone display
        /// </summary>
        public void DisplayDrone()
        {
            Console.WriteLine("enter drone id:");
            int input;
            ValidRange(1, Drones.Count + 1, out input);
            Console.WriteLine(Drones[input-1]);
        }
        /// <summary>
        /// DisplayCustomer is a method in the DalObject class.
        /// the method allows customer view
        /// </summary>
        public void DisplayCustomer()
        {
            Console.WriteLine("enter customer id:");
            int input;
            ValidRange(0,Customers.Count, out input);
            Console.WriteLine(Customers[input-1]);
        }
        /// <summary>
        /// DisplayParcel is a method in the DalObject class.
        /// the method allows parcel view
        /// </summary>
        public void DisplayParcel()
        {
            Console.WriteLine("Enter parcel id:");
            int input;
            ValidRange(0, Parcels.Count, out input);
            Console.WriteLine(Parcels[input-1]);
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
            foreach (Parcel parcel in Parcels)
            {
                if (parcel.DroneId == 0)
                {
                    Console.WriteLine(parcel);
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
                foreach (DroneCharge droneCharge in DroneCharges)
                {
                    if (droneCharge.DroneId == item.Id)
                        sum++;
                    if (droneCharge.DroneId > item.Id)
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
        public static void ValidRange(int min, int max, out int input)
        {
            bool parse = int.TryParse(Console.ReadLine(), out input);
            while (!parse || input > max || input < min)
            {
                Console.WriteLine("not valid input! please enter again");
                parse = int.TryParse(Console.ReadLine(), out input);
            }
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

