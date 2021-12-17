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
    /// Interaction logic for DroneView.xaml
    /// </summary>
    public partial class DroneView : Window
    {
        public DroneView()
        {
            InitializeComponent();
            Content.DataContext=  new AddsNewDrone(); 
        }
        //public DroneView(drone)
        //{
        //    InitializeComponent();
        //    Content.DataContext = new ViewDrone();
        //}
    }
}
