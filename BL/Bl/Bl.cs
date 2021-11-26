using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IDAL;
using IBL;
using static BL.BO.Enums;
using IBL.BO;
using System.Device.Location;


namespace IBL
{
    public partial class BL : IBL
    {
        private static readonly Random rand = new();
        private IDal dalObject;
        private List<DroneToList> drones;
        public BL()
        {
            dalObject = new DalObject.DalObject();
            drones = new List<DroneToList>();
            initializeDrones();
            //drones = dal.GetDrones();



        }
        public void initializeDrones()
        {
            foreach (var drone in dal.GetDrones())
            {
                drones.Add(new DroneToList
                {
                    IdDrone = drone.Id,
                    ModelDrone = drone.Model,
                    DroneWeight = (WeightCategories)drone.MaxWeight
                });
            }
            int electricityConsumption;//צריכת חשמל
            int SkimmerLoadingRate;//קצב טעינת רחפן
                                   //TODO : DeliveryId
            var parcels = dal.GetParcels().ToList();

            foreach (var drone in drones)
            {
                drone.PackageNumberIsTransferred = 0;
            }
            //TODO : Battery & Status
            foreach (var drone in drones)
            {
                drone.BatteryDrone = 1;
               if( drone.DroneStatus = ) ;
            }

            foreach (var drone in drones)
            {
                // drone.Location = findDroneLocation(drone);
                if (drone.DroneStatus != Enums.DroneStatuses.Delivery)
                {
                    drone.DroneStatus = (DroneStatuses)rand.Next(0, 2);
                    if (drone.DroneStatus == Enums.DroneStatuses.Meintenence)
                    {
                        IDAL.DO.Stations station = dal.GetStations().ToList()[rand.Next(0, dal.GetStations().ToList().Count)];
                        drone.DroneLocation = new Location()
                        {
                            Lattitude = station.Lattitude,
                            Longitude = station.Longitude

                        };
                       drone.BatteryDrone = rand.Next(0, 20) + rand.NextDouble();
                    }

                    if (drone.DroneStatus==Enums.DroneStatuses.Available)
                    {
                        
                    }


                    
                }

                       
                        

                    }

                }
            }

           /* foreach (var parcel in parcels)
            {


                if (parcel.PickedUp.Equals(default))
                {
                    var tmpStation = FindCloseLocation(dal.GetStations(), new() { Longitude = dal.GetCustomer(parcel.SenderId).Longitude, Latitude = dal.GetCustomer(parcel.SenderId).Latitude });
                    drones.DroneLocation = new Location()
                    {
                        Longitude = tmpStation.Longitude,
                        Latitude = tmpStation.Latitude
                    };
                }
                else
                    tmpDrone.CurrentLocation = new() { Longitude = dal.GetCustomer(parcel.SenderId).Longitude, Latitude = dal.GetCustomer(parcel.SenderId).Latitude };
                double minDistance;
                IDAL.DO.Customer customerSender = dal.GetCustomer(parcel.SenderId);
                IDAL.DO.Customer customerReciver = dal.GetCustomer(parcel.TargetId);
                double electrity = calculateElectricity(tmpDrone, new() { Latitude = customerSender.Latitude, Longitude = customerSender.Longitude }, new() { Latitude = customerReciver.Latitude, Longitude = customerReciver.Longitude }, (BO.WeightCategories)parcel.Weigth, out minDistance);

            }*/




        }
        /*public double FindCloseLocation(*/IDAL.IDal.   //.Location sLocation, Location tLocation)
        {
            var sCoord = new GeoCoordinate(sLocation.Lattitude, sLocation.Longitude);
            var tCoord = new GeoCoordinate(tLocation.Lattitude, tLocation.Longitude);
            double distance = sCoord.GetDistanceTo(tCoord);
            return private DroneStatuses associated;

        distance;
        }
    }
    }
    