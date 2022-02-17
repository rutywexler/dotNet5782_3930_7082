using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class UpDateStation : DependencyObject
    {
        BlApi.IBL bl;
        public RelayCommand UpdateStationCommand { get; set; }
        public RelayCommand OpenViewDroneInStationWindowCommand { get; set; }



        public BaseStation Station
        {
            get { return (BaseStation)GetValue(StationProperty); }
            set { SetValue(StationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationProperty =
            DependencyProperty.Register("Station", typeof(BaseStation), typeof(UpDateStation), new PropertyMetadata(null));

        public UpDateStation()
        {
            bl = BlApi.BlFactory.GetBL();
        }
        public UpDateStation(BaseStationForList station) : this()
        {
            Station = GetStation(station.Id);
            UpdateStationCommand = new(UpdateStation, param=>CheckValid.CheckValidUpdateStation(this.Station));
            OpenViewDroneInStationWindowCommand = new(OpenViewDroneWindow, null);
        }


        private BaseStation GetStation(int id)
        {
            return StationConverter.ConvertStationBlToPo(bl.GetStation(id));
        }

        public void UpdateStation(object parameter)
        {
           // if(Station.Name!=Name|| Station.AvailableChargeSlots!= NumOfChargeSlote)
            {
                bl.UpdateStation(Station.Id, Station.Name, Station.AvailableChargeSlots);
                MessageBox.Show("Succeed to Update station");
            }
        }

        private void OpenViewDroneWindow(object param)
        {
            var drone = param as DroneInCharging;
           // new ViewDrone(bl, ConvertDroneInChargingToDrone(drone, Station), refreshDroneList).Show();
        }

        private Drone ConvertDroneInChargingToDrone(DroneInCharging drone, BaseStation station)
        {
            
                return new Drone
                {
                    Id = drone.Id,
                    Model = bl.GetDrone(drone.Id).DroneModel,
                    Battery = drone.BatteryStatus,
                    Status = 0,
                    Location = new Location
                    {
                        Latitude = station.Location.Latitude,
                        Longitude = station.Location.Longitude
                    },
                    Weight = (Enums.WeightCategories)bl.GetDrone(drone.Id).Weight,
                    DeliveryByTransfer = null
                };
            
        }
    }
}


