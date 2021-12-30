using PL.Model;
using PL.UsingBl;
using PL.View.Parcel;
using System.Collections.Generic;
using System.Linq;

namespace PL.ViewModel.Parcel
{
    public class ViewParcelList
    {
        public IEnumerable<ParcelForList> ViewParcels { get; set; }
        BlApi.IBL bl;

        public RelayCommand ViewParcelsList { get; set; }
        public RelayCommand OpenAddParcelWindow { get; set; }
        public ViewParcelList()
        {
            bl = BlApi.BlFactory.GetBL();
            ViewParcels = GetParcels();
            OpenAddParcelWindow = new(OpenAddWindow, null);
        }

        public IEnumerable<ParcelForList> GetParcels()
        {
            return bl.GetParcels().Select(parcel => ParcelConverter.ConvertParcelForListBoToPo(parcel)).ToList();
        }

        public static void OpenAddWindow(object param)
        {
            new AddParcels().Show();
        }


    }

}
