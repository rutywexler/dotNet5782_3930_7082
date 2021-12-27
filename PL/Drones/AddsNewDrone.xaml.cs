using BO;
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
using static BO.Enums;
using System.Text.RegularExpressions;

namespace PL.Drones
{
    /// <summary>
    /// Interaction logic for AddsNewDrone.xaml
    /// </summary>
    public partial class AddsNewDrone : Window

    {
        private BlApi.IBL bl;
         Action refreshDroneList;

        public AddsNewDrone()
        {
            InitializeComponent();
            NewDrone.DataContext = new Drone();
        }

        public AddsNewDrone(BlApi.IBL ibl, Action refreshDroneList) :this()
        {
            bl = ibl;
            this.refreshDroneList = refreshDroneList;
            //NewDrone.DataContext = ibl.GetDrones();
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void AddingDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                
                BaseStation baseStation = bl.GetStation(int.Parse(ID_Station.Text));
                try
                {
                    bl.AddDrone(int.Parse(ID_Drone.Text), (DO.WeightCategories)(WeightCategories)WeightSelector.SelectedItem, Drone_model.Text, int.Parse(ID_Station.Text));
                    refreshDroneList();
                    if (MessageBox.Show("the drone succeeded to add ", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                    

                }
                catch
                {
                    MessageBox.Show("Didnt succeed to add the drone. enter the details again");
                }

            }
            catch
            {
                MessageBox.Show("Doesnt succeed to find the station enter id again");
            }
          

        }
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
    public int? DroneId { get; set; }

        private void cancelationAdd(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
