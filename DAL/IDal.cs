using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using static DalObject.DataSource.Config;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude);
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0);
        public void AddDrone(int id, string model, WeightCategories MaxWeight);
        public void AddStation(int id, int name, double longitude, double latitude, int chargeSlots);
        public void AssignParcelToDrone(int parcelId, int droneId);
        public void CollectParcel(int parcelId);
        public void SupplyParcel(int parcelId);
        public void SendDroneToRecharge(int droneId);
        public void ReleaseDroneFromRecharge(int droneId);
        public Station GetStation(int id);
        public Drone GetDrone(int id);
        public Customer GetCustomer(int id);
        public Parcel GetParcel(int id);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Parcel> GetUnAssignmentParcels();
        int AvailableChargingPorts(int baseStationId);
        public IEnumerable<Station> GetAvailableChargingStations();
        public void RemoveCustomer(Customer customer);
        public void RemoveParcel(Parcel parcel);
        public double[] GetPowerConsumptionByDrone();
    }
}
                     