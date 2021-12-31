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
    class UpDateStation : DependencyObject
    {
        BlApi.IBL bl;
        public RelayCommand UpdateStationCommand { get; set; }
        public BaseStation Station
        {
            get { return (BaseStation)GetValue(StationDP); }
            set { SetValue(StationDP, value); }
        }
        public static readonly DependencyProperty StationDP =
        DependencyProperty.Register("Station", typeof(BaseStation), typeof(UpDateStation), new PropertyMetadata(new BaseStation()));

        public string Name
        {
            get { return (string)GetValue(nameDP); }
            set { SetValue(nameDP, value); }
        }

        public static readonly DependencyProperty nameDP =
       DependencyProperty.Register("Name", typeof(string), typeof(UpDateStation), new PropertyMetadata(""));

        public int NumOfChargeSlote
        {
            get { return (int)GetValue(numOfChargeSloteDP); }
            set { SetValue(numOfChargeSloteDP, value); }
        }

        public static readonly DependencyProperty numOfChargeSloteDP =
       DependencyProperty.Register("NumOfChargeSlote", typeof(int), typeof(UpDateStation), new PropertyMetadata(0));

        public UpDateStation()
        {
            bl = BlApi.BlFactory.GetBL();
            //לשנות שיקבל רחפן מסוים
            Station = GetStation(1);
            Name = Station.Name;
            NumOfChargeSlote = Station.AvailableChargeSlots;
            UpdateStationCommand = new(UpdateStation, null);
        }

        private BaseStation GetStation(int id)
        {
            return StationConverter.ConvertStationBlToPo(bl.GetStation(id));
        }

        private void UpdateStation(object parameter)
        {
            if(Station.Name!=Name|| Station.AvailableChargeSlots!= NumOfChargeSlote)
            {
                bl.UpdateStation(Station.Id, Name, NumOfChargeSlote);
                MessageBox.Show("Succeed to Update station");
            }
        }


    }
}

