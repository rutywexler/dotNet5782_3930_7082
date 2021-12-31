using PL.Converters;
using PL.Model;
using PL.View.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel.Customer
{
    public class ViewCustomerList
    {
        public IEnumerable<CustomerForList> ViewCustomers { get; set; }
        public RelayCommand OpenAddCustomerWindow { get; set; }
        BlApi.IBL bl;

        public ViewCustomerList()
        {
            bl = BlApi.BlFactory.GetBL();
            ViewCustomers = ViewCustomersList();
            OpenAddCustomerWindow = new(OpenAddWindow, null);
        }

        public static void OpenAddWindow(object param)
        {
            new AddCustomer().Show();
        }

        public IEnumerable<CustomerForList> ViewCustomersList()
        {
            return bl.GetCustomers().Select(customer => CustomerConverter.ConvertBoCustomerForListToPo(customer));
        }
    }
}
