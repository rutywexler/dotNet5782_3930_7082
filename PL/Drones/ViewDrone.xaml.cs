using BO;
using System;
using System.Windows;

namespace PL.Drones
{
    /// <summary>
    /// Interaction logic for ViewDrone.xaml
    /// </summary>
    public partial class ViewDrone : Window
    {
        BlApi.IBL MyIbl;
        Action RefreshDroneList;
        public ViewDrone()
        {
            InitializeComponent();
        }

        public ViewDrone(BlApi.IBL ibl, DroneToList selectedDrone,Action refreshDroneList) 
            : this()
        {
            MyIbl = ibl;
            SelectedDrone = selectedDrone;
            this.DataContext = selectedDrone;
            RefreshDroneList = refreshDroneList;
        }

        public DroneToList SelectedDrone { get; }

        private void SendingTheDroneForCharging(object sender, RoutedEventArgs e)
        {
            try
            {
                MyIbl.SendDroneForCharge(SelectedDrone.DroneId);
                RefreshDroneList();
                MessageBox.Show("succees to Send Drone For Charge");
            }
            catch
            {
                MessageBox.Show("failed to Send Drone For Charge");
            }

        }

        private void SendingTheDroneForDelivery(object sender, RoutedEventArgs e)
        {
            try
            {
                MyIbl.AssignParcelToDrone(SelectedDrone.DroneId);
                RefreshDroneList();
                MessageBox.Show("succees to Sending The Drone For Delivery");
            }
            catch
            {
                MessageBox.Show("failed to Sending The Drone For Delivery");
            }
           
        }

        private void ReleaseDroneFromCharging(object sender, RoutedEventArgs e)
        {
            //MyIbl.ReleaseDroneFromCharging(SelectedDrone.DroneId);
            RefreshDroneList();
        }


        private void UpdateModel(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedDrone.ModelDrone = UpdateModelContext.Text;
                RefreshDroneList();
                MessageBox.Show("the drone succeeded to update ", "success", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("Failed to update the drone");
            }
           

        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ParcelCollection(object sender, RoutedEventArgs e)
        {
            try
            {
                MyIbl.ParcelCollectionByDrone(SelectedDrone.DroneId);
                RefreshDroneList();
                MessageBox.Show("succees Parcel Collection By Drone");
            }
            catch
            { 
                MessageBox.Show("Failed to Parcel Collection By Dronee");
            }
          
        }

        private void ParcelDelivery(object sender, RoutedEventArgs e)
        {
            try
            {
                MyIbl.DeliveryParcelByDrone(SelectedDrone.DroneId);
                RefreshDroneList();
                MessageBox.Show("succees Delivery Parcel By Drone");
            }
            catch
            {
                MessageBox.Show("Failed to Delivery Parcel By Drone");
            }
           
        }
    }
}
