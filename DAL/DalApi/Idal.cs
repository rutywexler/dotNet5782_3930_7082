using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using static DalObject.DataSource.Config;
using DO;

namespace DalApi
{
    public interface Idal
    {
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude);
        public void AddParcel(int SenderId, int TargetId, WeightCategories Weigth, Priorities Priority, int id = 0, int droneId = 0, DateTime requested = default, DateTime sceduled = default, DateTime pickedUp = default, DateTime delivered = default);
        public void AddDrone(int id, string model, WeightCategories MaxWeight);
        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots);
        public void AddDRoneCharge(int droneId, int stationId);

        public void AssignParcelToDrone(int parcelId, int droneId);
        public void ReleaseDroneFromRecharge(int droneId);
        public Station GetStation(int id);
        public Drone GetDrone(int id);
        public Customer GetCustomer(int id);
        public Parcel GetParcel(int id);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Parcel> GetParcels(Func<Parcel, bool> predicate);
        public IEnumerable<Customer> GetCustomers();
       // public IEnumerable<Parcel> GetUnAssignmentParcels();
        public int NotAvailableChargingPorts(int baseStationId);
        public List<int> GetDronechargingInStation(int id);
        public IEnumerable<Station> GetAvailableChargingStations();
        //public void RemoveCustomer(Customer customer);
        public void RemoveParcel(int id);
        public void RemoveStation(int id);
        public double[] GetPowerConsumptionByDrone();
        public void RemoveDrone(Drone drone);
        public void RemoveCustomer(int id);

    }
}
