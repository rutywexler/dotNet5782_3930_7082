using PL.Converters;
using PL.Model.Po;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModel.Customer
{
    public class AddCustomerViewModel
    {
        BlApi.IBL bl;
        public CustomerToAdd customer { get; set; }
        public RelayCommand AddCustomerCommand { get; set; }
        public AddCustomerViewModel()
        {
            bl = BlApi.BlFactory.GetBL();
            customer = new();
            AddCustomerCommand = new(AddStation,null)/*checkValid.CheckValidAddCustomer)*/;
        }

        private void AddStation(object parameter)
        {
            try
            {
                var customerBO = CustomerInParcelUseBL.ConvertPoCustomerToBO(customer);
                bl.AddCustomer(customerBO);
                MessageBox.Show("Success to add customer:)");

            }
            catch (KeyNotFoundException)
            {

                MessageBox.Show("Didnt succeed to add the Customer:( Enter details Again");
            }
        }
    }
}
