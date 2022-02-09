using DalApi;
using DalObject;
using DO;
using Singelton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Dal
{
    public sealed partial class DalXml : Singleton<DalXml>, Idal
    {
        private static string dir = @"..\..\data\";
        private static string Config = @"XmlConfig.xml";
        private DalXml()
        {

        }
        internal static void InitializeConfig()
        {
            new XDocument(
            new XElement("Config",
                            new XElement("IdParcel", 0),
                            new XElement("Available", 2),
                            new XElement("LightWeightCarrier", 2),
                            new XElement("MediumWeightBearing", 25),
                            new XElement("CarriesHeavyWeight", 40),
                            new XElement("DroneLoadingRate", 10))
                      ).Save(dir + Config);
        }
        public static XElement LoadConfigToXML(string filePath)
        {
            try
            {
                if (!File.Exists(dir + filePath))
                {
                    throw new XMLFileLoadCreateException($"fail to load xml file: {filePath}");
                }
                XDocument document = XDocument.Load(dir + filePath);
                return document.Root;
            }
            catch (Exception)
            {

                throw new XMLFileLoadCreateException();
            }

        }

        internal static void SaveConfigToXML(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch (Exception ex)
            {
                throw new XMLFileLoadCreateException($"fail to create xml file: {filePath}");
            }
        }

        public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        public void AddStation(int id, string name, double longitude, double latitude, int chargeSlots)
        {
            throw new NotImplementedException();
        }

        public void AddDRoneCharge(int droneId, int stationId)
        {
            throw new NotImplementedException();
        }

        public void ReleaseDroneFromRecharge(int droneId)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public int NotAvailableChargingPorts(int baseStationId)
        {
            throw new NotImplementedException();
        }

        public List<int> GetDronechargingInStation(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAvailableChargingStations()
        {
            throw new NotImplementedException();
        }

        public void RemoveStation(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
