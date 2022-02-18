using PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL
{
    public class ViewParcelVM
    {
        BlApi.IBL bl;
        public Parcel Parcel { get; set; }
        public RelayCommand DeleteParcelCommand { get; set; }
        public RelayCommand OpenDroneWindowCommand { get; set; }
        public RelayCommand OpenCustomerWindowCommand { get; set; }
        public RelayCommand CollectParcel { get; set; }
        public RelayCommand DeliveryParcel { get; set; }

        public ViewParcelVM()
        {
            bl = BlApi.BlFactory.GetBL();
        }
        public ViewParcelVM(ParcelForList parcel) : this()
        {
            Parcel = ParcelConverter.ConvertParcelBoToPo(bl.GetParcel(parcel.Id));
            DeleteParcelCommand = new(DeleteParcel, null);
            OpenDroneWindowCommand = new(OpenDroneWindow, null);
            OpenCustomerWindowCommand = new(OpenCustomerWindow, null);
            CollectParcel = new(CollectTheParcel, null);
            DeliveryParcel = new(DeliveryTheParcel, null);
        }

        private ParcelForList GetParcel(int id)
        {
            return ParcelConverter.ConvertParcelForListBoToPo(bl.GetParcel(id));
        }
        private void CollectTheParcel(object param)
        {
            //var parcel = param as Parcel;
            try 
            {
                bl.ParcelCollectionByDrone(Parcel.Drone1.Id);
                MessageBox.Show("succees collect Parcel By Drone");
            }

            catch
            {
                MessageBox.Show("Failed to collect Parcel By Drone");
            }
        }


        private void DeliveryTheParcel(object param)
        {
            //var parcel = param as Parcel;
            try
            {
                bl.AssignParcelToDrone(Parcel.Id);
                MessageBox.Show("succees Delivery Parcel By Drone");
            }
            catch
            {
                MessageBox.Show("Failed to Delivery Parcel By Drone");
            }

        }

        public void OpenCustomerWindow(object param)
        {
            var Customer = param as CustomerInParcel;
            new ViewCustomer(Customer.Id).Show();
        }
        public void DeleteParcel(object obj)
        {

            try
            {
                bl.DeleteParcel(Parcel.Id);
            }
            catch (BL.InValidActionException ex) 
            {
                MessageBox.Show($"the parcel cant be deleted because the parcel belong to drone:( ,{ex.Message} ");
                return;
            }
            MessageBox.Show("Succeed to delete Parcel");
            

        }

        public void OpenDroneWindow(object param)
        {
           //DroneForList drone = DroneConverter.ConvertDroneToList(param as DroneInParcel);
           //new ViewDrone(drone).Show();
          
        }

    }
}
