﻿using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class AddStationViewModel
    {
        BlApi.IBL bl;
        public BaseStationToAdd BaseStation { get; set; }
        public RelayCommand AddStationCommand { get; set; }
        public AddStationViewModel()
        {
            bl = BlApi.BlFactory.GetBL();
            BaseStation = new();
            AddStationCommand = new(AddStation,param=>CheckValid.CheckValidAddStation(this.BaseStation));
        }

        private void AddStation(object parameter)
        {
            try
            {
                var baseStationBO = StationConverter.ConvertPoBaseStationToBO(BaseStation);
                
                bl.AddStation(baseStationBO);
                MessageBox.Show("Success to add Station:)");

            }
            catch (BL.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {

                MessageBox.Show($"Didnt succeed to add the parcel:( Enter details Again ,{ex.Message}");
            }
        }
    }
}
