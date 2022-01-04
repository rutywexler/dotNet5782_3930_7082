using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DataSource
    {
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Station> BaseStations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();

        internal class Config
        {
            internal static int IdParcel = 0;
            internal static double Available = 2;
            internal static double LightWeightCarrier = 10;
            internal static double MediumWeightBearing = 25;
            internal static double CarryingHeavyWeight = 40;
            internal static double DroneLoadingRate = 10;

        }

        public static Random rand = new Random();
        public static void Initalize()
        {
            //station


            for (int i = 0; i < 2; i++)
            {
                Station tempStation = new Station();
                tempStation.Id = BaseStations.Count + 1;
                tempStation.Name = $"station_{'a' + rand.Next()}"; 
                tempStation.Longitude = rand.Next(91);
                tempStation.Lattitude = rand.Next(181);
                tempStation.ChargeSlots = rand.Next() + 1;
                BaseStations.Add(tempStation);

            }

            //Drone
            const int DRONE_NUM = 5;
            for (int i = 0; i < DRONE_NUM; i++)
            {
                Drone tempDrone = new Drone();
                tempDrone.Id = Drones.Count + 1;
                tempDrone.Model = rand.Next(100, 1000).ToString();
                tempDrone.MaxWeight = (WeightCategories)rand.Next(Enum.GetNames(typeof(WeightCategories)).Length);
                Drones.Add(tempDrone);

            }

            //Customers
            const int CUSTOMER_NUM = 10;

            string[] tempNames = { "Tamar", "Ruty", "Michal", "Moshe", "Aviad", "Shimon", "Eliether", "Ariel", "Naomi", "Tehila" };
            for (int i = 0; i < CUSTOMER_NUM; i++)
            {
                Customer tempcustomer = new Customer();
                tempcustomer.Id = Customers.Count + 1;
                tempcustomer.Name = tempNames[rand.Next(tempNames.Length)];
                tempcustomer.Phone = $"05 {rand.Next(100000000)}";
                tempcustomer.Lattitude = rand.Next(181) + rand.NextDouble();
                tempcustomer.Longitude = rand.Next(91) + rand.NextDouble();
                Customers.Add(tempcustomer);

            }

            //parcel
            const int PARCEL_NUM = 10;
            for (int i = 0; i < PARCEL_NUM; i++)
            {
                Parcel tempParcel = new Parcel();
                tempParcel.Id = Parcels.Count + 1;
                tempParcel.SenderId = Customers[rand.Next(Parcels.Count)].Id;
                do
                {
                    tempParcel.TargetId = Customers[rand.Next(Customers.Count)].Id;
                } while (tempParcel.TargetId == tempParcel.SenderId);
                
                tempParcel.Weight = (WeightCategories)rand.Next((int)Enum.GetValues<WeightCategories>().Min(), (int)Enum.GetValues<WeightCategories>().Max());
                tempParcel.Priority = (Priorities)rand.Next((int)Enum.GetValues<Priorities>().Min(), (int)Enum.GetValues<Priorities>().Max());
                tempParcel.Requested = DateTime.Now;
                if(i<3)
                {
                    foreach (Drone drone in Drones)
                    {
                        if (drone.MaxWeight >= tempParcel.Weight)
                        {
                            tempParcel.DroneId = drone.Id;
                            break;
                        }
                    }
                }
                else
                {
                    tempParcel.DroneId = 0;
                }
             
               
                tempParcel.Scheduled = DateTime.Now.AddDays(1);
                tempParcel.PickedUp = DateTime.Now.AddDays(15);
                tempParcel.Delivered = DateTime.Now.AddDays(16);
                Parcels.Add(tempParcel);

            }


        }
    }
}



