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
        public void addCustomer(int id, string phone, string name, double longitude, double latitude);
        public void addDrone(int id, string model, WeightCategories MaxWeight);
        public void addStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void AssignParcelToDrone(int parcelId, int droneId);
        public void CollectParcel(int parcelId);
        public void SupplyParcel(int parcelId);
        public void SendDroneToRecharge(int droneId);
        public void ReleaseDroneFromRecharge(int droneId);
        public Stations GetStation(int id);
        public Drone GetDrone(int id);
        public Customer GetCustomer(int id);
        public Parcel GetParcel(int id);
        public IEnumerable<Stations> GetStations();
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Parcel> GetUnAssignmentParcels();
        int AvailableChargingPorts(int baseStationId);
        public IEnumerable<Stations> GetAvailableChargingStations();


    }
}
                     