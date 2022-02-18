using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static BO.Enums;
using System.Runtime.CompilerServices;
using static System.Math;

using DalApi;
using System.ComponentModel;

namespace BL
{
    internal class DroneSimulator
    {
        enum Maintenance { Starting, Going, Charging }
        enum Delivery { Starting, Going, Delivery }
        BL.Bl bl { set; get; }
        Parcel? parcel { set; get; }
        BaseStation Station { set; get; }
        int batteryUsage = 0;
        int? stationId;
        DroneToList drone;
        bool pickedUp = false;
        Customer customer = null;
        private const int DELAY = 500;

        private const double TIME_STEP = DELAY / 1000.0;
        private const double VELOCITY = 1000;
        private const double STEP = VELOCITY / TIME_STEP;
        double distance = 0.0;
        Func<bool> stop;
        Action update;
        private Delivery delivery= Delivery.Starting;
        Maintenance maintenance;
        private Idal dal;
        private int? parcelId = null;

        public DroneSimulator(int id, BL.Bl bl, Action update, Func<bool> checkStop)
        {
            this.bl = bl;
            lock (this.bl)
            {
                stop = checkStop;
                this.update = update;
                drone = this.bl.drones.FirstOrDefault(Drone => Drone.DroneId == id);
                //  maintenance = Maintenance.Starting;
                dal = bl.dal;
            }
            while (!stop())
            {
                switch (drone.DroneStatus)
                {
                    case DroneStatus.Available:
                        AvailbleDrone();
                        break;
                    case DroneStatus.Meintenence:
                        MaintenanceDrone();
                        break;
                    case DroneStatus.Delivery:
                        DeliveryDrone();
                        break;
                    default:
                        break;
                }
            }
            update();
        }

        private void AvailbleDrone()
        {
            lock (bl)
            {
                try
                {
                    bl.AssignParcelToDrone(drone.DroneId);
                }
                catch (NotExsistSutibleParcelException)
                {
                    if (drone.BatteryDrone >= 100)
                    {
                        sleepDelayTime();
                    }
                    else
                    {
                        Station = bl.ClosetStationThatPossible(drone.Location, drone.BatteryDrone, out double minDistance);
                        if (Station != null)
                        {
                            drone.DroneStatus = DroneStatus.Meintenence;
                            dal.AddDRoneCharge(drone.DroneId, Station.Id);
                        }
                        else
                        {
                            sleepDelayTime();
                        }
                    }
                }
            }
            update();

        }
        private void MaintenanceDrone()
        {
            switch (maintenance)
            {
                case Maintenance.Starting:
                    lock (bl)
                    {
                        /* try {*/
                        Station = bl.GetStation((int)(stationId != null ? stationId : dal.GetDroneChargeBaseStationId(drone.DroneId)));/* }*/
                        //  catch (DO. ex) { throw new BadStatusException("Internal error base station", ex); }
                        distance = LocationExtensions.Distance(drone.Location, Station.Location);
                        maintenance = Maintenance.Going;
                    }
                    break;
                case Maintenance.Going:
                    if (distance < 0.01)
                        lock (bl)
                        {
                            drone.Location = Station.Location;
                            maintenance = Maintenance.Charging;
                        }
                    else
                    {
                        if (!sleepDelayTime()) break;
                        lock (bl)
                        {
                            double delta = distance < STEP ? distance : STEP;
                            distance -= delta;
                            drone.BatteryDrone = Max(0.0, drone.BatteryDrone - delta * bl.Available);
                        }
                    }
                    break;
                case Maintenance.Charging:
                        if (drone.BatteryDrone == 100)
                            lock (bl)
                            {
                                drone.DroneStatus = DroneStatus.Available;
                                dal.ReleaseDroneFromRecharge(drone.DroneId);
                            }
                        else
                        {
                            if (!sleepDelayTime()) break;
        
                            lock (bl) drone.BatteryDrone = Min(100, drone.BatteryDrone + bl.DroneLoadingRate * TIME_STEP);
                        }
                    break;
                default:
                    break;
            }
            update();
            //if (Station == null)
            //    Station = bl.GetStations().Select(station => bl.GetStation(station.IdStation)).FirstOrDefault(station => station.DronesInCharge.FirstOrDefault(drone => drone.ID == drone.ID) != null);
            //UpdateLocationAndBattary(Station.Location, drone.BatteryDrone);
            //if (drone.BatteryDrone == 100)
            //{
            //    bl.ReleaseDroneFromCharging(drone.DroneId);
            //}
            //else
            //    lock (bl) drone.BatteryDrone = Math.Min(100, drone.BatteryDrone + BL.Bl.DroneLoadingRate * TIME_STEP);
        }


