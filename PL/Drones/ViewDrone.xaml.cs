using PL;
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
        //public ViewDrone(DroneForList drone)
        //{
        //    InitializeComponent();
        //    DataContext = new ViewDroneVM(this, drone, );
        //}
        public void  CloseDroneWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Close(object sender, CancelEventArgs e)
        {
            ViewDroneVM pl= new();
            pl.Window_Closing(sender, e);
            this.Close();
        }
    }
}
