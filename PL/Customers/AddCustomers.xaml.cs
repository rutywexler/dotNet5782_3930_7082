using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Customers
{
    /// <summary>
    /// Interaction logic for AddCustomers.xaml
    /// </summary>
    public partial class AddCustomers : Window
    {
        private BlApi.IBL bl;

        public AddCustomers()
        {
            InitializeComponent();
            NewCustomer.DataContext = new Customer();

        }
        public AddCustomers(BlApi.IBL ibl) : this()
        {
            bl = ibl;
           // this.refreshDroneList = refreshDroneList;
            NewCustomer.DataContext = ibl.GetDrones();
           // WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void AddingDrone(object sender, RoutedEventArgs e)
        {
            //try
            //{

            //    Customer customer = bl.GetCustomer(int.Parse(ID_Customer.Text));
                try
                {
                    bl.AddCustomer(int.Parse(ID_Customer.Text),Customer_Name.Text, Customer_Phone_Number.Text, double.Parse(Longitude_Customer.Text),double.Parse(Latitude_Customer.Text));
                    //refreshDroneList();
                    if (MessageBox.Show("the Customer succeeded to add ", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                    {
                        this.Close();
                    }


                }
                catch
                {
                    MessageBox.Show("Didnt succeed to add the Customer. enter the details again");
                }

            }
            //catch
            //{
            //    MessageBox.Show("Doesnt succeed to find the cu enter id again");
            //}
        

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void cancelationAdd(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

    }
}
