using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static BO.Enums;

namespace BL
{
    internal class DroneSimulator
    {
        enum Maintenance { Starting, Going }
        enum Delivery { Starting, Going, Delivery }
        int? parcelId;
        int? senderId;
        int? reciverId;
        int? stationId;
        Bl.BL bl { set; get; }
        Parcel parcel { set; get; }
        BaseStation Station { set; get; }
        DroneToList Drone { set; get; }
        private const int DELAY = 500;
        private Maintenance maintenance = Maintenance.Starting;
        private Delivery delivery = Delivery.Starting;
        private const double TIME_STEP = DELAY / 1000.0;
        private const double VELOCITY = 1000;
        private const double STEP = VELOCITY / TIME_STEP;
        double distance = 0.0;
        public DroneSimulator(int id, Bl.BL BL, Action<int?, int?, int?, int?> update, Func<bool> checkStop)
        {
            try
            {
                bl = BL;
                lock (bl)
                {
                    Drone = bl.drones.FirstOrDefault(Drone => Drone.DroneId == id);
                    if (Drone.DroneStatus == DroneStatus.Delivery
                        && bl.GetParcel((int)Drone.ParcelId).CollectionTime != null)
                        delivery = Delivery.Delivery;
                }
                while (!checkStop())
                {
                    parcelId = senderId = reciverId = stationId = null;
                    if (sleepDelayTime())
                        switch (Drone.DroneStatus)
                        {
                            case DroneStatus.Available:
                                AvailbleDrone();
                                break;
                            //case DroneStatus.WAYTOCHARGE:
                            //    WayToChargeDrone();
                            //    break;
                            case DroneStatus.Meintenence:
                                MaintenanceDrone();
                                break;
                            case DroneStatus.Delivery:
                                DeliveryDrone();
                                break;
                            default:
                                break;
                        }
                    update(parcelId, senderId, reciverId, stationId);
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("", ex);
            }
        }
        private void WayToChargeDrone()
        {
            switch (maintenance)
            {
                case Maintenance.Starting:
                    {
                        lock (bl)
                        {
                            try
                            {

                                Station = bl.ClosetStationThatPossible(Drone.Location, Drone.BatteryDrone, out double n);
                                //try { Station = bl.GetStation(stationId ?? bl.GetDroneChargeBaseStationId(Drone.DroneId)); }
                                //catch (DO.ExistIdException ex) { throw new BadStatusException("Internal error base station", ex); }
                                //distance = drone.Distance(bs);
                                //maintenance = Maintenance.Going;
                                if (Station != null)
                                {

                                    distance = Bl.LocationExtensions.Distance(Drone.Location, Station.Location);
                                    maintenance = Maintenance.Going;
                                }
                            }
                            catch (Exception)//NotExsistSuitibleStationException)
                            {

                            }

                        }
                        break;
                    }
                case Maintenance.Going:
                    {
                        if (distance < 0.01)
                            bl.SendDroneForCharge(Drone.DroneId);
                        else
                        {
                            Drone.Location = UpdateLocationAndBattary(Station.Location, bl.Available);
                            distance = Bl.LocationExtensions.Distance(Drone.Location, Station.Location);
                        }
                        break;
                    };
            }

        }
        private void AvailbleDrone()
        {
            lock (bl)
            {
                try
                {
                    bl.AssignParcelToDrone(Drone.DroneId);
                    parcelId = Drone.ParcelId;
                }
                catch (/*NotExsistSutibleParcelException*/Exception)
                {
                    if (Drone.BatteryDrone >= 100)
                        return;
                    //Drone.DroneStatus = DroneStatus.WAYTOCHARGE;
                    maintenance = Maintenance.Starting;
                }
            }
        }
        private void MaintenanceDrone()
        {
            if (Station == null)
                Station = bl.GetStations().Select(station => bl.GetStation(station.IdStation)).FirstOrDefault(station => station.DronesInCharge.FirstOrDefault(drone => drone.ID == Drone.DroneId) != null);
            if (Drone.BatteryDrone == 100)
            {
                bl.ReleaseDroneFromCharging(Drone.DroneId);
                delivery = Delivery.Starting;
            }

            else
                lock (bl) Drone.BatteryDrone = Math.Min(100, Drone.BatteryDrone + Bl.BL.DroneLoadingRate * TIME_STEP);
            stationId = Station.Id;

        }

        private void DeliveryDrone()
        {
            try
            {
                lock (bl)
                {
                    parcel = bl.GetParcel((int)Drone.ParcelId);
                }
                switch (delivery)
                {
                    case Delivery.Starting:
                        {
                            lock (bl)
                            {
                                distance = Bl.LocationExtensions.Distance(Drone.Location, bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location);
                            }
                            delivery = Delivery.Going;
                            break;
                        }
                    case Delivery.Going:
                        {
                            lock (bl)
                            {
                                if (distance > 0.01)
                                {
                                    Drone.Location = UpdateLocationAndBattary(bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location, bl.Available);
                                    distance = Bl.LocationExtensions.Distance(Drone.Location, bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location);
                                }

                                else
                                {
                                    lock (bl)
                                    {
                                        try
                                        {
                                            delivery = Delivery.Delivery;
                                            Drone.Location = bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location;
                                            distance = Bl.LocationExtensions.Distance(Drone.Location, bl.GetCustomer(parcel.CustomerReceivesTo.Id).Location);
                                            bl.ParcelCollectionByDrone(Drone.DroneId);
                                            parcelId = Drone.ParcelId;
                                            delivery = Delivery.Delivery;
                                        }
                                        catch (ArgumentNullException)
                                        {
                                            delivery = Delivery.Delivery;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case Delivery.Delivery:
                        {
                            if (distance < 0.01)
                            {
                                lock (bl)
                                {
                                    bl.DeliveryParcelByDrone(Drone.DroneId);
                                    Drone.DroneStatus = DroneStatus.Available;
                                    parcelId = Drone.ParcelId;
                                }
                            }
                            else
                            {
                                lock (bl)
                                {
                                    double elect = bl.GetParcel((int)Drone.ParcelId).WeightParcel switch
                                    {
                                        WeightCategories.Heavy => bl.CarryingHeavyWeight,
                                        WeightCategories.Medium => bl.MediumWeightBearing,
                                        WeightCategories.Light => bl.LightWeightCarrier,
                                        _ => 0.0
                                    };
                                    Drone.Location = UpdateLocationAndBattary(bl.GetCustomer(parcel.CustomerReceivesTo.Id).Location, elect);
                                    distance = Bl.LocationExtensions.Distance(Drone.Location, bl.GetCustomer(parcel.CustomerReceivesTo.Id).Location);
                                }
                            }

                            break;
                        }

                    default:
                        break;
                }

            }
            catch (KeyNotFoundException)
            {
                Drone.DroneStatus = DroneStatus.Available;
            }



        }
        private static bool sleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }
        private Location UpdateLocationAndBattary(Location Target, double elec)
        {
            double delta = distance < STEP ? distance : STEP;
            double proportion = delta / distance;
            Drone.BatteryDrone = Math.Max(0.0, Drone.BatteryDrone - delta * elec);
            double lat = Drone.Location.Lattitude + (Target.Lattitude - Drone.Location.Lattitude) * proportion;
            double lon = Drone.Location.Longitude + (Target.Longitude - Drone.Location.Longitude) * proportion;
            return new() { Lattitude = lat, Longitude = lon };
        }

    }
}