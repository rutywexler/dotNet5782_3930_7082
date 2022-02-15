using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static BO.Enums;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListView.xaml
    /// </summary>
    public partial class DroneListView : UserControl
    {
        private BlApi.IBL ibl;
        ListCollectionView droneCollectionView;

        public DroneListView()
        {
            InitializeComponent();

        }

        public DroneListView(BlApi.IBL bl) : this()
        {
            ibl = bl;
            RefreshDroneList();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private bool FilterDrone(object obj)
        {
            if (obj is BO.DroneToList drone)
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
            var droneToLists = new ObservableCollection<BO.DroneToList>(ibl.GetDrones());
            droneCollectionView = new ListCollectionView(droneToLists);
            droneCollectionView.Filter = FilterDrone;
            DronesListView.DataContext = droneCollectionView;
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            droneCollectionView.Refresh();
            //DroneStatus droneStatus = (DroneStatus)StatusSelector.SelectedItem;
            //DronesListView.DataContext = ibl.GetSomeDronesByStatus(droneStatus);
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            droneCollectionView.Refresh();
            //WeightCategories weightCategories = (WeightCategories)WeightSelector.SelectedItem;
            //DronesListView.DataContext = ibl.GetSomeDronesByWeight(weightCategories);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddDrone().ShowDialog();
            RefreshDroneList();
        }

        private void ViewDrone(object sender, MouseButtonEventArgs e)
        {
            var selectedDrone = (e.OriginalSource as FrameworkElement).DataContext;
            new ViewDrone(ibl, DroneConverter.ConvertDroneToList((BO.DroneToList)selectedDrone), RefreshDroneList).ShowDialog();
            RefreshDroneList();
        }

        private void Cancel_filtering(object sender, RoutedEventArgs e)
        {
            WeightSelector.SelectedItem = null;
            StatusSelector.SelectedItem = null;
            RefreshDroneList();

        }
    }
}
