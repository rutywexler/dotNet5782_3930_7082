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
        public void InsertCustomer(Customer customer) => Customers.Add(customer);

        /// <summary>
        /// DisplayCustomer is a method in the DalObject class.
        /// the method allows customer view
        /// </summary>
        public void DisplayCustomer()
        {
            Console.WriteLine("enter customer id:");
            int input;
            ValidRange(0, Customers.Count, out input);
            Console.WriteLine(Customers[input - 1]);
        }

        /// <summary>
        /// ViewListParcels is a method in the DalObject class.
        /// the method displays View the customer list
        /// </summary>
        public void ViewListCustomers()
        {
            foreach (Customer item in Customers)
            {
                Console.WriteLine(item);
            }
        }
    }
    
   
}
