using PL.Converters;
using PL.Model;
using PL.UsingBl;
using PL.View.Customer;
using PL.View.Station;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL.ViewModel.Customer
{
    public class ViewCustomerList : DependencyObject
    {


        //public ListCollectionView ViewCustomers { get; set; }


        public ListCollectionView ViewCustomers
        {
            get { return (ListCollectionView)GetValue(ViewCustomersProperty); }
            set { SetValue(ViewCustomersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListCollectionView ViewCustomers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewCustomersProperty =
            DependencyProperty.Register("ViewCustomers", typeof(ListCollectionView), typeof(ViewCustomerList), new PropertyMetadata(null));


        public RelayCommand OpenAddCustomerWindow { get; set; }
        public RelayCommand OpenViewCustomerWindowCommand { get; set; }
        BlApi.IBL bl;

        public ViewCustomerList()
        {
            bl = BlApi.BlFactory.GetBL();
            ViewCustomers = new ListCollectionView(ViewCustomersList().ToList());
            OpenAddCustomerWindow = new(OpenAddWindow, null);
            OpenViewCustomerWindowCommand = new(OpenViewCustomerWindow, null);
        }

        private void RefreshList()
        {
            ViewCustomers = new ListCollectionView(ViewCustomersList().ToList());
        }

        public void OpenAddWindow(object param)
        {
            new AddCustomer().ShowDialog();
            RefreshList();
        }

        public IEnumerable<CustomerForList> ViewCustomersList()
        {
            return bl.GetCustomers().Select(customer => CustomerInParcelUseBL.ConvertBoCustomerForListToPo(customer));
        }

        private void OpenViewCustomerWindow(object param)
        {
            var customer = param as CustomerForList;
            new ViewCustomer(customer.Id).ShowDialog();
            RefreshList();
        }

    }
}
