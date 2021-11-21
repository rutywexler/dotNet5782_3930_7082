using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.BO.Enums;
//לא גמוררר בכללללל
namespace IBL
{
    public partial class BL : IblCustomer
    {
        public void AddCustomer(int id, int name, int phoneNumber, int position)
        {
            throw new NotImplementedException();
            
        }

        public Customer GetCustomer(int id)
        {
            var customer = dal.GetCustomer(id);
            return new Customer()
            {
                Id = customer.Id,
                Location = new Location() { Lattitude = customer.Lattitude, Longitude = customer.Longitude },
                Name = customer.Name,
                PhoneNumber = customer.Phone,
                getCustomerSendParcels = FindSendParcels(id),
                getCustomerReceivedParcels = FindReceivedParcels(id),
            };
        }

        private List<ParcelInCustomer> FindReceivedParcels(int id)
        {
            throw new NotImplementedException();
        }

        private List<ParcelInCustomer> FindSendParcels(int id)
        {
            List<ParcelInCustomer> sendParcels =dal.GetParcels()
                .Where(p => p.TargetId == id)
                .Select(parcel =>
                    new ParcelInCustomer
                    {
                        //דרוש תיקון דחוף
                        Id = parcel.Id,
                        Sender = parcel.SenderId,
                        Target = parcel.TargetId,
                        Priority = (Priorities)parcel.Priority,
                        Weight = (WeightCategories)parcel.Weight

                    });
            return sendParcels;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(int id, string name, string PhoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
