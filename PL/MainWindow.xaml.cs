using PL.Customers;
using PL.Drones;
using PL.Stations;
using System.Windows;
using System.Windows.Controls;

namespace PL
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public BlApi.IBL bl { get; set; }
      
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBL();
        }

        private void Drone_Click(object sender, RoutedEventArgs e)
        {

            new DroneListView(bl).Show();

        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            new CustomerListView().Show();

        }

        private void Station_Click(object sender, RoutedEventArgs e)
        {
            new StationsListView().Show();
        }
    }
}
