using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static BO.Enums;
using System.Runtime.CompilerServices;
using DalApi;

namespace BL
{
    internal class DroneSimulator
    {
        enum Maintenance { Starting, Going }
        enum Delivery { Starting, Going, Delivery }
        BL.Bl bl { set; get; }
        Parcel parcel { set; get; }
        BaseStation Station { set; get; }
        DroneToList drone;
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

        public DroneSimulator(int id, BL.Bl bl, Action update, Func<bool> checkStop)
        {
            this.bl = bl;
            lock (this.bl)
            {
                stop = checkStop;
                this.update = update;
                drone = this.bl.drones.FirstOrDefault(Drone => Drone.DroneId == id);
                maintenance = Maintenance.Starting;
                dal=bl.dal;
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
                try
                {
                    bl.AssignParcelToDrone(drone.DroneId);
                }
                catch (NotExsistSutibleParcelException)
                {
                    if (drone.BatteryDrone >= 100)
                        Thread.Sleep(100);
                    if (drone.BatteryDrone < 100)
                    {
                        Station = bl.ClosetStationThatPossible(drone.Location, drone.BatteryDrone, out double minDistance);
                        if(Station!=null)
                        {
                            drone.DroneStatus = DroneStatus.Meintenence;
                            maintenance = Maintenance.Starting;
                            dal.AddDRoneCharge(drone.DroneId, Station.Id);
                        }
                    }
                       


                }
            }
        }
        private void MaintenanceDrone()
        {
            if (Station == null)
                Station = bl.GetStations().Select(station => bl.GetStation(station.IdStation)).FirstOrDefault(station => station.DronesInCharge.FirstOrDefault(drone => drone.ID == drone.ID) != null);
            UpdateLocationAndBattary(Station.Location, drone.BatteryDrone);
            if (drone.BatteryDrone == 100)
            {
                bl.ReleaseDroneFromCharging(drone.DroneId);
            }

            else
                lock (bl) drone.BatteryDrone = Math.Min(100, drone.BatteryDrone + BL.Bl.DroneLoadingRate * TIME_STEP);

        }


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
                                distance = BL.LocationExtensions.Distance(drone.Location, bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location);
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
                                    distance = BL.LocationExtensions.Distance(drone.Location, bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location);
                                }

                                else
                                {
                                    lock (bl)
                                    {
                                        try
                                        {
                                            delivery = Delivery.Delivery;
                                            drone.Location = bl.GetCustomer(parcel.CustomerSendsFrom.Id).Location;
                                            distance = BL.LocationExtensions.Distance(drone.Location, bl.GetCustomer(parcel.CustomerReceivesTo.Id).Location);
                                            bl.ParcelCollectionByDrone(drone.DroneId);
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
                                    distance = BL.LocationExtensions.Distance(drone.Location, bl.GetCustomer(parcel.CustomerReceivesTo.Id).Location);
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

    }
}