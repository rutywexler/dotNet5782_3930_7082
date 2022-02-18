using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dal.DataSource;
using System.Runtime.CompilerServices;

namespace Dal
{
    public partial class DalObject
    {

        /// <summary>
        /// Gets parameters and create new customer 
        /// </summary>
        /// <param name="phone">The customer`s number phone</param>
        /// <param name="name">The customer`s name</param>
        /// <param name="longitude">>The position of the customer in relation to the longitude</param>
        /// <param name="latitude">>The position of the customer in relation to the latitude</param>
         [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        {
            if (ExistsIDCheck(DataSource.Customers, id))
                throw new ThereIsAnotherObjectWithThisUniqueID();
            Customer newCustomer = new Customer();
            newCustomer.Id = id;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Lattitude = latitude;
            newCustomer.Longitude = longitude;
            Customers.Add(newCustomer);
        }

        /// <summary>
        /// Prepares the list of customer for display
        /// </summary>
        /// <returns>A list of customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Customer> GetCustomers()
        {
            return Customers.Where(customer => customer.IsDeleted == false);
        }

        /// <summary>
        /// Find a customer that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested customer</param>
        /// <returns>A customer for display</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            if (Customers.Equals(default(Customer)))
                throw new KeyNotFoundException("There isn't suitable customer in the data");
            return Customers.FirstOrDefault(item => item.Id == id);
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void RemoveCustomer(int id)
        //{
        //    Customer customer = Customers.FirstOrDefault(customer => customer.Id == id);
        //    Customers.Remove(customer);
        //    //customer.IsDeleted = true;
        //    //Customers.Add(customer);
        //}

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
        {
            var d = Customers.FirstOrDefault(item => item.Id == customer.Id);
            if (customer.Equals(default(Customer)))
                throw new KeyNotFoundException("There isnt suitable customer in the data!");
            Customers.Remove(d);
            AddCustomer(customer.Id,  customer.Phone,customer.Name, customer.Longitude, customer.Lattitude);
        }

        public void RemoveCustomer(int id)
        {
            var customer = GetCustomer(id);
            Customers.Remove(customer);
        }


    }
}
