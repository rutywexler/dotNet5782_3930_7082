using PL.Converters;
using PL.Drones;
using PL.Model;
using PL.UsingBl;
using PL.View.Customer;
using PL.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PL.Model.Enums;

namespace PL.ViewModel.Parcels
{
    public class ViewParcelVM
    {
        BlApi.IBL bl;
        public Parcel Parcel { get; set; }
        public RelayCommand DeleteParcelCommand { get; set; }
        public RelayCommand OpenDroneWindowCommand { get; set; }
        public RelayCommand OpenCustomerWindowCommand { get; set; }
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
        }

        private ParcelForList GetParcel(int id)
        {
            return ParcelConverter.ConvertParcelForListBoToPo(bl.GetParcel(id));
        }

        public void OpenCustomerWindow(object param)
        {
            var Customer = param as CustomerInParcel;
            new ViewCustomer(Customer.Id).Show();
        }
        public void DeleteParcel(object obj)
        {
            bl.DeleteParcel(Parcel.Id);
        }

        public void OpenDroneWindow(object param)
        {
           //DroneForList drone = DroneConverter.ConvertDroneToList(param as DroneInParcel);
           //new ViewDrone(drone).Show();
          
        }

    }
}
