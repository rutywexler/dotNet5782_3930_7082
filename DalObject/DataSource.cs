using DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dal
{
    public class DataSource
    {
        static readonly Random Rnd = new();

        private const int DRONE_INIT = 20;
        private const int STATIONS_INIT = 9;
        private const int CUSTOMERS_INIT = 25;
        private const int PARCELS_INIT = 50;
        private const int PHONE_MIN = 100000000;
        private const int PHONE_MAX = 1000000000;
        private const int LATITUDE_MAX = 90;
        private const int LATITUDE_MIN = -90;
        private const int LONGITUDE_MAX = 90;
        private const int CHARGE_SLOTS_MAX = 100;
        private const int PARCELS_STATE = 4;

        internal static List<Drone> Drones = new();
        internal static List<Station> Stations = new();
        internal static List<Customer> Customers = new();
        internal static List<Parcel> Parcels = new();
        internal static List<DroneCharge> DroneCharges = new();



        internal class Config
        {
            internal static int IdParcel = 0;
            internal static double Available = 0.001;
            internal static double LightWeightCarrier = 0.002;
            internal static double MediumWeightBearing = 0.003;
            internal static double CarriesHeavyWeight = 0.004;
            internal static double DroneLoadingRate = 2;
        }

        static internal void Initialize(DalObject dal)
        {
            for (int i = 1; i <= DRONE_INIT; ++i)
                RandomDrone(dal, i);
            for (int i = 1; i <= STATIONS_INIT; ++i)
                RandomStation(dal, i);
            for (int i = 1; i <= CUSTOMERS_INIT; ++i)
                RandomCustomer(dal, i);
            for (int i = 1; i <= PARCELS_INIT; ++i)
                RandParcel();
        }
        public static int AssignParcelDrone(WeightCategories weight)
        {
            Drone tmpDrone = Drones.FirstOrDefault(item => (weight <= item.MaxWeight));
            if (!tmpDrone.Equals(default(Drone)))
            {
                return tmpDrone.Id;
            }
            return 0;

        }
        private static void RandomDrone(DalObject dal, int id)
        {
            string model = $"Model_Drone_ {'a' + id}_{id * Rnd.Next()}";
            WeightCategories maxWeight = (WeightCategories)Rnd.Next((int)Enum.GetValues<WeightCategories>().Min(), (int)Enum.GetValues<WeightCategories>().Max());

            dal.AddDrone(id, model, maxWeight);
        }
        private static void RandomStation(DalObject dal, int id)
        {
            string name = $"station_{'a' + id}";
            double latitude = Rnd.Next(LATITUDE_MIN, LATITUDE_MAX) + Rnd.NextDouble();
            double longitude = Rnd.Next(LONGITUDE_MAX) + Rnd.NextDouble();
            int chargeSlots = Rnd.Next(1, CHARGE_SLOTS_MAX);
            dal.AddStation(id, name, longitude, latitude, chargeSlots);
        }
        private static void RandomCustomer(DalObject dal, int id)
        {
            string[] tempNames = { "Tamar", "Ruty", "Michal", "Moshe", "Aviad", "Shimon", "Eliether", "Ariel", "Naomi", "Tehila" };
            string name = tempNames[Rnd.Next(tempNames.Length)]; 
            string phone = $"05 {Rnd.Next(100000000)}";
            double latitude = Rnd.Next(LATITUDE_MIN, LATITUDE_MAX) + Rnd.NextDouble();
            double longitude = Rnd.Next(LONGITUDE_MAX) + Rnd.NextDouble();
            dal.AddCustomer(id, phone, name, longitude, latitude);
        }
        private static void RandParcel()
        {
            Parcel newParcel = new();
            newParcel.Id = ++Config.IdParcel;
            newParcel.SenderId = Customers[Rnd.Next(1, Customers.Count(customer => !customer.IsDeleted))].Id;
            do
            {
                newParcel.TargetId = Customers[Rnd.Next(1, Customers.Count(customer => !customer.IsDeleted))].Id;
            } while (newParcel.TargetId == newParcel.SenderId);
            newParcel.Weight = (WeightCategories)Rnd.Next((int)Enum.GetValues<WeightCategories>().Min(), (int)Enum.GetValues<WeightCategories>().Max());
            newParcel.Priority = (Priorities)Rnd.Next((int)Enum.GetValues<Priorities>().Min(), (int)Enum.GetValues<Priorities>().Max());
            newParcel.Created = DateTime.Now; ;
            newParcel.Associated = default;
            newParcel.Collected = default;
            newParcel.Delivered = default;
            newParcel.DroneId = 0;
            newParcel.IsDeleted = false;
            int status = Rnd.Next(PARCELS_STATE);
            if (status != 0)
            {
                newParcel.DroneId = AssignParcelDrone(newParcel.Weight);
                if (newParcel.DroneId != 0)
                {
                    Parcel tmp = Parcels.FirstOrDefault(parcel => parcel.DroneId == newParcel.DroneId && parcel.Delivered == null);
                    if (tmp.DroneId == 0)
                    {
                        newParcel.Associated = DateTime.Now;
                        if (status == 2)
                        {
                            newParcel.Collected = DateTime.Now;
                        }

                    }
                    if (status == 3)
                    {
                        newParcel.Associated = DateTime.Now;
                        newParcel.Collected = DateTime.Now;
                        newParcel.Delivered = DateTime.Now;
                    }

                }

            }
            Parcels.Add(newParcel);
        }
    }

}


