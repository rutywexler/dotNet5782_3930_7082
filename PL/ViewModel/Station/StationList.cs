using PL.Model;
using PL;
using PL.View.Station;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL.ViewModel.Station
{
    public class StationList : DependencyObject
    {

        public RelayCommand GroupingStation { get; set; }
        public ObservableCollection<string> ComboboxItems { get; set; }
        public string SelectedItemGrouping { get; set; }
        public ListCollectionView ViewStations
        {
            get { return (ListCollectionView)GetValue(ViewStationsProperty); }
            set { SetValue(ViewStationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewStations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewStationsProperty =
            DependencyProperty.Register("ViewStations", typeof(ListCollectionView), typeof(StationList), new PropertyMetadata(null));


        public RelayCommand OpenAddStationWindow { get; set; }
        public RelayCommand OpenViewStationWindowCommand { get; set; }
        BlApi.IBL bl;

        public StationList()
        {
            bl = BlApi.BlFactory.GetBL();
            ComboboxItems = new ObservableCollection<string>(typeof(BaseStationForList).GetProperties().Where(prop => prop.PropertyType.IsValueType || prop.PropertyType == typeof(string)).Select(prop => prop.Name));
            ViewStations = new ListCollectionView(ViewStationList().ToList());
            OpenAddStationWindow = new(OpenAddWindow, null);
            OpenViewStationWindowCommand = new(OpenStationView);
            GroupingStation = new(Grouping, null);
        }
        private void RefreshList()
        {
            ViewStations = new ListCollectionView(ViewStationList().ToList());
        }
        public void OpenAddWindow(object param)
        {
            new AddStation().ShowDialog();
            RefreshList();
        }
        public void OpenStationView(object param)
        {
            var station = param as BaseStationForList;
            new ViewStation(station).ShowDialog();
            RefreshList();
        }
        public IEnumerable<BaseStationForList> ViewStationList()
        {
            return bl.GetStations().Select(station => StationConverter.ConvertBoStationForListToPo(station));
        }
        public void Grouping(object param)
        {
            //for (int i = 0; i < ViewStations.GroupDescriptions.Count; i++)
            //{
            //    ViewStations.GroupDescriptions.RemoveAt(i);
            //}
            ViewStations.GroupDescriptions.Clear();
            ViewStations.GroupDescriptions.Add(new PropertyGroupDescription(param.ToString()));
        }
    }
}
