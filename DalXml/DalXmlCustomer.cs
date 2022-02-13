using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    public partial class DalXml
    {
        private readonly string customersPath = "Customers.xml";

        /// <summary>
        /// Gets parameters and create new customer 
        /// </summary>
        /// <param name="phone">The customer`s number phone</param>
        /// <param name="name">The customer`s name</param>
        /// <param name="longitude">>The position of the customer in relation to the longitude</param>
        /// <param name="latitude">>The position of the customer in relation to the latitude</param>
        //public void AddCustomer(int id, string phone, string name, double longitude, double latitude)
        //{

        //    if (DalObject.DalObject.ExistsIDCheck(DataSource.Customers, id))
        //        throw new Exception_ThereIsInTheListObjectWithTheSameValue();
        //    Customer newCustomer = new Customer();
        //    newCustomer.Id = id;
        //    newCustomer.Name = name;
        //    newCustomer.Phone = phone;
        //    newCustomer.Lattitude = latitude;
        //    newCustomer.Longitude = longitude;
        //    Customers.Add(newCustomer);
        //}
        private Customer ConvertXElementToCustomerObject(XElement element)
        {
            return new Customer()
            {
                Id = int.Parse(element.Element(nameof(Customer.Id)).Value),
                Name = element.Element(nameof(Customer.Name)).Value,
                Phone = element.Element(nameof(Customer.Phone)).Value,
                Longitude = double.Parse(element.Element(nameof(Customer.Longitude)).Value),
                Lattitude = double.Parse(element.Element(nameof(Customer.Lattitude)).Value),
                IsDeleted = bool.Parse(element.Element(nameof(Customer.IsDeleted)).Value),
            };
        }
        /// <summary>
        /// Prepares the list of customer for display
        /// </summary>
        /// <returns>A list of customer</returns>

        public IEnumerable<Customer> GetCustomers()
        {
            XElement customersXML = XMLTools.LoadListFromXmlElement(customersPath);
            return customersXML.Elements().Select(customer => ConvertXElementToCustomerObject(customer)).Where(customer => customer.IsDeleted == false);
        }

        /// <summary>
        /// Find a customer that has tha same id number as the parameter
        /// </summary>
        /// <param name="id">The id number of the requested customer</param>
        /// <returns>A customer for display</returns>
        public Customer GetCustomer(int id)
        {
            XElement root = XMLTools.LoadListFromXmlElement(customersPath);
            // try
            {
                return root.Elements(nameof(Customer))
                     .Select(customerElement => ConvertXElementToCustomerObject(customerElement))
                     .SingleOrDefault(customer => customer.Id == id && !customer.IsDeleted);
            }
            //catch (Exception e)
            //{
            //    //dthydyfrhedtrh לטפפפפפפפפפפפפפפפפללללל
            //}
        }

        //public void RemoveCustomer(int id)
        //{
        //    Customer customer = Customers.FirstOrDefault(customer => customer.Id == id);
        //    Customers.Remove(customer);
        //    customer.IsDeleted = true;
        //    Customers.Add(customer);
        //}

        public void AddCustomer(int customerId, string customerPhone, string customername, double customerLongitude, double customerLatitude)
        {
            XElement Customer = XMLTools.LoadListFromXmlElement(customersPath);
            XElement id = new XElement(nameof(DO.Customer.Id), customerId);
            XElement name = new XElement(nameof(DO.Customer.Name), customername);
            XElement phone = new XElement(nameof(DO.Customer.Phone), customerPhone);
            XElement longitude = new XElement(nameof(DO.Customer.Longitude), customerLongitude);
            XElement latitude = new XElement(nameof(DO.Customer.Lattitude), customerLatitude);
            XElement isDeleted = new XElement(nameof(DO.Customer.IsDeleted), false);
            Customer.Add(new XElement(nameof(DO.Customer), id, name, phone, latitude, longitude, isDeleted));
            XMLTools.SaveListToXmlElement(Customer, customersPath);
        }

        public void RemoveCustomer(int id)
        {
            throw new NotImplementedException();
        }


    }
}
