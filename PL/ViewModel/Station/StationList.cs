using PL.Model;
using PL.UsingBl;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace PL.ViewModel.Station
{
    public class StationList
    {
        public IEnumerable<BaseStationForList> ViewStations { get; set; }
        BlApi.IBL bl;
       
        public StationList()
        {
            bl = BlApi.BlFactory.GetBL();
            ViewStations = ViewStationList();
        }

        public IEnumerable<BaseStationForList> ViewStationList()
        {
            return bl.GetStations().Select(station => StationConverter.ConvertBoStationForListToPo(station));
        }

    }
}