        //private void MaintenanceDrone()
        //{
        //     if (Station == null)
        //        Station = bl.GetStations().Select(station => bl.GetStation(station.IdStation)).FirstOrDefault(station => station.DronesInCharge.FirstOrDefault(drone => drone.ID == drone.ID) != null);
        //    if (drone.BatteryDrone >= 100)
        //    {
        //        bl.ReleaseDroneFromCharging(drone.DroneId);
        //    }

        //    else
        //        lock (bl) drone.BatteryDrone = Min(100, drone.BatteryDrone + bl.DroneLoadingRate * TIME_STEP);
        //    update();

        //}

        private void DeliveryDrone()
        {

            update();
            try
            {
                lock (bl)
                {
                    parcel = bl.GetParcel((int)drone.ParcelId);
                }
                switch (delivery)
                {
                    case Delivery.Starting:
                        {
                            lock (bl)
                            {
                                distance = LocationExtensions.Distance(drone.Location, bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location);
                                if (!sleepDelayTime()) break;
                  
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
                                    drone.Location = UpdateLocationAndBattary(bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location, bl.Available);
                                    distance = LocationExtensions.Distance(drone.Location, bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location);
                                }

                                else
                                {
                                    lock (bl)
                                    {
                                      
                                        delivery = Delivery.Delivery;
                                            drone.Location = bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location;
                                            distance = LocationExtensions.Distance(drone.Location, bl.GetCustomer(parcel.CustomerReceivesTo.Id).Location);
                                            bl.ParcelCollectionByDrone(drone.DroneId);
                                            delivery = Delivery.Delivery;
                                        update();
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
                                    bl.DeliveryParcelByDrone(drone.DroneId);
                                    delivery = Delivery.Starting;
                                }
                            }
                            else
                            {
                                lock (bl)
                                {
                                    double elect = bl.GetParcel((int)drone.ParcelId).WeightParcel switch
                                    {
                                        WeightCategories.Heavy => bl.CarryingHeavyWeight,
                                        WeightCategories.Medium => bl.MediumWeightBearing,
                                        WeightCategories.Light => bl.LightWeightCarrier,
                                        _ => 0.0
                                    };
                                    drone.Location = UpdateLocationAndBattary(bl.GetCustomer(parcel.CustomerReceivesTo.Id).Location, elect);
                                    distance = LocationExtensions.Distance(drone.Location, bl.GetCustomer(parcel.CustomerReceivesTo.Id).Location);
                                    update();
                                }
                            }
                            
          
                            break;
                        }

                      
                    default:
                        break;
                }
                update();


            }
            catch (KeyNotFoundException)
            {
                drone.DroneStatus = DroneStatus.Available;
            }

            update();


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
            drone.BatteryDrone = Math.Max(0.0, drone.BatteryDrone - delta * elec);
            double lat = drone.Location.Lattitude + (Target.Lattitude - drone.Location.Lattitude) * proportion;
            double lon = drone.Location.Longitude + (Target.Longitude - drone.Location.Longitude) * proportion;
            return new() { Lattitude = lat, Longitude = lon };
        }

        //void initDelivery(int id)
        //{
        //    parcel = dal.GetParcel(id);
        //    batteryUsage = (int)Enum.Parse(typeof(BatteryUsage), parcel?.Weight.ToString());
        //    pickedUp = parcel?.Collected is not null;
        //    customer = bl.GetCustomer((int)(pickedUp ? parcel?.TargetId : parcel?.SenderId));
        //}

    }
}
