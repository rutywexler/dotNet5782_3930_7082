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
       
        public  BlApi.IBL bl{ get; set; }

        public CustomerListView()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBL();
            DataContext = bl.GetCustomers();
        }
   

        private void OpenAddCustomr(object sender, RoutedEventArgs e)
        {
            new AddCustomers(bl).Show();
        }
    }
}
