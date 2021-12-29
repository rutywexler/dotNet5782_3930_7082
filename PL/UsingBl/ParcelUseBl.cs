
using PL.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.UsingBl
{
    public class ParcelUseBl
    {
        public BlApi.IBL bl { get; set; }
        public ParcelUseBl()
        {
            bl = BlApi.BlFactory.GetBL();
        }
        public void AddParcelPl(ParcelToAdd parcel)
        {
            bl.AddParcel(ConvertParcel(parcel));
        }

        private BO.Parcel ConvertParcel(ParcelToAdd parcel)
        {
            BO.Parcel parcelBo=new BO.Parcel();
            parcelBo.Priority = (BO.Enums.Priorities)parcel.Priority;
            parcelBo.WeightParcel = (BO.Enums.WeightCategories)parcel.Weight;
            parcelBo.CustomerSendsFrom = CustomerInParcelUseBl.ConvertBackCustomerInParcel(new() { Id = parcel.Sender.Id });
            parcelBo.CustomerReceivesTo= CustomerInParcelUseBl.ConvertBackCustomerInParcel(new() { Id = parcel.Target.Id });
            return parcelBo;
        }
    }
}
