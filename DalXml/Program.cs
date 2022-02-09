using DalApi;
using DO;
using System;
using System.Collections.Generic;

namespace Dal
{
    internal sealed partial class DalXml : Idal
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(int id, string model, WeightCategories MaxWeight)
        {
            throw new NotImplementedException();
        }

        public void AddDRoneCharge(int droneId, int stationId)
        {
            throw new NotImplementedException();
        }

        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime requested = default, DateTime sceduled = default, DateTime pickedUp = default, DateTime delivered = default)
        {
            throw new NotImplementedException();
        }

        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            throw new NotImplementedException();
        }

        public void AssignParcelToDrone(int parcelId, int droneId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAvailableChargingStations()
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public Drone GetDrone(int id)
        {
            throw new NotImplementedException();
        }

        public List<int> GetDronechargingInStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> GetDrones()
        {
            throw new NotImplementedException();
        }

      
        public double[] GetPowerConsumptionByDrone()
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStations()
        {
            throw new NotImplementedException();
        }

        public int NotAvailableChargingPorts(int baseStationId)
        {
            throw new NotImplementedException();
        }

        public void ReleaseDroneFromRecharge(int droneId)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveDrone(Drone drone)
        {
            throw new NotImplementedException();
        }

        public void RemoveParcel(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveStation(int id)
        {
            throw new NotImplementedException();
        }
    }
}
