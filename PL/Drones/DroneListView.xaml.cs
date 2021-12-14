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
using static BL.BO.Enums;

namespace PL.Drones
{
    /// <summary>
    /// Interaction logic for DroneListView.xaml
    /// </summary>
    public partial class DroneListView : Window
    {
        private IBL.IBL ibl;
        
        public DroneListView()
        {
            InitializeComponent();
            
        }

        public DroneListView(IBL.IBL bl): this()
        {
            ibl = bl;

            DronesListView.DataContext = ibl.GetDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));


        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            switch ((DroneStatus)StatusSelector.SelectedItem)
            {
                case DroneStatus.Available:
                    {
                        DronesListView.
                        break;
                    }
                case DroneStatus.Delivery:
                    {
                        break;
                    }
                case DroneStatus.Meintenence:
                    {
                        break;
                    }
            }
            
        }
    }
}
