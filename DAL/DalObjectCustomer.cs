using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;

namespace DalObject
{
    public partial class DalObject
    {
        /// <summary>
        /// AddCustomer is a method in the DalObject class.
        /// the method adds a new customer
        /// </summary>
        public void AddCustomer(Customer customer) => Customers.Add(customer);

        /// <summary>
        /// Prepares the list of customer for display
        /// </summary>
        /// <returns>A list of customer</returns>
        public IEnumerable<Customer> GetCustomers() => Customers;

        /// <summary>
        /// Find a customer that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested customer</param>
        /// <returns>A customer for display</returns>
        public Customer GetCustomer(int id)
        {
            return Customers.First(item => item.Id == id);
        }
    }
}
