using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class CustomerOption : DependencyObject
    {
        BlApi.IBL bl;
        public CustomerForList CustomerToList { get; set; }
        public RelayCommand UpdateCustomerCommand { get; set; }
        public RelayCommand OpenViewParcelrWindowCommand { get; set; }





        public SimpleCustomer Customer
        {
            get { return (SimpleCustomer)GetValue(CustomerDP); }
            set { SetValue(CustomerDP, value); }
        }
        public static readonly DependencyProperty CustomerDP =
        DependencyProperty.Register("Customer", typeof(SimpleCustomer), typeof(CustomerOption), new PropertyMetadata(null));


        public string Name
        {
            get { return (string)GetValue(NameDP); }
            set { SetValue(NameDP, value); }
        }

        public static readonly DependencyProperty NameDP =
       DependencyProperty.Register("Name", typeof(string), typeof(CustomerOption), new PropertyMetadata(""));

        public string PhoneNumber
        {
            get { return (string)GetValue(PhoneNumberDP); }
            set { SetValue(PhoneNumberDP, value); }
        }

        public static readonly DependencyProperty PhoneNumberDP =
       DependencyProperty.Register("PhoneNumber", typeof(string), typeof(CustomerOption), new PropertyMetadata(""));

        public CustomerOption()
        {
            bl = BlApi.BlFactory.GetBL();
        }
        public CustomerOption(int customerID) : this()
        {
            Customer = GetCustomer(customerID);
            Name = Customer.Name;
            PhoneNumber = Customer.PhoneNumber;
            UpdateCustomerCommand = new(UpdateCustomer, param=> CheckValid.CheckValidUpdateCustomer(this.Customer));
            OpenViewParcelrWindowCommand = new(OpenViewParcelrWindow, null);
        }

        private void OpenViewParcelrWindow(object param)
        {
            var parcel = param as ParcelToCustomer;
            new ViewParcel(ParcelConverter.ConvertParcelInCustomerToParcelForList(parcel, Customer)).Show();
        }

        public SimpleCustomer GetCustomer(int id)
        {
            return CustomerInParcelUseBL.ConvertCustomer(bl.GetCustomer(id));
        }

        public void UpdateCustomer(object parameter)
        {
            // if(Station.Name!=Name|| Station.AvailableChargeSlots!= NumOfChargeSlote)
            {
                bl.UpdateCustomer(Customer.Id, Customer.Name, Customer.PhoneNumber);
                MessageBox.Show("Succeed to Update customer:)");
            }
        }

    }
}
