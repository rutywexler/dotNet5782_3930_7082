using BO;
using PL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for ViewDrone.xaml
    /// </summary>
    public partial class ViewDrone : Window ,INotifyPropertyChanged
    {
        BlApi.IBL MyIbl;
        Action RefreshDroneList;
        private DroneForList drone;
        
        public bool auto { get; set; }
        public ViewDrone()
        {
            InitializeComponent();
        }

        public ViewDrone(BlApi.IBL ibl, DroneForList selectedDrone, Action refreshDroneList)
            : this()
        {
            MyIbl = ibl;
            SelectedDrone = selectedDrone;
            DataContext = selectedDrone;
            RefreshDroneList = refreshDroneList;
        }

        public ViewDrone(DroneForList drone) : this()
        {
            this.drone = drone;
        }

        private DroneForList selectedDrone;

        public DroneForList SelectedDrone
        {
            get { return selectedDrone; }
            set {
                selectedDrone = value;
                OnPropertyChange("SelectedDrone");
            }
        }


        private void SendingTheDroneForCharging(object sender, RoutedEventArgs e)
        {
            try
            {
                MyIbl.SendDroneForCharge(SelectedDrone.Id);
                RefreshDroneList();
                MessageBox.Show("succees to Send Drone For Charge");
            }
            catch
            {
                MessageBox.Show("failed to Send Drone For Charge");
            }

        }

        private void SendingTheDroneForDelivery(object sender, RoutedEventArgs e)
        {
            try
            {
                MyIbl.AssignParcelToDrone(SelectedDrone.Id);
                RefreshDroneList();
                MessageBox.Show("succees to Sending The Drone For Delivery");
            }
            catch
            {
                MessageBox.Show("failed to Sending The Drone For Delivery");
            }

        }

        private void ReleaseDroneFromCharging(object sender, RoutedEventArgs e)
        {
            //MyIbl.ReleaseDroneFromCharging(SelectedDrone.DroneId);
            RefreshDroneList();
        }


        private void UpdateModel(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedDrone.Model = UpdateModelContext.Text;
                RefreshDroneList();
                MessageBox.Show("the drone succeeded to update ", "success", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("Failed to update the drone");
            }


        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ParcelCollection(object sender, RoutedEventArgs e)
        {
            try
            {
                MyIbl.ParcelCollectionByDrone(SelectedDrone.Id);
                RefreshDroneList();
                MessageBox.Show("succees Parcel Collection By Drone");
            }
            catch
            {
                MessageBox.Show("Failed to Parcel Collection By Dronee");
            }

        }

        private void ParcelDelivery(object sender, RoutedEventArgs e)
        {
            try
            {
                MyIbl.DeliveryParcelByDrone(SelectedDrone.Id);
                RefreshDroneList();
                MessageBox.Show("succees Delivery Parcel By Drone");
            }
            catch
            {
                MessageBox.Show("Failed to Delivery Parcel By Drone");
            }

        }

        #region simulator
        BackgroundWorker worker;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string memberName)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs( memberName));
        }

        private void updateDrone() => worker.ReportProgress(0);
        private bool checkStop() => worker.CancellationPending;

        private void Auto_Click(object sender, RoutedEventArgs e)
        {
            if(!auto)
            {
            auto = true;
            worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true, };
            worker.DoWork += (sender, args) => MyIbl.StartSimulator(updateDrone,(int)args.Argument,  checkStop);
            worker.RunWorkerCompleted += (sender, args) => auto = false;
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
           SelectedDrone = PL.DroneConverter.ConvertDroneToList(MyIbl.GetDrones().FirstOrDefault(Drone => Drone.DroneId == SelectedDrone.Id));
        }

       


        #endregion
        //    private void Add_Click(object sender, RoutedEventArgs e)
        //    {
        //        try
        //        {
        //            bl.AddDrone(Drone);
        //            var drone = bl.GetDroneForList(Drone.Id);
        //            if ((Model.StatusSelector == DroneStatuses.None || drone.Status == Model.StatusSelector) &&
        //                (Model.WeightSelector == WeightCategories.None || drone.MaxWeight == Model.WeightSelector))
        //                Model.Drones.Add(drone);
        //            Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Failed to add: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}
    }
}
