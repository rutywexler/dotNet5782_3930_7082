using PL.PO;
using System;
using System.Collections.Generic;
using static PL.Model.Enum;

namespace PL.ViewModel.Parcel
{
    public class AddParcel
    {

        public ParcelToAdd Parcel { get; set; }
        public RelayCommand AddParcelCommand { get; set; }
        public Array Priorities { get; set; }
        public Array Weight { get; set; }
        public List<int> CustomersId  { get; set; }
        public AddParcel()
        {
            Parcel = new();
            AddParcelCommand=new()
        }

        public void ToAddParcel(object param)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
