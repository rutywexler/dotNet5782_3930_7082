using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PL.Enums;

namespace PL
{
     class ViewDroneVM : INotifyPropertyChanged
    {
        BlApi.IBL bl;
        Action refreshDroneList;
        public static bool buttonCacel { get; set; } = false;


        public ViewDroneVM(BlApi.IBL ibl, Drone selectedDrone, Action refreshDroneList)
        {
            bl = ibl;
            SelectedDrone = selectedDrone;
            this.refreshDroneList = refreshDroneList;
            SendingTheDroneForChargingCommand = new RelayCommand(SendingTheDroneForCharging, null);
            SendingTheDroneForDeliveryCommand = new RelayCommand(SendingTheDroneForDelivery, null);
            ReleaseDroneFromChargingCommand = new RelayCommand(ReleaseDroneFromCharging, null);
            UpdateModelCommand = new RelayCommand(UpdateModel, param => CheckValid.CheckValidUpdateDrone(this.SelectedDrone));
            //ParcelCollectionCommand = new RelayCommand(ParcelCollection, null);
            //ParcelDeliveryCommand = new RelayCommand(ParcelDelivery, null);
            StartSimulatorCommand = new RelayCommand(Auto_Click, null);
            ParcelTreatedByDroneCommand = new RelayCommand(parcelTreatedByDrone, null);
            StopTheAuto = new RelayCommand(Manual, null);

        }
        public ViewDroneVM()
        {

        }

        private void Manual(object obj)
        {
            worker?.CancelAsync();
        }


        private void Refresh()
        {
            SelectedDrone = PL.DroneConverter.ConvertDrone(bl.GetDrone(SelectedDrone.Id));
            refreshDroneList();
        }
        public RelayCommand SendingTheDroneForChargingCommand { get; set; }
        public RelayCommand ParcelTreatedByDroneCommand { get; set; }
        public RelayCommand StopTheAuto { get; set; }
        private void SendingTheDroneForCharging(object obj)
        {
            try
            {
                bl.SendDroneForCharge(SelectedDrone.Id);
                Refresh();
                MessageBox.Show("succees to Send Drone For Charge");
            }
            catch (BL.InvalidDroneStateException ex)
            {
                MessageBox.Show($"failed to Send Drone For Charge,{ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"failed to Send Drone For Charge,{ex.Message}");
            }
            catch(BL.InValidActionException ex)
            {
                MessageBox.Show($"failed to Send Drone For Charge,{ex.Message}");
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
            catch (BL.NotExsistSutibleParcelException ex)
            {
                MessageBox.Show($"failed to Sending The Drone For Delivery, {ex.Message}");
            }
            catch (BL.InValidActionException ex)
            {
                MessageBox.Show($"failed to Sending The Drone For Delivery, {ex.Message}");
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
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Failed to release from charging, {ex.Message}");
            }
            catch (InvalidEnumArgumentException ex)
            {
                MessageBox.Show($"Failed to release from charging, {ex.Message}");
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
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"Failed to update the drone, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Failed to update the drone, {ex.Message}");
            }
        }

        public void parcelTreatedByDrone(object param)
        {

            if (SelectedDrone.Status == DroneStatuses.DELIVERY)
            {
                if (SelectedDrone.DeliveryByTransfer.Status == true)
                {
                    ParcelDelivery(SelectedDrone.Id);
                }
                else
                {
                    ParcelCollection(SelectedDrone.Id);
                }
            }
            else
            {
                SendingTheDroneForDelivery(SelectedDrone.Id);
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
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Failed to Parcel Collection By Dronee,{ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"Failed to Parcel Collection By Dronee,{ex.Message}");

            }
            catch (BL.Exception_ThereIsInTheListObjectWithTheSameValue ex)
            {
                MessageBox.Show($"Failed to Parcel Collection By Dronee,{ex.Message}");

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
        //public void Window_Closing(object sender, CancelEventArgs e)
        //{
        //    buttonCacel = true;
        //    if(worker!=null)
        //         worker.CancelAsync();     
        //}

        #region simulator

        public BackgroundWorker worker;
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
                worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
                worker.DoWork += (sender, args) => bl.StartSimulator(updateDrone, (int)args.Argument, checkStop);
                worker.RunWorkerCompleted += (sender, args) =>
                {

                    Auto = false;
                    if (buttonCacel)
                    {
                        App.Current.Windows.Cast<Window>().First(worker => worker.Title == "ViewDrone").Close();
                    }
                };
                
                //worker.RunWorkerCompleted += (sender, args) => CloseDroneWindow();
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
