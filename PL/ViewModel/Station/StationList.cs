using PL.Model;
using PL.UsingBl;
using PL.View.Station;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace PL.ViewModel.Station
{
    public class StationList
    {
        public IEnumerable<BaseStationForList> ViewStations { get; set; }
        public RelayCommand OpenAddStationWindow { get; set; }
        BlApi.IBL bl;
       
        public StationList()
        {
            bl = BlApi.BlFactory.GetBL();
            ViewStations = ViewStationList();
            OpenAddStationWindow = new(OpenAddWindow, null);
        }

        public static void OpenAddWindow(object param)
        {
            new AddStation().Show();
        }

        public IEnumerable<BaseStationForList> ViewStationList()
        {
            return bl.GetStations().Select(station => StationConverter.ConvertBoStationForListToPo(station));
        }

    }
}
