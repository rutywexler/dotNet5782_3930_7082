using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using IBL.BO;

namespace IBL
{
    public interface IblCustomer
    {
        public void AddCustomer(int id, string name, string phoneNumber, Location position);
        public Customer GetCustomer(int id);
        public IEnumerable<CustomerForList> GetCustomers();
        public void UpdateCustomer(int id, string name, string PhoneNumber);


    }
}
