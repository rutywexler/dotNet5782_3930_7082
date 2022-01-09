using System.Collections.Generic;
using BO;

namespace BlApi
{
    public interface IblCustomer
    {
        public void AddCustomer(Customer customerBL);
        public Customer GetCustomer(int id);
        public IEnumerable<CustomerForList> GetCustomers();
        public void UpdateCustomer(int id, string name, string PhoneNumber);
        public void DeleteCustomer(int id);


    }
}
