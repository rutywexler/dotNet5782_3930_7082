using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
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
            AddCustomerCommand = new(AddCustomer,null)/*checkValid.CheckValidAddCustomer)*/;
        }

        private void AddCustomer(object parameter)
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
