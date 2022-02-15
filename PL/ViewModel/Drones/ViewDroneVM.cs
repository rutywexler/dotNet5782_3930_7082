﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModel.Drones
{
    class ViewDroneVM : INotifyPropertyChanged
    {
        BlApi.IBL bl;
        Action refreshDroneList;

        public ViewDroneVM(BlApi.IBL ibl, Drone selectedDrone, Action refreshDroneList)
        {
            bl = ibl;
            SelectedDrone = selectedDrone;
            this.refreshDroneList = refreshDroneList;
            SendingTheDroneForChargingCommand = new RelayCommand(SendingTheDroneForCharging);
            SendingTheDroneForDeliveryCommand = new RelayCommand(SendingTheDroneForDelivery);
            ReleaseDroneFromChargingCommand = new RelayCommand(ReleaseDroneFromCharging);
            UpdateModelCommand = new RelayCommand(UpdateModel);
            ParcelCollectionCommand = new RelayCommand(ParcelCollection);
            ParcelDeliveryCommand = new RelayCommand(ParcelDelivery);
            StartSimulatorCommand = new RelayCommand(Auto_Click);
        }
        private void Refresh()
        {
            SelectedDrone = PL.DroneConverter.ConvertDrone(bl.GetDrone(SelectedDrone.Id));
            refreshDroneList();
        }
        public RelayCommand SendingTheDroneForChargingCommand { get; set; }
        private void SendingTheDroneForCharging(object obj)
        {
            try
            {
                bl.SendDroneForCharge(SelectedDrone.Id);
                Refresh();
                MessageBox.Show("succees to Send Drone For Charge");
            }
            catch
            {
                MessageBox.Show("failed to Send Drone For Charge");
            }
        }
        public RelayCommand SendingTheDroneForDeliveryCommand { get; set; }

        private void SendingTheDroneForDelivery(object obj)
        {
            try
            {
                bl.AssignParcelToDrone(SelectedDrone.Id);
                Refresh();
                MessageBox.Show("succees to Sending The Drone For Delivery");
            }
            catch
            {
                MessageBox.Show("failed to Sending The Drone For Delivery");
            }
        }

        public RelayCommand ReleaseDroneFromChargingCommand { get; set; }
        private void ReleaseDroneFromCharging(object obj)
        {
            try
            {
                bl.ReleaseDroneFromCharging(SelectedDrone.Id);
                Refresh();
                MessageBox.Show("the drone succeeded to release from charging ", "success", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("Failed to release from charging");
            }
        }

        public RelayCommand UpdateModelCommand { get; set; }
        private void UpdateModel(object obj)
        {
            try
            {
                bl.UpdateDrone(SelectedDrone.Id, SelectedDrone.Model);
                Refresh();
                MessageBox.Show("the drone succeeded to update ", "success", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("Failed to update the drone");
            }
        }

        public RelayCommand ParcelCollectionCommand { get; set; }
        private void ParcelCollection(object obj)
        {
            try
            {
                bl.ParcelCollectionByDrone(SelectedDrone.Id);
                Refresh();
                MessageBox.Show("succees Parcel Collection By Drone");
            }
            catch
            {
                MessageBox.Show("Failed to Parcel Collection By Dronee");
            }
        }

        public RelayCommand ParcelDeliveryCommand { get; set; }
        private void ParcelDelivery(object obj)
        {
            try
            {
                bl.DeliveryParcelByDrone(SelectedDrone.Id);
                Refresh();
                MessageBox.Show("succees Delivery Parcel By Drone");
            }
            catch
            {
                MessageBox.Show("Failed to Delivery Parcel By Drone");
            }

        }

        private Drone selectedDrone;

        public Drone SelectedDrone
        {
            get { return selectedDrone; }
            set
            {
                selectedDrone = value;
                OnPropertyChange(nameof(SelectedDrone));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string memberName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        #region simulator

        BackgroundWorker worker;
        private bool auto;

        public bool Auto
        {
            get => auto;
            set
            {
                auto = value;
                OnPropertyChange(nameof(Auto));
            }
        }
        private void updateDrone() => worker.ReportProgress(0);
        private bool checkStop() => worker.CancellationPending;
        public RelayCommand StartSimulatorCommand { get; set; }

        private void Auto_Click(object obj)
        {
            if (!Auto)
            {
                Auto = true;
                worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true, };
                worker.DoWork += (sender, args) => bl.StartSimulator(updateDrone, (int)args.Argument, checkStop);
                worker.RunWorkerCompleted += (sender, args) => Auto = false;
                worker.ProgressChanged += (sender, args) => updateDroneView();
                worker.RunWorkerAsync(SelectedDrone.Id);
            }
            else
            {
                worker?.CancelAsync();
            }
        }

        private void updateDroneView()
        {
            SelectedDrone = PL.DroneConverter.ConvertDrone(bl.GetDrone(SelectedDrone.Id));
            refreshDroneList();
        }

        #endregion
    }
}
