﻿using PL.Model;
using PL.UsingBl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModel.Station
{
    public class UpDateStation : DependencyObject
    {
        BlApi.IBL bl;
        public RelayCommand UpdateStationCommand { get; set; }
        public RelayCommand DeleteStationCommand { get; set; }



        public BaseStation Station
        {
            get { return (BaseStation)GetValue(StationProperty); }
            set { SetValue(StationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Station.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StationProperty =
            DependencyProperty.Register("Station", typeof(BaseStation), typeof(UpDateStation), new PropertyMetadata(null));

        public UpDateStation()
        {
            bl = BlApi.BlFactory.GetBL();
        }
        public UpDateStation(BaseStationForList station) : this()
        {
            Station = GetStation(station.Id);
            UpdateStationCommand = new(UpdateStation, null);
            DeleteStationCommand = new(DeleteStation, null);
        }

        private void DeleteStation(object param)
        {
            try
            {
                bl.RemoveStation(Station.Id);
            }
            catch (Exception)//למצוא שגיאה מתאימה 
            {
                throw;
            }
            MessageBox.Show("Succeed to delete station");
        }
        private BaseStation GetStation(int id)
        {
            return StationConverter.ConvertStationBlToPo(bl.GetStation(id));
        }

        public void UpdateStation(object parameter)
        {
           // if(Station.Name!=Name|| Station.AvailableChargeSlots!= NumOfChargeSlote)
            {
                bl.UpdateStation(Station.Id, Station.Name, Station.AvailableChargeSlots);
                MessageBox.Show("Succeed to Update station");
            }
        }
    }
}


