﻿using PL.Model;
using PL.UsingBl;
using PL.View.Station;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL.ViewModel.Station
{
    public class StationList:DependencyObject
    {
       // public ObservableCollection<BaseStationForList> ViewStations { get; set; }


        public ObservableCollection<BaseStationForList> ViewStations
        {
            get { return (ObservableCollection<BaseStationForList>)GetValue(ViewStationsProperty); }
            set { SetValue(ViewStationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewStations.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewStationsProperty =
            DependencyProperty.Register("ViewStations", typeof(ObservableCollection<BaseStationForList>), typeof(StationList), new PropertyMetadata(null));


        public RelayCommand OpenAddStationWindow { get; set; }
        public RelayCommand OpenViewStationWindowCommand { get; set; }
        BlApi.IBL bl;

        public StationList()
        {
            bl = BlApi.BlFactory.GetBL();
            ViewStations =new ObservableCollection<BaseStationForList>( ViewStationList());
            OpenAddStationWindow = new(OpenAddWindow, null);
            OpenViewStationWindowCommand = new(OpenStationView);
        }
        private void RefreshList()
        {
            ViewStations = new ObservableCollection<BaseStationForList>(ViewStationList());
        }
        public  void OpenAddWindow(object param)
        {
            new AddStation().ShowDialog();
            RefreshList();
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
