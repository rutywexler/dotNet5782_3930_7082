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
        public ViewDrone(BlApi.IBL ibl, Drone selectedDrone)
            : this()
        {
            DataContext = new ViewDroneVM(ibl, selectedDrone);
           
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
           
            if ((DataContext as ViewDroneVM).Auto)
            {
               
                (DataContext as ViewDroneVM).worker.CancelAsync();
                e.Cancel = true;
            }
            PL.ViewDroneVM.buttonCacel = true;
            //Window.GetWindow(sender as FrameworkElement).Close();
            //this.Close();
        }
    }
}
