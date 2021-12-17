using IBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static BL.BO.Enums;

namespace PL.Drones
{
    /// <summary>
    /// Interaction logic for DroneListView.xaml
    /// </summary>
    public partial class DroneListView : Window
    {
        private IBL.IBL ibl;
        ListCollectionView droneCollectionView;

        public DroneListView()
        {
            InitializeComponent();

        }

        public DroneListView(IBL.IBL bl) : this()
        {
            ibl = bl;
            var droneToLists = new ObservableCollection<IBL.BO.DroneToList>(ibl.GetDrones());
            droneCollectionView = new ListCollectionView(droneToLists);
            droneCollectionView.Filter = FilterDrone;

            DronesListView.DataContext = droneCollectionView;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private bool FilterDrone(object obj)
        {
            if (obj is IBL.BO.DroneToList drone)
            {
                //var selectedDroneStatus = (DroneStatus)StatusSelector.SelectedItem;
                //var selectedWeightCategories = (WeightCategories)WeightSelector.SelectedItem;
             
                return (WeightSelector.SelectedItem == null ||drone.DroneWeight == (WeightCategories)WeightSelector.SelectedItem)
                    &&(StatusSelector.SelectedItem == null||drone.DroneStatus== (DroneStatus)StatusSelector.SelectedItem );
            }
            else
                return false;
        }

        private void RefreshDroneList()
        {
            DronesListView.DataContext = ibl.GetDrones();
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            droneCollectionView.Filter = FilterDrone;
            //DroneStatus droneStatus = (DroneStatus)StatusSelector.SelectedItem;
            //DronesListView.DataContext = ibl.GetSomeDronesByStatus(droneStatus);
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            droneCollectionView.Filter = FilterDrone;
            //WeightCategories weightCategories = (WeightCategories)WeightSelector.SelectedItem;
            //DronesListView.DataContext = ibl.GetSomeDronesByWeight(weightCategories);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddsNewDrone(ibl, RefreshDroneList).Show();
        }

        private void ViewDrone(object sender, MouseButtonEventArgs e)
        {
            var selectedDrone = (e.OriginalSource as FrameworkElement).DataContext as IBL.BO.DroneToList;
            new ViewDrone(ibl, selectedDrone).Show();
        }

        private void Cancel_filtering(object sender, RoutedEventArgs e)
        {
            RefreshDroneList();

        }
    }
}
