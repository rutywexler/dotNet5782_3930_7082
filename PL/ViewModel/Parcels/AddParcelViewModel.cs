using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using static PL.Enums;



namespace PL
{
    public class AddParcelViewModel
    {
        BlApi.IBL bl;
        public AddParcelViewModel()
        {
            //Custom control
            bl = BlApi.BlFactory.GetBL();
            Parcel = new();
            AddParcelCommand = new(ToAddParcel, param => CheckValid.CheckValidAddParcel(this));
            CustomersId = new CustomerUseBl().GetCustomers();
            Priorities = Enum.GetValues(typeof(Priorities));
            Weight = Enum.GetValues(typeof(WeightCategories));
        }
        public ParcelToAdd Parcel { get; set; }
        public RelayCommand AddParcelCommand { get; set; }
        public Array Priorities { get; set; }
        public Array Weight { get; set; }
        public IEnumerable<CustomerInParcel> CustomersId  { get; set; }

        public void ToAddParcel(object param)
        {
            try
            {
                var parcelBO = ParcelConverter.ConvertToBO(Parcel);
                bl.AddParcel(parcelBO);
                MessageBox.Show("Success to add parcel:)");

            }
            catch (KeyNotFoundException)
            {

                MessageBox.Show("Didnt succeed to add the parcel:( Enter details Again");
            }
        }

    }
}
