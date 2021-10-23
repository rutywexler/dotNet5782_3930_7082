using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    class DataSource
    {
        internal static Drone[] Drones = new Drone[10];
        internal static Stations[] BaseStations = new Stations[5];
        internal static Customer[] Customers = new Customer[100];
        internal static Parcel[] Parcels = new Parcel[1000];
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>();

        internal class Config
        {
            internal static int NumOfDrons = 0;
            internal static int NumOfBaseStations = 0;
            internal static int  NumOfCustomers= 0;
            internal static int NumOfParcels = 0;
            internal static int ParcelId=0;
        }

        public static Random rand = new Random();
        public static void Initalize()
        {
            //station
            const int STATION_NUM = 2;
            Stations tempStation = new Stations();
            for (int i = 0; i < STATION_NUM; i++)
            {
                tempStation.Id = Config.NumOfDrons + 1;
                tempStation.Name = Config.NumOfDrons * rand.Next();
                tempStation.Longitude = rand.Next(91);
                tempStation.Lattitude = rand.Next(181);
                tempStation.ChargeSlots = rand.Next() + 1;
                BaseStations[Config.NumOfBaseStations] = tempStation;
                Config.NumOfBaseStations++;
            }

            //Drone
            const int DRONE_NUM = 5;
            Drone tempDrone = new Drone();
            for (int i = 0; i < DRONE_NUM; i++)
            {
                tempDrone.Id = Config.NumOfDrons + 1;
                tempDrone.Model = rand.Next(100, 1000).ToString();
                tempDrone.MaxWeight= (WeightCategories)rand.Next(Enum.GetNames(typeof(WeightCategories)).Length);
                tempDrone.Status= (DroneStatuses)rand.Next(Enum.GetNames(typeof(DroneStatuses)).Length);
                tempDrone.Battery = rand.Next(101);
                Drones[Config.NumOfDrons] = tempDrone;
                Config.NumOfDrons++;
            }

            //Customers
            const int CUSTOMER_NUM = 10;
            Customer tempcustomer = new Customer();
            string[] tempNames = { "Tamar", "Ruty", "Michal", "Moshe", "Aviad", "Shimon","Eliether","Ariel", "Naomi","Tehila" };
            for (int i = 0; i < CUSTOMER_NUM; i++)
            {
                tempcustomer.Id = Config.NumOfCustomers + 1;
                tempcustomer.Name = tempNames[rand.Next(tempNames.Length)];
                tempcustomer.Phone = $"05 {rand.Next(100000000)}";
                tempcustomer.Lattitude = rand.Next(181) + rand.NextDouble();
                tempcustomer.Longitude = rand.Next(91) + rand.NextDouble();
                Customers[Config.NumOfCustomers] = tempcustomer;
                Config.NumOfCustomers++;
            }

            //parcel
            const int PARCEL_NUM = 10;
            Parcel tempParcel = new Parcel();
            for (int i = 0; i < PARCEL_NUM; i++)
            {
                tempParcel.Id = Config.NumOfParcels + 1;
                tempParcel.SenderId = Customers[rand.Next(Config.NumOfCustomers)].Id;
                do
                {
                    tempParcel.TargetId = Customers[rand.Next(Config.NumOfCustomers)].Id;
                } while (tempParcel.TargetId == tempParcel.SenderId);
                tempParcel.Weight = (WeightCategories)(rand.Next(3));
                tempParcel.Priority= (Priorities)(rand.Next(3));
                tempParcel.Requested = DateTime.Now;
                foreach (Drone drone in Drones)
                {
                    if(drone.Status==0&&drone.MaxWeight>= tempParcel.Weight)
                    {
                        tempParcel.DroneId = drone.Id;
                        break;
                    }
                }
                if(tempParcel.DroneId==0)
                {
                    // Console.WriteLine("No drone found suitable for send the parcel");
                    break;
                }
                tempParcel.Scheduled = DateTime.Now.AddDays(1);
                tempParcel.PickedUp = DateTime.Now.AddDays(15);
                tempParcel.Delivered = DateTime.Now.AddDays(16);
            }
            Parcels[Config.NumOfParcels] = tempParcel;
            Config.NumOfParcels++;

        }






    }
}



