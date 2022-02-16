using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PL.Enums;

namespace PL
{
    public static class ParcelConverter
    {
        //BlApi.IBL bl { get; set; }
        //public ParcelConverter()
        //{
        //    bl = BlApi.BlFactory.GetBL();
        //}
        //public void AddParcelPl(ParcelToAdd parcel)
        //{
        //    bl.AddParcel(ConvertParcel(parcel));
        //}
        //לתקן בדחיפות!!!
        public static BO.Parcel ConvertToBO(ParcelToAdd parcel)
        {
            return new()
            {
                Priority = (BO.Enums.Priorities)parcel.Priority,
                WeightParcel = (BO.Enums.WeightCategories)parcel.Weight,
                CustomerReceivesTo = CustomerInParcelUseBl.ConvertBackCustomerInParcel(new() { Id = parcel.Target.Id }),
                CustomerSendsFrom = CustomerInParcelUseBl.ConvertBackCustomerInParcel(new() { Id = parcel.Sender.Id })
            };       
        }
        public static ParcelToAdd ConvertToPO(BO.Parcel parcel)
        {
            return new()
            {
                //Priority = (Model.Enums.Priorities?)parcel.Priority,
                //Weight = (Model.Enums.WeightCategories?)parcel.WeightParcel,
                //Target = CustomerInParcelUseBl.ConvertBackCustomerInParcel(new() { Id = parcel.CustomerReceivesTo.Id }),
                //CustomerSendsFrom = CustomerInParcelUseBl.ConvertBackCustomerInParcel(new() { Id = parcel.Sender.Id })
            };
        }

        public static ParcelForList ConvertParcelForListBoToPo(BO.ParcelList parcel)
        {
            return new()
            {
                Id = parcel.Id,
                TargetName = parcel.ReceivesCustomer,
                SenderName = parcel.SendCustomer,
                Weight = (WeightCategories)parcel.Weight,
                Priority = (Priorities)parcel.Priority,
                Status = (DeliveryStatus)parcel.ParcelStatus

            };
        }

        //public static Parcel ConvertParcelForListToParcel(BO.ParcelList parcel)
        //{
        //    return new()
        //    {
        //        Id=parcel.Id,
        //        Sender = parcel.SendCustomer,
        //        Target=parcel.ReceivesCustomer,

        //    };
        //}

        public static ParcelForList ConvertParcelForListBoToPo(BO.Parcel parcel)
        {
            return new()
            {
                Id = parcel.Id,
                TargetName = parcel.CustomerReceivesTo.Name,
                SenderName = parcel.CustomerSendsFrom.Name,
                Weight = (Enums.WeightCategories)parcel.WeightParcel,
                Priority = (Enums.Priorities)parcel.Priority,
                Status = (Enums.DeliveryStatus)parcel.Priority

            };
        }

        public static Parcel ConvertParcelBoToPo(BO.Parcel parcel)
        {
            DroneInParcel drone=new DroneInParcel();
           if (parcel.DroneParcel!=null)
                drone = DroneConverter.ConvertDroneInParcel(parcel.DroneParcel);
            return new()
            {
                Id = parcel.Id,
                Target = CustomerInParcelUseBl.ConvertCustomerInParcel(parcel.CustomerReceivesTo),
                Sender = CustomerInParcelUseBl.ConvertCustomerInParcel(parcel.CustomerSendsFrom),
                Weight = (WeightCategories)parcel.WeightParcel,
                Priority = (Priorities)parcel.Priority,
                Drone1 = drone,
                Created= parcel.TimeCreatedTheParcel,
                Delivered=parcel.DeliveryTime,
                PickedUp=parcel.CollectionTime,
                Scheduled=parcel.AssignmentTime
            };
        }

        public static ParcelToCustomer ConvertParcelAtCustomer(BO.ParcelInCustomer parcelAtCustomer)
        {
            return new ParcelToCustomer()
            {
                Id = parcelAtCustomer.Id,
                Weight = (WeightCategories)parcelAtCustomer.Weight,
                Priority = (Priorities)parcelAtCustomer.Priority,
                Status = (DeliveryStatus)parcelAtCustomer.Status,
                Partner = CustomerInParcelUseBl.ConvertCustomerInParcel(parcelAtCustomer.CustomerInDelivery)
            };
        }
        public static BO.CustomerInParcel ConvertBackCustomerInParcel(CustomerInParcel customerInParcel)
        {
            return new BO.CustomerInParcel()
            {
                Id = customerInParcel.Id,
                Name = customerInParcel.Name
            };
        }



        public static BO.ParcelInCustomer ConvertBackParcelAtCustomer(ParcelToCustomer parcelAtCustomer)
        {
            return new BO.ParcelInCustomer()
            {
                Id = parcelAtCustomer.Id,
                Weight = (BO.Enums.WeightCategories)parcelAtCustomer.Weight,
                Priority = (BO.Enums.Priorities)parcelAtCustomer.Priority,
                Status = (BO.Enums.PackageStatuses)parcelAtCustomer.Status,
                CustomerInDelivery = CustomerInParcelUseBl.ConvertBackCustomerInParcel(parcelAtCustomer.Partner)
            };
        }

        public static ParcelForList ConvertParcelInCustomerToParcelForList(ParcelToCustomer ParcelToCustomer,SimpleCustomer customer)
        {
            return new ParcelForList()
            {
                Id = ParcelToCustomer.Id,
                SenderName = customer.Name,
                TargetName = ParcelToCustomer.Partner.Name,
                Priority= ParcelToCustomer.Priority,
                Status= ParcelToCustomer.Status,
                Weight= ParcelToCustomer.Weight
            };
        }

        public static ParcelInTransfer ConvertParcelInTransfer(BO.ParcelInTransfer parcel)
        {
            return new()
            {
                Id = parcel.Id,
                Priority = (Priorities)parcel.Priority,
                Status = parcel.ParcelStatus,
                Location = LocationConverter.ConvertLocation(parcel.CollectParcelLocation),
                Target = CustomerInParcelUseBl.ConvertCustomerInParcel(parcel.Recipient),
                Sender = CustomerInParcelUseBl.ConvertCustomerInParcel(parcel.Sender),
                Destination = LocationConverter.ConvertLocation(parcel.DeliveryDestination),
                TransportDistance = parcel.DeliveryDistance,
                Weight = (WeightCategories)parcel.Weight
            };
        }

    }
}
