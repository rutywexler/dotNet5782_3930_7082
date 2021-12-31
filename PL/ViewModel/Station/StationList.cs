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
        public RelayCommand OpenViewStationWindowCommand { get; set; }
        BlApi.IBL bl;

        public StationList()
        {
            bl = BlApi.BlFactory.GetBL();
            ViewStations = ViewStationList();
            OpenAddStationWindow = new(OpenAddWindow, null);
            OpenViewStationWindowCommand = new(OpenStationView);
        }
        private void RefreshList()
        {
            //
        }
        public static void OpenAddWindow(object param)
        {
            new AddStation().Show();
        }
        public static void OpenStationView(object param)
        {
            var station = param as BaseStationForList;
            
            new ViewStation(station).Show();
        }
        public IEnumerable<BaseStationForList> ViewStationList()
        {
            return bl.GetStations().Select(station => StationConverter.ConvertBoStationForListToPo(station));
        }

    }
}
