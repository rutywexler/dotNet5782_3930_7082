﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Converters;
using PL.Model;
using PL.Model.Po;
using PL.PO;

namespace PL.UsingBl
{
    public static class StationConverter
    {
        public static BaseStationForList ConvertBoStationForListToPo(BO.BaseStationToList station)
        {
            return new BaseStationForList()
            {
                Id = station.IdStation,
                Name = station.StationName,
                NumOfAvailableChargingStations = station.NumOfAvailableChargingStations,
                NumOfBusyChargingStations = station.NumOfBusyChargingStations,
            };
        }
        

        internal static BO.BaseStation ConvertPoBaseStationToBO(BaseStationToAdd baseStation)
        {
            return new()
            {
                Id = (int)baseStation.Id,
                Name = baseStation.Name,
                NumberOfChargingStations = (int)baseStation.ChargeSlots,
                Location = LocationConverter.ConvertBackLocation(baseStation.Location),
            };
        }
    }
}
