using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static BO.Enums;
using System.Runtime.CompilerServices;
using static System.Math;

using DalApi;

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
        private Delivery delivery;
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
                maintenance = Maintenance.Starting;
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
                update();
                Thread.Sleep(1000);
            }
        }

        private void AvailbleDrone()
        {
            lock (bl)
            {

                lock (bl)
                {
                    try
                    {
                        bl.AssignParcelToDrone(drone.DroneId);
                        parcelId = drone.ParcelId;
                    }
                    catch (NotExsistSutibleParcelException)
                    {
                        if (drone.BatteryDrone >= 100)
                            return;
                        else
                        {
                            Station = bl.ClosetStationThatPossible(drone.Location, drone.BatteryDrone, out double minDistance);
                            stationId = Station.Id;
                            if (Station != null)
                            {
                                drone.DroneStatus = DroneStatus.Meintenence;
                                maintenance = Maintenance.Starting;
                                dal.AddDRoneCharge(drone.DroneId, Station.Id);
                            }
                        }
                    }
                }

                //    parcelId = bl.FindTheMuchParcel(bl.GetDrone(drone.DroneId)).FirstOrDefault()?.Id;

                //if(parcelId==null)
                //{
                //    if(drone.BatteryDrone<100)
                //    {
                //        stationId = bl.ClosetStationThatPossible(drone.Location, drone.BatteryDrone, out double minDistance)?.Id;
                //        if (Station != null)
                //        {
                //            drone.DroneStatus = DroneStatus.Meintenence;
                //            maintenance = Maintenance.Starting;
                //            dal.AddDRoneCharge(drone.DroneId, Station.Id);
                //        }
                //    }
                //    else
                //    {
                //        return;
                //    }

                //}
                //else
                //{
                //    dal.AssignParcelToDrone((int)parcelId, drone.DroneId);
                //    drone.ParcelId = parcelId;
                //    initDelivery((int)parcelId);
                //    drone.DroneStatus = DroneStatus.Delivery;
                //}




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


        //private void DeliveryDrone()
        //{
        //    lock (bl)
        //    {
        //        /*try {*/
        //        if (parcelId == null) initDelivery((int)drone.ParcelId); /*}*/
        //        //catch (DO.ExistIdException ex) { throw new BadStatusException("Internal error getting parcel", ex); }
        //        distance = LocationExtensions.Distance(drone.Location, customer.Location);
        //    }

        //    if (distance < 0.01 || drone.BatteryDrone == 0.0)
        //        lock (bl)
        //        {
        //            drone.Location = customer.Location;
        //            if (pickedUp)
        //            {
        //                bl.ParcelDeliveredDrone((int)parcel?.Id);
        //                drone.DroneStatus = DroneStatus.Available;

        //            }
        //            else
        //            {
        //                bl.colloctDalParcel((int)parcel?.Id);
        //                customer = bl.GetCustomer((int)parcel?.TargetId);
        //                pickedUp = true;
        //            }
        //        }
        //    else
        //    {
        //        if (!sleepDelayTime()) return;
        //        lock (bl)
        //        {
        //            double delta = distance < STEP ? distance : STEP;
        //            double proportion = delta / distance;
        //            drone.BatteryDrone = Max(0.0, drone.BatteryDrone - delta * (pickedUp ? batteryUsage : bl.Available));
        //            double lat = drone.Location.Lattitude + (customer.Location.Lattitude - drone.Location.Lattitude) * proportion;
        //            double lon = drone.Location.Longitude + (customer.Location.Longitude - drone.Location.Longitude) * proportion;
        //            drone.Location = new() { Lattitude = lat, Longitude = lon };
        //        }
        //    }

        //    update();
        //}

        private void DeliveryDrone()
        {
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
                                        try
                                        {
                                            delivery = Delivery.Delivery;
                                            drone.Location = bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location;
                                            distance = LocationExtensions.Distance(drone.Location, bl.GetCustomer(parcel.CustomerReceivesTo.Id).Location);
                                            bl.ParcelCollectionByDrone(drone.DroneId);
                                            parcelId = drone.ParcelId;
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
                                    bl.DeliveryParcelByDrone(drone.DroneId);
                                    drone.DroneStatus = DroneStatus.Available;
                                    parcelId = drone.ParcelId;
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
                drone.DroneStatus = DroneStatus.Available;
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
