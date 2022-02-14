using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL;


namespace PL
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
        public static BaseStation ConvertStationBlToPo(BO.BaseStation station)
        {
            return new BaseStation()
            {
                Id = station.Id,
                Name = station.Name,
                Location = LocationConverter.ConvertLocation(station.Location),
                AvailableChargeSlots = station.NumberOfChargingStations,
                DronesInCharching = station.DronesInCharge.Select(item => DroneConverter.ConvertDroneCharging(item)).ToList()
            };
        }
    }
}
