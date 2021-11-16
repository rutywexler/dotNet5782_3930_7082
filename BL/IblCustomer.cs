using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IblCustomer
    {
        //חשווווובב!!!! לשנות את סוג המיקום
        public void AddCustomer(int id, int name, int phoneNumber, int position);
        public Customer GetCustomer(int id);
        public IEnumerable<Customer> GetCustomers();
        public void UpdateCustomer(int id, string name, string PhoneNumber);


    }
}
