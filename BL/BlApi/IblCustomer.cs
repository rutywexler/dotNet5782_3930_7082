using System.Collections.Generic;
using BO;

namespace BlApi
{
    public interface IblCustomer
    {
        public void AddCustomer(int id, string name, string phoneNumber, double longitude, double lattitude);
        public Customer GetCustomer(int id);
        public IEnumerable<CustomerForList> GetCustomers();
        public void UpdateCustomer(int id, string name, string PhoneNumber);


    }
}
