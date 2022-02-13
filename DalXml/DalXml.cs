﻿using DalApi;
using Dal;
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
            catch (Exception)
            {
                throw new XMLFileLoadCreateException($"fail to create xml file: {filePath}");
            }
        }
    }
}
