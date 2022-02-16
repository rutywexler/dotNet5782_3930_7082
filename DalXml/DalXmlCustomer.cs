using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.CompilerServices;


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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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


        /// <summary>
        /// Gets parameters and create new customer 
        /// </summary>
        /// <param name="phone">The customer`s number phone</param>
        /// <param name="name">The customer`s name</param>
        /// <param name="longitude">>The position of the customer in relation to the longitude</param>
        /// <param name="latitude">>The position of the customer in relation to the latitude</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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




        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
        {
            try
            {
                XElement customers = XMLTools.LoadListFromXmlElement(customersPath);
                XElement e = (from s in customers.Elements()
                              where int.Parse(s.Element("Id").Value) == customer.Id
                              select s).FirstOrDefault();

                e.Element("Name").Value = customer.Name;
                e.Element("Phone").Value = customer.Phone;
                XMLTools.SaveListToXmlElement(customers, customersPath);


            }
            catch { throw new Exception(); }
        }
    }
}
