using IBL;
using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
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
