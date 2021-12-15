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
    /// Interaction logic for AddsNewDrone.xaml
    /// </summary>
    public partial class AddsNewDrone : Window
    {
        public AddsNewDrone()
        {
            InitializeComponent();
        }

        public AddsNewDrone(IBL.IBL ibl) :this()
        {
            
        }

    }
}
