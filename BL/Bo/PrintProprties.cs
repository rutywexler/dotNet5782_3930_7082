using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bo
{
    public static class PrintProprties
    {
        public static string ToStringProps<T>(this T obj)
        {
            Type type = obj.GetType();
            string description = $"{type.Name}";

            foreach (var prop in type.GetProperties())
            {
                // Is the prop a list?
                //if (prop.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                //{

                //}
                description += $"{Environment.NewLine}{prop.Name} = {prop.GetValue(obj)}";
            }

            return description;
        }
    }
}
