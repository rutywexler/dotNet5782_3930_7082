using PL;
using PL.View.Customer;
using PL.View.Parcel;
using PL.View.Station;
using System.Windows;
using System.Windows.Controls;

namespace PL
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel;


        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            InitializeComponent();
        }

        private void Drone_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.CurrentView = new DroneListView(mainWindowViewModel.bl);
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.CurrentView = new CustomersList();
        }

        private void Station_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.CurrentView = new StationList();
        }

        private void Parcel_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.CurrentView = new ParcelsList();
        }
    }
}
