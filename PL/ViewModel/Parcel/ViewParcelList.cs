using BO;
using PL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public IEnumerable<ParcelForList> GetParcels()
        {
            return bl.GetParcels().Select(parcel => ConvertParcelForListBoToPo(parcel)).ToList();
        }

       
    }
       
}
