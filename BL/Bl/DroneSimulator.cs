using System;
using BO;
using System.Threading;
using DalApi;
using System.Linq;
using static System.Math;

namespace BL
{
    internal class DroneSimulator
    {
        enum Maintenance { Starting, Going, Charging }
        private const double VELOCITY = 1.0;
        private const int DELAY = 500;
        private const double TIME_STEP = DELAY / 1000.0;
        private const double STEP = VELOCITY / TIME_STEP;

        public DroneSimulator(Bl.BL blImp, int droneId, Action updateDrone, Func<bool> checkStop)
        {
            var bl = blImp;
            //var dal = bl.Dal;
            var drone = bl.GetDrone(droneId);
            int? parcelId = null;
            int? baseStationId = null;
            BaseStation bs = null;
            double distance = 0.0;
            int batteryUsage = 0;
            BO.Parcel? parcel = null;
            bool pickedUp = false;
            Customer customer = null;
            Maintenance maintenance = Maintenance.Starting;

            void initDelivery(int id)
            {
                parcel = bl.GetParcel(id);
                batteryUsage = (int)Enum.Parse(typeof(BatteryUsage), parcel?.Weight.ToString());
                pickedUp = parcel?.PickedUp is not null;
                customer = bl.GetCustomer((int)(pickedUp ? parcel?.TargetId : parcel?.SenderId));
            }

            do
            {
                //(var next, var id) = drone.nextAction(bl);

                switch (drone)
                {
                    case DroneForList { Status: DroneStatuses.Available }:
                        if (!sleepDelayTime()) break;

                        lock (bl)
                        {
                            parcelId = bl.Dal.GetParcels(p => p?.Scheduled == null
                                                              && (WeightCategories)(p?.Weight) <= drone.MaxWeight
                                                              && drone.RequiredBattery(bl, (int)p?.Id) < drone.Battery)
                                             .OrderByDescending(p => p?.Priority)
                                             .ThenByDescending(p => p?.Weight)
                                             .FirstOrDefault()?.Id;
                            switch (parcelId, drone.Battery)
                            {
                                case (null, 1.0):
                                    break;

                                case (null, _):
                                    baseStationId = bl.FindClosestBaseStation(drone, charge: true)?.Id;
                                    if (baseStationId != null)
                                    {
                                        drone.Status = DroneStatuses.Maintenance;
                                        maintenance = Maintenance.Starting;
                                        dal.BaseStationDroneIn((int)baseStationId);
                                        dal.AddDroneCharge(droneId, (int)baseStationId);
                                    }
                                    break;
                                case (_, _):
                                    try
                                    {
                                        dal.ParcelSchedule((int)parcelId, droneId);
                                        drone.DeliveryId = parcelId;
                                        initDelivery((int)parcelId);
                                        drone.Status = DroneStatuses.Delivery;
                                    }
                                    catch (DO.ExistIdException ex) { throw new BadStatusException("Internal error getting parcel", ex); }
                                    break;
                            }
                        }
                        break;

                    case DroneForList { Status: DroneStatuses.Maintenance }:
                        switch (maintenance)
                        {
                            case Maintenance.Starting:
                                lock (bl)
                                {
                                    try { bs = bl.GetBaseStation(baseStationId ?? dal.GetDroneChargeBaseStationId(drone.Id)); }
                                    catch (DO.ExistIdException ex) { throw new BadStatusException("Internal error base station", ex); }
                                    distance = drone.Distance(bs);
                                    maintenance = Maintenance.Going;
                                }
                                break;

                            case Maintenance.Going:
                                if (distance < 0.01)
                                    lock (bl)
                                    {
                                        drone.Location = bs.Location;
                                        maintenance = Maintenance.Charging;
                                    }
                                else
                                {
                                    if (!sleepDelayTime()) break;
                                    lock (bl)
                                    {
                                        double delta = distance < STEP ? distance : STEP;
                                        distance -= delta;
                                        drone.Battery = Max(0.0, drone.Battery - delta * bl.BatteryUsages[DRONE_FREE]);
                                    }
                                }
                                break;

                            case Maintenance.Charging:
                                if (drone.Battery == 1.0)
                                    lock (bl)
                                    {
                                        drone.Status = DroneStatuses.Available;
                                        dal.DeleteDroneCharge(droneId);
                                        dal.BaseStationDroneOut(bs.Id);
                                    }
                                else
                                {
                                    if (!sleepDelayTime()) break;
                                    lock (bl) drone.Battery = Min(1.0, drone.Battery + bl.BatteryUsages[DRONE_CHARGE] * TIME_STEP);
                                }
                                break;
                            default:
                                throw new BadStatusException("Internal error: wrong maintenance substate");
                        }
                        break;

                    case DroneForList { Status: DroneStatuses.Delivery }:
                        lock (bl)
                        {
                            try { if (parcelId == null) initDelivery((int)drone.DeliveryId); }
                            catch (DO.ExistIdException ex) { throw new BadStatusException("Internal error getting parcel", ex); }
                            distance = drone.Distance(customer);
                        }

                        if (distance < 0.01 || drone.Battery == 0.0)
                            lock (bl)
                            {
                                drone.Location = customer.Location;
                                if (pickedUp)
                                {
                                    dal.ParcelDelivery((int)parcel?.Id);
                                    drone.Status = DroneStatuses.Available;

                                }
                                else
                                {
                                    dal.ParcelPickup((int)parcel?.Id);
                                    customer = bl.GetCustomer((int)parcel?.TargetId);
                                    pickedUp = true;
                                }
                            }
                        else
                        {
                            if (!sleepDelayTime()) break;
                            lock (bl)
                            {
                                double delta = distance < STEP ? distance : STEP;
                                double proportion = delta / distance;
                                drone.Battery = Max(0.0, drone.Battery - delta * bl.BatteryUsages[pickedUp ? batteryUsage : DRONE_FREE]);
                                double lat = drone.Location.Latitude + (customer.Location.Latitude - drone.Location.Latitude) * proportion;
                                double lon = drone.Location.Longitude + (customer.Location.Longitude - drone.Location.Longitude) * proportion;
                                drone.Location = new() { Latitude = lat, Longitude = lon };
                            }
                        }
                        break;

                    default:
                        throw new BadStatusException("Internal error: not available after Delivery...");

                }
                updateDrone();
            } while (!checkStop());
        }

        private static bool sleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }
    }
}
