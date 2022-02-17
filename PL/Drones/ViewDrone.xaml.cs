using PL.ViewModel.Drones;
using System;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for ViewDrone.xaml
    /// </summary>
    public partial class ViewDrone : Window
    {
        public ViewDrone(BlApi.IBL ibl, Drone selectedDrone, Action refreshDroneList)
            : this()
        {
            DataContext = new ViewDroneVM(ibl, selectedDrone, refreshDroneList);
           
        }
        public ViewDrone()
        {
            InitializeComponent();
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
