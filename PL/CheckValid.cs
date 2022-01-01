using PL.Model;
using PL.Model.Po;
using PL.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public static class CheckValid
    {
        public static bool CheckValidAddParcel(ParcelToAdd parcel)
        {
            if (parcel.Id == null)
            {
                NotEnter("parcel");
                return false;
            }
            if (parcel.Priority == null)
            {
                NotEnter("priority");
                return false;
            }
            if (parcel.Sender == null)
            {
                NotEnter("sender Id");
                return false;
            }
            if (parcel.Target == null)
            {
                NotEnter("target Id");
                return false;
            }
            if (parcel.Weight == null)
            {
                NotEnter("weight");
                return false;
            }
            return true;
        }
        public static bool valid(object obj)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(typeof(object), false).Length == 0)
                    return false;

            }
            return true;
        }

        public static bool CheckValidAddStation(object obj)
        {
            if(valid(obj))
            {
                if (obj is BaseStationToAdd baseStation)
                {

                    if (baseStation.Id == null)
                    {
                        NotEnter("station Id");
                        return false;
                    }
                    if (baseStation.Location == null)
                    {
                        NotEnter("location");
                        return false;
                    }
                    if (baseStation.Location.Latitude > 90 || baseStation.Location.Latitude < 0)
                    {
                        EnterdWrongDetail("latitude");
                        return false;

                    }
                    if (baseStation.Location.Longitude > 90 || baseStation.Location.Longitude < 0)
                    {
                        EnterdWrongDetail("Longitude");
                        return false;

                    }
                    if (baseStation.Name == null)
                    {
                        NotEnter("Name");
                        return false;
                    }
                    if (baseStation.ChargeSlots == null)
                    {
                        NotEnter("charge slots");
                        return false;
                    }
                    if (baseStation.ChargeSlots < 0)
                    {
                        EnterdWrongDetail("num of charge slote");
                        return false;
                    }
                }
 
                return true;
            }
            else
                return false;

        }


        public static bool CheckValidAddCustomer(CustomerToAdd customer)
        {
            if (customer.Id == null)
            {
                NotEnter("station Id");
                return false;
            }
            if (customer.Name == null)
            {
                NotEnter("Name");
                return false;
            }
            if (customer.Phone == null)
            {
                NotEnter("Phone");
                return false;
            }
            if (customer.Location.Latitude == null)
            {
                NotEnter("Latitude");
                return false;
            }
            if (customer.Location.Longitude == null)
            {
                NotEnter("Longitude");
                return false;
            }
            if (customer.Location.Latitude > 90 || customer.Location.Latitude < 0)
            {
                EnterdWrongDetail("latitude");
                return false;

                }
                if (customer.Location.Longitude > 90 || customer.Location.Longitude < 0)
                {
                    EnterdWrongDetail("Longitude");
                    return false;

                }
                return true;
            }
            else
                return true;
        }

        private static void EnterdWrongDetail(string v)
        {
            MessageBox.Show("$ You Enterd error {v}, please enter again!");
        }

        private static void NotEnter(string v)
        {
            MessageBox.Show("$ You didnt Enter {v}, please enter!");
        }
    }
}
