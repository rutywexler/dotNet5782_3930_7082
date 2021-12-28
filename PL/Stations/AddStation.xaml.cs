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

namespace PL.Stations
{
    /// <summary>
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        public BlApi.IBL bl { get; set; }
        public AddStation()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBL();
        }

        private void AddingStation(object sender, RoutedEventArgs e)
        {
           // bl.AddStation();
        }
    }
}
