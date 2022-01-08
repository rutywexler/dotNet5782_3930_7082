using PL.Model;
using PL.UsingBl;
using PL.View.Parcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModel.Parcel
{
    public class ViewParcelVM
    {
        BlApi.IBL bl;
        public ParcelForList Parcel { get; set; }
        public RelayCommand DeleteParcelCommand { get; set; }
        public ViewParcelVM()
        {
            bl = BlApi.BlFactory.GetBL();
        }
        public ViewParcelVM(ParcelForList parcel) : this()
        {
            Parcel = GetParcel(parcel.Id);
            DeleteParcelCommand = new(DeleteParcel, null);
        }

        private ParcelForList GetParcel(int id)
        {
            return ParcelConverter.ConvertParcelForListBoToPo(bl.GetParcel(id));
        }

        public void DeleteParcel(object obj)
        {
            try
            {
                bl.DeleteParcel(Parcel.Id);
            }            
            catch (Exception)//למצוא שגיאה מתאימה 
            {
                throw;
            }
            MessageBox.Show("Succeed to delete Parcel");
        }

    }
}
