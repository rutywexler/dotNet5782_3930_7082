using PL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PL.Enums;
using System.Windows;


namespace PL
{
    public static class CheckValid
    {

        public static bool CheckValidAddParcel(object obj)
        {
            if (obj is ParcelToAdd parcel)
            {

                if (parcel.Priority == null)
                {
                    return false;
                }
                if (parcel.Sender == null)
                {
                    return false;
                }
                if (parcel.Target == null)
                {
                    return false;
                }
                if (parcel.Weight == null)
                {
                    return false;
                }
                return true;
            }
            else
                return false;
        }


        public static bool CheckValidAddStation(object obj)
        {

            if (obj is BaseStationToAdd baseStation)
            {

                if (baseStation.Id == null)
                {
                    return false;
                }
                if (baseStation.Location == null)
                {
                    return false;
                }
                if (baseStation.Location.Latitude > 90 || baseStation.Location.Latitude < -90)
                {
                    return false;

                }
                if (baseStation.Location.Longitude > 90 || baseStation.Location.Longitude < -90)
                {
                    return false;

                }
                if (baseStation.Name == null)
                {
                    return false;
                }
                if (baseStation.ChargeSlots == null)
                {
                    return false;
                }
                if (baseStation.ChargeSlots < 0)
                {
                    return false;
                }

                return true;
            }
            else
                return false;

        }

        public static bool CheckValidAddCustomer(Object obj)
        {
            if (obj is CustomerToAdd customer)
            {
                if (customer.Id == null)
                {
                    return false;
                }
                if (customer.Name == null)
                {
                    return false;
                }
                if (customer.Phone == null || customer.Phone.Length != 10)
                {

                    return false;
                }
                if (customer.Location.Latitude == null)
                {
                    return false;
                }
                if (customer.Location.Longitude == null)
                {
                    return false;
                }
                if (customer.Location.Latitude > 90 || customer.Location.Latitude < 0)
                {
                    return false;

                }
                if (customer.Location.Longitude > 90 || customer.Location.Longitude < 0)
                {
                    return false;
                }
                return true;



            }
            else return false;
        }
        public static bool CheckValidUpdateCustomer(object obj)
        {
            if (obj is SimpleCustomer customer)
            {
                if (customer.Name == null || customer.PhoneNumber == null || customer.PhoneNumber.Length != 10 || customer.Name == "")
                {
                    return false;
                }
                return true;
            }
            else return false;
        }
        public static bool CheckValidAddDrone(object obj)
        {
            if (obj is DroneToAdd drone)
            {
                if (drone.Model == null || drone.Weight == null || drone.Id == null || drone.StationId == null)
                {
                    return false;
                }

                return true;
            }
            else return false;
        }
        public static bool CheckValidUpdateStation(object obj)
        {
            if (obj is BaseStation baseStation)
            {
                if (baseStation.Name == null || baseStation.AvailableChargeSlots == null || baseStation.AvailableChargeSlots < 0 || baseStation.Name == "")
                {
                    return false;
                }
                return true;
            }
            else return false;
        }
        public static bool CheckValidUpdateDrone(object obj)
        {
            if (obj is Drone drone)
            {
                if (drone.Model == null || drone.Model == "")
                {
                    return false;
                }
                return true;
            }
            else return false;
        }


    }
}

