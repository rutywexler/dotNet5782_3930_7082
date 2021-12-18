using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Drones
{
    /// <summary>
    /// Interaction logic for ViewDrone.xaml
    /// </summary>
    public partial class ViewDrone : Window
    {
        IBL.IBL MyIbl;
        public ViewDrone()
        {
            InitializeComponent();
        }

        public ViewDrone(IBL.IBL ibl, DroneToList selectedDrone) 
            : this()
        {
            MyIbl = ibl;
            SelectedDrone = selectedDrone;
            this.DataContext = selectedDrone;
        }

        public DroneToList SelectedDrone { get; }

        private void SendingTheDroneForCharging(object sender, RoutedEventArgs e)
        {
            MyIbl.SendDroneForCharge(SelectedDrone.DroneId);
        }

        private void SendingTheDroneForDelivery(object sender, RoutedEventArgs e)
        {
            MyIbl.DeliveryParcelByDrone(SelectedDrone.DroneId);
        }

        private void ReleaseDroneFromCharging(object sender, RoutedEventArgs e)
        {
            MyIbl.ReleaseDroneFromCharging(SelectedDrone.DroneId);
        }


        private void UpdateModel(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedDrone.ModelDrone = UpdateModelContext.Text;
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
    }
}
