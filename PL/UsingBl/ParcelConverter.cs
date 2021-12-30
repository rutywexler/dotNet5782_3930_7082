﻿
using BO;
using PL.Model;
using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.UsingBl
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
            throw new NotImplementedException();
            //return new()
            //{
            //    Priority = (BO.Enums.Priorities)parcel.Priority,
            //    WeightParcel = (BO.Enums.WeightCategories)parcel.Weight,
            //    CustomerReceivesTo = CustomerInParcelUseBl.ConvertBackCustomerInParcel(new() { Id = parcel.Target.Id }),
            //    CustomerSendsFrom = CustomerInParcelUseBl.ConvertBackCustomerInParcel(new() { Id = parcel.Sender.Id })
            //};
        }

        public static ParcelForList ConvertParcelForListBoToPo(ParcelList parcel)
        {
            return new()
            {
                Id = parcel.Id,
                TargetName = parcel.ReceivesCustomer,
                SenderName = parcel.SendCustomer,
                Weight = (Model.Enums.WeightCategories)parcel.Weight,
                Priority = (Model.Enums.Priorities)parcel.Priority,
                Status = (Model.Enums.DeliveryStatus)parcel.Priority

            };
        }
    }
}
