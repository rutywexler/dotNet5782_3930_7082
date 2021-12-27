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

namespace PL.Customers
{
    /// <summary>
    /// Interaction logic for CustomerListView.xaml
    /// </summary>
    public partial class CustomerListView : Window
    {
        private BlApi.IBL ibl;

        public CustomerListView()
        {
            InitializeComponent();
        }
        public CustomerListView(BlApi.IBL bl) : this()
        {
            ibl = bl;


            //RefreshDroneList();
           // StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatus));
           // WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void OpenAddCustomr(object sender, RoutedEventArgs e)
        {
            new AddCustomers(ibl).Show();
        }
    }
}
